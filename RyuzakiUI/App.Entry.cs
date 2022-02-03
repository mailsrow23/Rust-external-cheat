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

            /* Creates an instance of our memory helper which maps physical memory for reading/writing later */
            var memHelper = PInvoke.GetMyHelper();

            Application.Run(new ConsoleApp(memHelper));
        }
    }
}