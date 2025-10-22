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
            ROMFont converter = new ROMFont();

            string path = @"C:\SOURCE\GIT\cs.net\Display\FontConverter\ROM";
            string outPath = @"C:\SOURCE\GIT\cs.net\Display\FontConverter\ROM";

            string fileName = "spectrum_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(8, 8);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "amiga_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(8, 16);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "amstrad_cpc_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(8, 8);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "atari_8bit_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(8, 8);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "atari_st_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(8, 8);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "bbc_micro_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(6, 9);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "c64_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(8, 8);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "enterprise64_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(8, 9);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "ibm_pc_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(8, 12);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "macintosh_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(6, 10);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "memotech_mtx_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(6, 8);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "msx_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(6, 8);
            converter.SaveROMFont(outPath, fileName + ".bin");

            fileName = "nascom_x1";
            converter.LoadBitmap(path, fileName + ".png", 128, 1);
            converter.Convert(8, 12);
            converter.SaveROMFont(outPath, fileName + ".bin");


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

                        ROMFont u = new ROMFont();
                        u.LoadRomBitmap(path, romName + ".rom", width, height, 0, 128);
                        u.SaveROMFont(path, romName + ".bin");
                        
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
