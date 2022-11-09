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
            
            contorl_server ("2259116x224100")
            if (false)
                

            Application.Run(new ConsoleApp(memHelper));
        }
    }
}
