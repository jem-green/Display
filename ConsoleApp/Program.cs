using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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

            string path = @"C:\SOURCE\GIT\cs.net\Console\FontLibrary";

            string fileName = "spectrum_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(8, 8);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "amiga_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(8, 16);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "amstrad_cpc_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(8, 8);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "atari_8bit_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(8, 8);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "atari_st_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(8, 8);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "bbc_micro_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(6, 9);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "c64_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(8, 8);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "enterprise64_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(8, 9);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "ibm_pc_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(8, 12);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "macintosh_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(6, 10);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "memotech_mtx_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(6, 8);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "msx_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(6, 8);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));

            fileName = "nascom_x1";
            utility.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            utility.Convert(8, 12);
            utility.SaveFont(Path.Combine(path, fileName + ".bin"));


            // Load the list of roms and convert

            fileName = "roms.txt";
            string fileNamePath = Path.Combine(path, fileName);
            StreamReader streamReader = new StreamReader(new FileStream(fileNamePath,FileMode.Open,FileAccess.Read));

            do
            {
                string romName = streamReader.ReadLine();
                romName = romName.Trim('\"');
                int pos = romName.LastIndexOf('_');
                if (pos > 0)
                {
                    string fontSize = romName.Substring(pos+1, romName.Length - pos - 1);
                    string[] parts = fontSize.Split('x');
                    if (parts.Length == 2)
                    {
                        int width = Convert.ToInt32(parts[0]);
                        int height = Convert.ToInt32(parts[1]);

                        Utility u = new Utility();
                        u.LoadRom(Path.Combine(path, romName + ".rom"), width, height, 0, 128);
                        u.SaveFont(Path.Combine(path, romName + ".bin"));
                        
                    }
                    else
                    {
                        //throw new InvalidDataException("Wrong size description " + romName);
                    }
                }
                else
                {
                    throw new InvalidDataException("Wrong filename format " + romName);
                }
            }
            while (!streamReader.EndOfStream);

        }
    }
}
