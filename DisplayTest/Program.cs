using ConsoleFrom;
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

            // Colour tests 1-bit 2-colour

            //Application.Run(new DisplayMonochromeText());
            Application.Run(new DisplayMonochromeGraphics());

            // Colour tests 2-bit 4-colour

            //Application.Run(new DisplayColourText());
            Application.Run(new DisplayColourGraphics());

            //// Enhanced tests 4-bit 16-colour

            ////Application.Run(new DisplayEnhancedText());
            Application.Run(new DisplayEnhancedGraphics());

            //// Vibrant tests 8-bit 256-colour

            ////Application.Run(new DisplayVibrantText());
            Application.Run(new DisplayVibrantGraphics());

            //// Vibrant tests 24-bit rgb-colour

            ////Application.Run(new DisplayTrueText());
            Application.Run(new DisplayTrueGraphics());

            ////Application.Run(new TerminalColour());
            ////Application.Run(new UserControlColour());
            ////Application.Run(new DisplayVibrantGraphics());

            basic.MonochromeGraphicsTest();     // Colour tests 1-bit 2-colour
            basic.ColourGraphicsTest();         // Colour tests 2-bit 4-colour
            basic.EnhancedGraphicsTest();     // Enhanced tests 4-bit 16-colour
            basic.VibrantGraphicsTest();      // Vibrant tests 8-bit 256-colour
            basic.TrueGraphicsTest();           // True tests 24-bit rgb-colour

                

        }
    }
}
