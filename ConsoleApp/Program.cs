using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Utility utility = new Utility();
            //utility.LoadBitmap(@"C:\Users\jemgr\OneDrive\Source\Projects\Console\ConsoleApp\bin\Debug\spectrum_x1.png", 128, 1);
            utility.LoadBitmap("spectrum_x1.png", 128, 1);
            utility.Convert(8, 8);
            utility.SaveFont("spectrum.bin");

            // check loading

            Utility u = new Utility();
            //u.LoadFont("spectrum.bin");
            u.LoadRom("IBM_PC_V1_8x8.rom",8,8,0,128);
            u.SaveFont("IBM_PC_V1_8x8.bin");

            for (byte c = 0; c < 128; c++)
            {
                u.DisplayChar((char)c);
            }

            u.LoadRom("IBM_VGA_8x16.rom",8,16,0,128);
            u.SaveFont("IBM_VGA_8x16.bin");
            for (byte c = 0; c < 128; c++)
            {
                u.DisplayChar((char)c);
            }

        }
    }
}
