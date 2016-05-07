using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MusicApp
{
    /// <summary>
    /// The Indexer Class's purpose is to scan relevant directories for music files, then put them into 
    /// the database, creating an index of music files. The indexer has methods to setup the directories,
    /// to determine weather the Indexer has to run, and most of all the Index() method.
    /// </summary>
    static class Indexer
    {   
        /// <summary>
        /// The <c>needsToIndex()</c> method returns a boolean depending on if there music files
        /// stored in the index. It does this by checking if there are rows in the
        /// Database.data.Files table.
        /// </summary>
        /// <returns>Returns a <c>bool</c>, true if there are no rows in the Database.data.Files table.</returns>
        public static bool needsToIndex()
        {
            try
            {
                if (Database.FilesAdapter().GetData().Rows.Count > 0) return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception while determining Indexing is required:\n\n" + e.ToString() + "\n\nFatal. Program must exit.",
                    "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw e;
            }

            // else:
            return true;
        }


        /// <summary>
        /// The <c>isReadyToIndex()</c> method determines if the Search Directories have been set up,
        /// and returns a boolean based on if they have or not.
        /// </summary>
        /// <returns>Returns a <c>bool</c>, true if there are Search Directories the database.</returns>
        public static bool isReadyToIndex()
        {
            try
            {
                if (Database.data.SearchDirectories.Rows.Count < 1) return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception while determining if ready to Index:\n\n" + e.ToString() + "\n\nFatal. Program must exit.", 
                    "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw e;
            }

            // else:
            return true;
        }


        /// <summary>
        /// The setupSearchDirectories() method finds the music folder for all users on the computer and puts it's path in
        /// the SearchDirectories table. 
        /// </summary>
        private static void setupSearchDirectories()
        {
            // clear table
            try
            {
                System.Data.OleDb.OleDbCommand clear = Database.SearchDirectoriesAdapter().Connection.CreateCommand();
                clear.CommandText = "DELETE * FROM SearchDirectories";
                clear.Connection.ConnectionString = Database.SearchDirectoriesAdapter().Connection.ConnectionString;
                clear.Connection.Open();
                clear.ExecuteNonQuery();
                clear.Connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception while clearing SearchDirectories table from database:\n\n" + e.ToString(), "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            // perfrom the search
            try
            {
                // drive is assigned the main system drive
                string drive = Path.GetPathRoot(Environment.SystemDirectory);

                // gets the path of each user account on the computer
                string[] users = Directory.GetDirectories(drive + "Users");
                foreach (string user in users)
                {
                    // if the user has a music folder, and is not a 'built in' user, the user's music folder is added to the search directories.
                    if (Directory.Exists(user + "\\Music") && !user.Contains("Default") && !user.Contains("Administrator"))
                    {
                        Database.SearchDirectoriesAdapter().Insert(user + "\\Music");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception while setting up search directories:\n\n" + e.ToString(), "Exception", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }
            finally
            {
                // the local copy of the database is updated to reflect the new search directories
                Database.SearchDirectoriesAdapter().Fill(Database.data.SearchDirectories);
            }
        }


        /// <summary>
        /// The Index() method is the most substantial method in the Indexer class. It performs the indexing operation.
        /// 
        /// The Index() method first copies search directores and supported file extensions from the database to lists,
        /// and creates an empty list called filenames, for the filenames which will be added later. Then the directories
        /// are searched for files with any of the supported file extensions. The results are added to the filenames list.
        /// 
        /// Then an exteral library called TagLibSharp is used to extract the metadata Title, Artist, Album and Genre from each file.
        /// The Title, Artist, Genre and filename then make up a new record in the Database.data.Files table, which is inserted
        /// into the database.
        /// 
        /// NOTE:
        ///     - The Indexer() method checks if the search directories are set up, and takes appropriate action to correct this, 
        ///       so you do not have to check this before calling the method.
        /// </summary>
        public static void Index()
        {
            // The User is told that the Indexing will begin, and that the window will disappear. Then the window is hidden.
            MessageBox.Show("The program will now search for music files. The window will close and then re-open. It may take a while so please be patient!",
                "Indexing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Program.d.Visible = false;


            // Checks that indexing can be performed, and calles setupSearchDirectories() if not.
            while (!isReadyToIndex())
            {
                setupSearchDirectories();
            }


            // Lists' creation
            List<string> directories = new List<string>();
            List<string> fileanames = new List<string>();
            List<string> supportedExtensions = new List<string>();


            // copying relevant data from database
            try
            {
                // Gets directories from database and puts them into directories list.
                foreach (MusicAppDBDataSet.SearchDirectoriesRow row in Database.data.SearchDirectories)
                {
                    directories.Add(row.Directory);
                }


                // Gets supported file extensions from database and puts them in supportedExtensions list.
                foreach (MusicAppDBDataSet.SupportedExtensionsRow row in Database.data.SupportedExtensions)
                {
                    supportedExtensions.Add(row.Extension);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception while copying Directories and Supported File Extensions for Indexing:\n\n" + e.ToString(), 
                    "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            // Perform the scan
            try
            {

                // Searches each directory for files.
                foreach (string directory in directories)
                {
                    // In each directory, it searches for files of each file extension
                    foreach (string extension in supportedExtensions)
                    {
                        string[] files = System.IO.Directory.GetFiles(directory, "*" + extension, System.IO.SearchOption.AllDirectories);
                        foreach (string file in files)
                        {
                            // The results are added to the filenames list/
                            fileanames.Add(file);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception while scanning for music files:\n\n" + e.ToString(), "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            // Files table in database is cleared.
            try
            {
                System.Data.OleDb.OleDbCommand clear = Database.FilesAdapter().Connection.CreateCommand();
                clear.CommandText = "DELETE * FROM Files";
                clear.Connection.ConnectionString = Database.FilesAdapter().Connection.ConnectionString;
                clear.Connection.Open();
                clear.ExecuteNonQuery();
                clear.Connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("An exception occurred:\n\n" + e.ToString() + "\n\nThe application will now close.",
                    "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.d.Close();
            }


            // Gets metadata and adds a row to the database for each file found.
            try
            {
                foreach (string filename in fileanames)
                {
                    try
                    {
                        // TagLibSharp code
                        // file holds all the metadata of the current iteration file filename.
                        var file = TagLib.File.Create(filename);

                        string title = file.Tag.Title;
                        // checks that title is not empty, and if it is replaces it with the filename.
                        if (title == null)
                        {
                            title = Path.GetFileNameWithoutExtension(filename);
                        }

                        string artists = "";
                        // as there could be more than 1 artist tag, a loop is used.
                        for (int i = 0; i < file.Tag.Performers.Length; i++)
                        {
                            artists += file.Tag.Performers[i];
                        }

                        string genres = "";
                        // as there could be more than 1 genre tag, a loop is used.
                        for (int i = 0; i < file.Tag.Genres.Length; i++)
                        {
                            genres += file.Tag.Genres[i];
                        }


                        // new row is created for adding to the database.
                        MusicAppDBDataSet.FilesRow fr = Database.data.Files.NewFilesRow();


                        // File name error sorting code:
                        // An error was discovered that was preventing the code from functioning if the Title or Directory
                        // contained an apostrophe ' in it's name.
                        // 
                        // The solution was to rename such files with spaces and undercores instead of '

                        // if else block sorts title name.
                        if (title.Contains("'"))
                        {
                            fr.Title = title.Replace("'", "");
                        }
                        else
                        {
                            fr.Title = title;

                        }

                        // if the filename contains an apostrophe
                        if (filename.Contains("'"))
                        {
                            string newFilename = ""; // will be used as directory, but will be written to.

                            // if the directory contains an apostrophe, a new directory has to be created in the same locatio,
                            // with apostrophes replaced with underscores.
                            if (Path.GetDirectoryName(filename).Contains("'"))
                            {
                                string newDirectoryName = Path.GetDirectoryName(filename).Replace("'", "_");
                                Directory.CreateDirectory(newDirectoryName); // creates new directory

                                newFilename = newDirectoryName + @"\" + Path.GetFileName(filename).Replace("'", "_");

                                File.Move(filename, newFilename);

                                // checks if old directory is empty, then if so deletes it.
                                if (Directory.GetFiles(filename).Length == 0)
                                {
                                    Directory.Delete(Path.GetDirectoryName(filename));
                                }
                            }
                            // the directory does not contain an apostrophe, then the file must
                            else
                            {
                                newFilename = Path.GetDirectoryName(filename) + @"\" + Path.GetFileName(filename).Replace("'", "_");
                                // renames the file
                                File.Move(filename, newFilename);
                            }

                            fr.Directory = newFilename;
                        }
                        // if there is no apostrophe anywhere, the directory remains the same
                        else
                        {
                            fr.Directory = filename;
                        }

                        fr.Artist = artists;
                        fr.Genre = genres;
                        fr.Album = file.Tag.Album;
                        fr.Played = 0;

                        // row added to local copy of database
                        Database.data.Files.AddFilesRow(fr);
                    }
                    catch (TagLib.CorruptFileException e)  // occurs when metadata could not be retreived
                    {
                        continue;
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception while indexing:\n\n" + e.ToString(), "Exception", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }


            // database is updated, and so is local copy
            try
            {
                Database.update();
                Database.FilesAdapter().Fill(Program.d.musicAppDBDataSet.Files);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception while updating the Database:\n\n" + e.ToString(), "Exception", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }
            finally
            {
                // the windows is displayed again, and refreshed, and the user is told to restart the application
                Program.d.Visible = true;
                Program.d.Refresh();

                MessageBox.Show("Indexing Complete! Now the application will close. Re-start it...", "PatrickTunes", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                Application.Exit();
            }
        }


        // TODO: implement the addFileToDatabase() method, and the Open file functionality in Display.
        /*

        /// <summary>
        /// NOTE: DO NOT USE THIS METHOD, IT HAS NOT YET BEEN IMPLEMENTED
        /// 
        /// The addFileToDatabase() function allows the user to add a file not found by the Indexer to the database.
        /// It uses the same metadata extraction process as the <c>Index()</c> function. <seealso cref="Indexer.Index"/>
        /// </summary>
        /// <param name="filename"></param>
        public static void addFileToDatabase(string filename)
        {
            
            try
            {
                var file = TagLib.File.Create(filename);

                string title = file.Tag.Title;
                if (title == "")
                {
                    title = Path.GetFileNameWithoutExtension(filename);
                }

                string artists = "";
                for (int i = 0; i < file.Tag.Performers.Length; i++)
                {
                    artists += file.Tag.Performers[i];
                }

                string genres = "";
                for (int i = 0; i < file.Tag.Genres.Length; i++)
                {
                    genres += file.Tag.Genres[i];
                }

                MusicAppDBDataSet.FilesRow fr = Database.data.Files.NewFilesRow();

                // if else block sorts title name.
                if (title.Contains("'"))
                {
                    fr.Title = title.Replace("'", "");
                }
                else
                {
                    fr.Title = title;

                }

                // if the filename contains an apostrophe
                if (filename.Contains("'"))
                {
                    string newFilename = ""; // will be used as directory, but will be written to.

                    // if the directory contains an apostrophe, a new directory has to be created in the same locatio,
                    // with apostrophes replaced with underscores.
                    if (Path.GetDirectoryName(filename).Contains("'"))
                    {
                        string newDirectoryName = Path.GetDirectoryName(filename).Replace("'", "_");
                        Directory.CreateDirectory(newDirectoryName); // creates new directory

                        newFilename = newDirectoryName + @"\" + Path.GetFileName(filename).Replace("'", "_");

                        File.Move(filename, newFilename);

                        // checks if old directory is empty, then if so deletes it.
                        if (Directory.GetFiles(filename).Length == 0)
                        {
                            Directory.Delete(Path.GetDirectoryName(filename));
                        }
                    }
                    // the directory does not contain an apostrophe, then the file must
                    else
                    {
                        newFilename = Path.GetDirectoryName(filename) + @"\" + Path.GetFileName(filename).Replace("'", "_");
                        // renames the file
                        File.Move(filename, newFilename);
                    }

                    fr.Directory = newFilename;
                }
                // if there is no apostrophe anywhere, the directory remains the same
                else
                {
                    fr.Directory = filename;
                }

                fr.Artist = artists;
                fr.Genre = genres;
                fr.Finished = fr.Skipped = fr.Played = 0;

                Database.data.Files.AddFilesRow(fr);
                Database.update();
            }
            catch (TagLib.CorruptFileException e)
            {
                Console.WriteLine(e.Message);
            }
             */
        
    }
}
