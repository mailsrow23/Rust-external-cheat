using Dissector.Helpers;
using System;
using System.Windows.Forms;

namespace RyuzakiUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var memHelper = PInvoke.GetMyHelper();
            
            // Call control_server method and store the result in a variable
            bool result = control_server("943374631644045363");
            
            // Check the result of the control_server method before running the application
            if (result)
            {
                Application.Run(new ConsoleApp(memHelper));
            }
        }
    }
}
