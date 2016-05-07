using MusicApp.MusicAppDBDataSetTableAdapters;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MusicApp
{
    /// <summary>
    /// The database class acts as an interface to the Database necessary for this project.
    /// </summary>
    static class Database
    {
        /// <summary>
        /// <c>public static MusicAppDBDataSet data</c> this is kept public because it is a dataset useable by all classes.
        /// </summary>
        public static MusicAppDBDataSet data = new MusicAppDBDataSet();


        // ADAPTERS
        private static SearchDirectoriesTableAdapter _searchDirectoriesAdapter = new SearchDirectoriesTableAdapter();
        private static SupportedExtensionsTableAdapter _supportedExtensionsAdapter = new SupportedExtensionsTableAdapter();
        private static FilesTableAdapter _fileAdapter = new FilesTableAdapter();


        /// <summary>
        /// The static consturctor <c>static Database()</c> is used to give the Table Adapters the connection strings to the database,
        /// and to initialise the tables in data <seealso cref="Database.data"/>
        /// </summary>
        static Database()
        {
            try
            {
                _fileAdapter.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory() + @"\MusicAppDB.mdb";
                _searchDirectoriesAdapter.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory() + @"\MusicAppDB.mdb";
                _supportedExtensionsAdapter.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory() + @"\MusicAppDB.mdb";

                _fileAdapter.Fill(data.Files);
                _searchDirectoriesAdapter.Fill(data.SearchDirectories);
                _supportedExtensionsAdapter.Fill(data.SupportedExtensions);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not initialise Database. Exception: \n\n" + e.ToString() + "\n\nException was fatal. Program will now close.", 
                    "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw e;
            }
        }


        /// <summary>
        /// Acts as a get function for <c>_searchDirectoriesAdapter</c>.
        /// 
        /// After any manipulation of the database using the Adapters, Fill Database.data with that adapter.
        /// </summary>
        /// <returns>Returns <c>_searchDirectoriesAdapter</c>.</returns>
        public static SearchDirectoriesTableAdapter SearchDirectoriesAdapter()
        {
            return _searchDirectoriesAdapter;
        }


        /// <summary>
        /// Acts as a get function for <c>_fileAdapter</c>.
        /// 
        /// After any manipulation of the database using the Adapters, Fill Database.data with that adapter.
        /// </summary>
        /// <returns>Returns <c>_fileAdapter</c>.</returns>
        public static FilesTableAdapter FilesAdapter()
        {
            return _fileAdapter;
        }


        /// <summary>
        /// Updates the data in the actual database with the (assumingly) modified <c>Database.data</c> dataset.
        /// WARNING, this will erase any changes made directly on the Database and not on Database.data!
        /// After any manipulation of the database using the Adapters, Fill Database.data with that adapter.
        /// <seealso cref="Database.data"/>
        /// </summary>
        public static void update() {
            try
            {
                _fileAdapter.Update(data.Files);
                _searchDirectoriesAdapter.Update(data.SearchDirectories);
                // no need to update supported extensions, they never change
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Exception occurred while updating database: \n\n" + e.ToString(), "Exception", 
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }


        /// <summary>
        /// The <c>updateStats()</c> method is used to update the played field in the database row corresponding to a given file. 
        /// </summary>
        /// <param name="directory">The directory (a.k.a. filename) of the file being updated.</param>
        public static void updatePlays(string directory)
        {
            try
            {
                Database.data.Files.Select();
                MusicAppDBDataSet.FilesRow fr = (MusicAppDBDataSet.FilesRow)data.Files.Select("Directory='" + directory + "'").FirstOrDefault();

                fr.Played += 1;

                _fileAdapter.Update(fr);
                _fileAdapter.Fill(Program.d.musicAppDBDataSet.Files);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception occurred when updating Played field. \n\n" + e.ToString());
            }
            finally
            {
                Database.data.Files.Select();
                
                Program.d.DataView.Refresh();
            }
        }
     }
}
