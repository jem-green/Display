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
        public static void MonochromeGraphicsTest()     // 1-bit colour
        {

            IGraphic mgd = new MonochromeGraphicsDisplay(2, 2, 10);

            mgd.Clear();  // Perhaps this is part of draw?

            mgd.Put(0, 0, new SolidColour(255, 0, 0));      // Red
            mgd.Put(1, 0, new SolidColour(255, 255, 0));    // Yellow
            mgd.Put(0, 1, new SolidColour(0, 255, 255));    // Cyan
            mgd.Put(1, 1, new SolidColour(255, 0, 255));    // Magenta

            mgd.PartialGenerate(0, 0, 1, 1);

            mgd.Save(".", "basic1.png");

            System.Console.WriteLine("Basic image complete!");

        }

        public static void ColourGraphicsTest()         // 2-bit colour
        {
            IGraphic cgd = new ColourGraphicsDisplay(2, 2, 10);

            cgd.Clear();  // Perhaps this is part of draw?

            cgd.Put(0, 0, new SolidColour(255, 0, 0));      // Red
            cgd.Put(1, 0, new SolidColour(255, 255, 0));    // Yellow
            cgd.Put(0, 1, new SolidColour(0, 0, 255));    // Cyan
            cgd.Put(1, 1, new SolidColour(255, 0, 255));    // Magenta

            cgd.PartialGenerate(0, 0, 1, 1);

            cgd.Save(".", "basic2.png");

            System.Console.WriteLine("Basic image complete!");

        }

        public static void EnhancedGraphicsTest()       // 4-bit colour
        {
            IGraphic egd = new EnhancedGraphicsDisplay(2, 2, 10);

            egd.Clear();  // Perhaps this is part of draw?

            egd.Put(0, 0, new SolidColour(255, 0, 0));        // Red
            egd.Put(1, 0, new SolidColour(255, 255, 0));      // Yellow
            egd.Put(0, 1, new SolidColour(0, 255, 255));      // Cyan
            egd.Put(1, 1, new SolidColour(255, 0, 255));      // Magenta

            egd.PartialGenerate(0, 0, 1, 1);

            egd.Save(".", "basic4.png");

            System.Console.WriteLine("Basic image complete!");

        }

        public static void VibrantGraphicsTest()        // 8-bit colour
        {
            IGraphic vgd = new VibrantGraphicsDisplay(2, 2, 10);

            vgd.Clear();  // Perhaps this is part of draw?

            vgd.Put(0, 0, new SolidColour(255, 0, 0));        // Red
            vgd.Put(1, 0, new SolidColour(255, 255, 0));      // Yellow
            vgd.Put(0, 1, new SolidColour(0, 255, 255));      // Cyan
            vgd.Put(1, 1, new SolidColour(255, 0, 255));      // Magenta

            vgd.PartialGenerate(0, 0, 1, 1);

            vgd.Save(".", "basic8.png");

            System.Console.WriteLine("Basic image complete!");

        }

        public static void TrueGraphicsTest()           // 24-bit colour
        {
            IGraphic tgd = new TrueGraphicsDisplay(2, 2, 10);

            tgd.Clear();  // Perhaps this is part of draw?

            tgd.Put(0, 0, new SolidColour(255, 0, 0));        // Red
            tgd.Put(1, 0, new SolidColour(255, 255, 0));      // Yellow
            tgd.Put(0, 1, new SolidColour(0, 255, 255));      // Cyan
            tgd.Put(1, 1, new SolidColour(255, 0, 255));      // Magenta

            tgd.PartialGenerate(0, 0, 1, 1);

            tgd.Save(".", "basic24.png");

            System.Console.WriteLine("Basic image complete!");

        }



    }
}
