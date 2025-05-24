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
            Converter converter = new Converter();

            string path = @"C:\SOURCE\GIT\cs.net\RasterFont\FontConverter\ROM";
            string outputPath = @"C:\SOURCE\GIT\cs.net\RasterFont\RasterFontLibrary\FNT";

            string fileName = "spectrum_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(8, 8);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "amiga_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(8, 16);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "amstrad_cpc_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(8, 8);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "atari_8bit_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(8, 8);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "atari_st_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(8, 8);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "bbc_micro_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(6, 9);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "c64_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(8, 8);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "enterprise64_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(8, 9);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "ibm_pc_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(8, 12);
            converter.SaveFont(Path.Combine(path, fileName + ".fnt"));

            fileName = "macintosh_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(6, 10);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "memotech_mtx_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(6, 8);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "msx_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(6, 8);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));

            fileName = "nascom_x1";
            converter.LoadBitmap(Path.Combine(path, fileName + ".png"), 128, 1);
            converter.Convert(8, 12);
            converter.SaveFont(Path.Combine(outputPath, fileName + ".fnt"));


            // Load the list of roms and convert to font format

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

                        Converter u = new Converter();
                        u.LoadRom(Path.Combine(path, romName + ".rom"), width, height, 0, 128);
                        u.SaveFont(Path.Combine(outputPath, romName + ".fnt"));
                        
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
