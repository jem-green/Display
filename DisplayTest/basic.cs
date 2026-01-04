using DisplayLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleFrom
{
    internal class basic
    {
        public static void VibrantGraphicsTest()   // 8-bit colour
        {
            //IGraphic vgd = new VibrantGraphicsDisplay(2, 2, 10);
            IGraphic vgd = new TrueGraphicsDisplay(2, 2, 10);

            vgd.Clear();  // Perhaps this is part of draw?

            vgd.Put(0, 0, new SolidColour(255,0,0));      // Red
            vgd.Put(1, 0, new SolidColour(255,255,0));    // Yellow
            vgd.Put(0, 1, new SolidColour(0,255,255));    // Cyan
            vgd.Put(1, 1, new SolidColour(255,0,255));  // Magenta

            vgd.PartialGenerate(0,0,2,2);

            vgd.Save(".", "basic8.png");

            System.Console.WriteLine("Basic image complete!");

        }

        public static void ColourGraphicsTest()   // 8-bit colour
        {
            //IGraphic vgd = new VibrantGraphicsDisplay(2, 2, 10);
            IGraphic cgd = new ColourGraphicsDisplay(2, 2, 10);

            cgd.Clear();  // Perhaps this is part of draw?

            cgd.Put(0, 0, new SolidColour(255, 0, 0));      // Red
            cgd.Put(1, 0, new SolidColour(255, 255, 0));    // Yellow
            cgd.Put(0, 1, new SolidColour(0, 255, 255));    // Cyan
            cgd.Put(1, 1, new SolidColour(255, 0, 255));  // Magenta

            cgd.PartialGenerate(0, 0, 2, 2);

            cgd.Save(".", "basic2.png");

            System.Console.WriteLine("Basic image complete!");

        }

    }
}
