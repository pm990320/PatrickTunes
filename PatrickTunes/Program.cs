using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace MusicApp
{
    /// <summary>
    /// The class that defines the entry point for the Program.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The Windows Form responsible for the application.
        /// </summary>
        public static Display d = new Display();
        
        
        /// <summary>
        /// This method is used to determine if the app.manifest worked in forcing Run As Administrator.
        /// If not, then the application cannot run.
        /// </summary>
        /// <returns>Retruns true if the user is Administrator.</returns>
        public static bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }
        
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!IsUserAdministrator())
            {
                MessageBox.Show("This program requires to be run as administrator.", "UAC Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            Application.EnableVisualStyles();
            Application.Run(d);
        }
    }
}
