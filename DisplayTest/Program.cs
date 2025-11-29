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
            
            // Colour tests

            Application.Run(new DisplayColourText());
            Application.Run(new DisplayColourGraphics());
            //Application.Run(new DisplayMonochromeText());
            //Application.Run(new ConsoleMonochrome());
            //Application.Run(new TerminalColour());
            //Application.Run(new UserControlColour());
            //Application.Run(new DisplayVibrantGraphics());

        }
    }
}
