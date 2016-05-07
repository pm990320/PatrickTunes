using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WMPLib;

namespace MusicApp
{
    /// <summary>
    /// The PlayerState enum is used in the Player class to determine the state of the player for various uses.
    /// </summary>
    enum PlayerState
    {
        PLAYING,
        PAUSED,
        STOPPED
    }

    /// <summary>
    /// The Player Class acts as an interface to Windows Media Player. 
    /// It utilises Windows Media Player to play audio files.
    /// 
    /// Invariant: 
    /// - The Windows Media Player library is installed in the destination computer.
    /// - _currFile holds the name of the file currently playing.
    /// - the PlayerMainLoop() method is running in a thread of it's own.
    /// </summary>
    static class Player
    {
        private static WindowsMediaPlayer _player = new WindowsMediaPlayer();
        private static string _currFile = Directory.GetCurrentDirectory() + "\\default.wav";
        private static PlayerState _state = PlayerState.STOPPED;


        /// <summary>
        /// To be called inside a separate thread. Makes the program shuffle after a file finishes playing.
        /// </summary>
        public static void playerMainLoop()
        {
            while (true)
            {
                try
                {
                    if (_player.status == "Stopped")
                    {
                        Player.playNew(Player.getShuffledFile());
                    }
                }
                // will throw an exception if _player.status is accessed when playing.
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// The <c>playNew()</c> method is used to play a new file. 
        /// 
        /// It configures the player, updates statistics and updates the Title, Artist, Album and Genre.
        /// </summary>
        /// <param name="filename">A string containing the path of a valid filename</param>
        public static void playNew(string filename)
        {
            // starts the file playing and updates the _currFile to reflect the new file playing.
            stop();
            _currFile = filename;
            play();

            // updates plays statistics
            Database.updatePlays(filename);

            try
            {
                // appropriate row selected from Files table
                MusicAppDBDataSet.FilesRow rowOfFilePlaying = (MusicAppDBDataSet.FilesRow)Database.data.Files.Select("Directory='" + filename + "'").FirstOrDefault();

                // update labels with new text
                Program.d.Controls["TitleLabel"].Text = @rowOfFilePlaying.Title;
                //Program.d.Controls["ArtistLabel"].Text = @rowOfFilePlaying.Artist;
                //Program.d.Controls["AlbumLabel"].Text = @rowOfFilePlaying.Album;
                //Program.d.Controls["GenreLabel"].Text = @rowOfFilePlaying.Genre;
            }
            catch (Exception e) // if fetching the row failed, then the labels are changed to show an error.
            {
                MessageBox.Show("Exception while updating labels:\n\n" + e.ToString(), "Exception", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                Program.d.Controls["TitleLabel"].Text = "Could not update track information";
                Program.d.Controls["ArtistLabel"].Text = "";
                Program.d.Controls["GenreLabel"].Text = "";
            }

            // clears selection (unsure if this is necessary)
            Database.data.Files.Select();
        }

        /// <summary>
        /// The <c>play()</c> method plays the file currently in Windows Media Player or currently stored in _currFile.
        /// </summary>
        public static void play()
        {
            try
            {
                _player.URL = _currFile;
                _player.controls.playItem(_player.controls.currentItem);
                _state = PlayerState.PLAYING;
            }
            catch (Exception e) // if an exception still arises, keep the program open but log the exception.
            {
                MessageBox.Show("Exception while playing file:\n\n" + e.ToString() + "\n\nPlease report this bug!",
                    "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The <c>resume()</c> method invokes the the play command in WMP.
        /// </summary>
        public static void resume()
        {
            _player.controls.play();
        }

        /// <summary>
        /// The <c>stop()</c> method invokes the stop command in WMP.
        /// </summary>
        public static void stop()
        {
            _player.controls.stop();
            _state = PlayerState.STOPPED;
        }

        /// <summary>
        /// The <c>pause()</c> method invokes the pause command in WMP.
        /// </summary>
        public static void pause()
        {
            _player.controls.pause();
            _state = PlayerState.PAUSED;
        }

        /// <summary>
        /// The <c>getShuffledFile()</c> method is called when a new file needs to be picked
        /// by the shuffle algorithm. The shuffle algorithm is a simple random number generation,
        /// with seed as current time in milliseconds, which produces an integer uses as an ID to find
        /// a file in the Database.
        /// </summary>
        /// <returns>Returns the filename of the file calculated by the 
        /// Shuffle algorithm as the next file.</returns>
        public static string getShuffledFile()
        {

            Random randomNumberGenerator = new Random((int)DateTime.Now.Ticks);

            int nSongs = Database.data.Files.Rows.Count;
            int nChosen = randomNumberGenerator.Next(1, nSongs);

            // get the number of the lowest ID
            MusicAppDBDataSet.FilesRow rowWithLowestID = (MusicAppDBDataSet.FilesRow)Database.data.Files.Select("", "ID ASC").FirstOrDefault();

            int nIDChosen = rowWithLowestID.ID + nChosen;

            // get the row with the chosen ID
            MusicAppDBDataSet.FilesRow resultRow = (MusicAppDBDataSet.FilesRow)Database.data.Files.Select("ID='" + nIDChosen + "'").FirstOrDefault();

            // temp
            return resultRow.Directory;

        }

    }
}



// OLD CODE - you know what they say: learn from your mistakes.
// This old shuffle algortithm is deprecated as it relied on tracking skipped and finished for each song,
// a feature theat was removed.

/*public static string getShuffledFile()
{
    List<int> idsOrderedMostSkipped = new List<int>();
    List<int> idsOrderedLeastFinished = new List<int>();
    List<int> idsOrderedLeastPlayed = new List<int>();
    List<int> shuffleAverage = new List<int>();

    // populate the idsOrderedMostSkipped list
    MusicAppDBDataSet.FilesRow[] skippedarray = (MusicAppDBDataSet.FilesRow[])Database.data.Files.Select("", "Skipped DESC");
    foreach (MusicAppDBDataSet.FilesRow fr in skippedarray)
    {
        if (fr.Directory != _currFile)
        {
            idsOrderedMostSkipped.Add(fr.ID);
        }
    }

    // populate the idsOrderedLeastFinished list
    MusicAppDBDataSet.FilesRow[] finishedarray = (MusicAppDBDataSet.FilesRow[])Database.data.Files.Select("", "Finished ASC");
    foreach (MusicAppDBDataSet.FilesRow fr in finishedarray)
    {
        if (fr.Directory != _currFile)
        {
            idsOrderedLeastFinished.Add(fr.ID);
        }
    }

    // populate the idsOrederedLeastPlayed list
    MusicAppDBDataSet.FilesRow[] playedarray = (MusicAppDBDataSet.FilesRow[])Database.data.Files.Select("", "Played ASC");
    foreach (MusicAppDBDataSet.FilesRow fr in playedarray)
    {
        if (fr.Directory != _currFile)
        {
            idsOrderedLeastPlayed.Add(fr.ID);
        }
    }

    // get the number of the lowest ID
    MusicAppDBDataSet.FilesRow rowWithLowestID = (MusicAppDBDataSet.FilesRow)Database.data.Files.Select("", "ID ASC").FirstOrDefault();

    // loop through all IDs
    for (int i = rowWithLowestID.ID; i < rowWithLowestID.ID + Database.data.Files.Rows.Count; i++)
    {
        // calculate the shuffle algorithm average
        int avg = (2 * idsOrderedLeastPlayed.IndexOf(i)) + (3 * idsOrderedLeastFinished.IndexOf(i)) + (idsOrderedMostSkipped.IndexOf(i) / 2);
        // add it to the list
        shuffleAverage.Add(avg);
    }

    // new random number generator
    Random r = new Random(new DateTime().Millisecond);

    // get a randomly selected number. The result ID will be the _(randomly selected)_ highest suffle average.
    int ranking = r.Next(0, 20);
    for (int i = 0; i < ranking; i++) {
        shuffleAverage.Remove(shuffleAverage.IndexOf(shuffleAverage.Max()));
    }

    int resultID = shuffleAverage.IndexOf(shuffleAverage.Max());

    // use the ID to retreive the filename
    MusicAppDBDataSet.FilesRow rr = (MusicAppDBDataSet.FilesRow) Database.data.Files.Select("ID='" + (resultID + rowWithLowestID.ID).ToString() + "'").FirstOrDefault();

    // return the filename
    return rr.Directory;
}*/

