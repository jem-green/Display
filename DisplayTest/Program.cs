using System;
using System.Windows.Forms;

namespace DisplayTest
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
            Application.Run(new DisplayColour());
            Application.Run(new DisplayMonochrome());
            Application.Run(new ConsoleMonochrome());
            Application.Run(new TerminalColour());
            Application.Run(new UserControlColour());

        }
    }
}
