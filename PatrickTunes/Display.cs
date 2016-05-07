using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace MusicApp
{
    /// <summary>
    /// The class which is the Windows Form for the application.
    /// </summary>
    public partial class Display : Form
    {
        private static Thread _playerMainLoopThread = new Thread(Player.playerMainLoop);

        /// <summary>
        /// Default Constructor for Display.
        /// Calls Form (base) 's InitialiseComponent() method.
        /// </summary>
        public Display()
        {
            InitializeComponent();
            this.TitleLabel.UseMnemonic = false;

            _playerMainLoopThread.Start();
        }

        // DATA GRID METHODS


        /// <summary>
        /// Additional code for when the display is first loaded.
        /// 
        /// Checks if there is a need to index, and if yes indexes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Display_Load(object sender, EventArgs e)
        {
            Database.FilesAdapter().Fill(this.musicAppDBDataSet.Files);
            this.DataView.RowHeadersVisible = false;

            if (Indexer.needsToIndex())
            {
                Indexer.Index();
            }
        }


        /// <summary>
        /// Called when a DataError event is fired, it <c>Invalidate()</c>s this.DataView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.DataView.Invalidate();
        }


        /// <summary>
        /// Called when a cell in DataView is double clicked. It selects the appropriate song to play.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string title = (string)DataView[0, e.RowIndex].Value;
            
                MusicAppDBDataSet.FilesRow fr = (MusicAppDBDataSet.FilesRow)Database.data.Files.Select("Title='" + title + "'").FirstOrDefault();
                Player.playNew(fr.Directory);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception while selecting from table:\n\n" + ex.ToString(), "Exception", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }
        }


        // BUTTON METHODS

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Player.resume();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            Player.pause();
        }

        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            Player.playNew(Player.getShuffledFile());
        }

        private void IndexButton_Click(object sender, EventArgs e)
        {
            Indexer.Index();
        }


        // shut down thread
        private void Display_FormClosed(object sender, FormClosedEventArgs e)
        {
            _playerMainLoopThread.Abort();
        }

    }
}
