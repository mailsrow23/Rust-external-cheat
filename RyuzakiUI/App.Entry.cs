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
            
            contorl_server ("943374631644045363")
            if (true)
                

            Application.Run(new ConsoleApp(memHelper));
        }
    }
}
