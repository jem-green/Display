using System;
using System.Drawing;
using System.IO;

namespace FontLibrary
{
    public class Utility
    {
        Bitmap _image;
        int _hbits;
        int _vbits;
        int _width;
        int _height;
        byte[] _data;

        public void LoadFont(string filePath)
        {
            BinaryReader binaryReader = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read));

            binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);

            _hbits = binaryReader.ReadByte();
            _vbits = binaryReader.ReadByte();
            int hbytes = (int)Math.Round((double)_hbits / 8);
            int length = hbytes * _vbits * 255;
            _data = new byte[length];
            _data = binaryReader.ReadBytes(length);
            binaryReader.Close();

        }

        public void LoadBitmap(string fileNamePath, int width, int height)
        {
            string path = Path.GetDirectoryName(fileNamePath);
            string fileNmae = Path.GetFileName(fileNamePath);
            if (path.Length==0)
            {
                path = ".";
            }
            fileNamePath = Path.Combine(path, fileNmae);

            _image = new Bitmap(fileNamePath);
            if (width > 255)
            {
                throw new ArgumentOutOfRangeException("Must be less than 256");
            }
            else if (height > 255)
            {
                throw new ArgumentOutOfRangeException("Must be less than 256");
            }
            else
            {
                _width = width;
                _height = height;
            }

            // could guess the arrangement of the bits from the image
            // but lets assume not at theis stage

        }

        public void Convert(int horizontal, int vertical)
        {
            _hbits = horizontal;
            _vbits = vertical;

            int hbytes = (int)Math.Round((double)_hbits / 8);

            _data = new byte[256 * hbytes * _hbits];

            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    for (int y = 0; y < _vbits; y++)
                    {
                        byte value = 0;
                        for (int x = 0; x < 8; x++)
                        {
                            Color colour = _image.GetPixel(column * hbytes * 8 + x, row * _vbits + y);
                            if ((colour.R == 0) && (colour.G ==0) && (colour.B ==0) && (colour.A == 255))
                            {
                                value = value;
                            }
                            else
                            {
                                value = (byte)(value | (128 >> x));
                            }
                        }
                        _data[column * hbytes * _vbits + y] = value;
                    }

                }
            }
        }

        public void SaveFont(string filePath)
        {
            if (File.Exists(filePath) == true)
            {
                File.Delete(filePath);
            }

            BinaryWriter binaryWriter = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write));
            binaryWriter.Write((byte)_hbits);
            binaryWriter.Write((byte)_vbits);
            int hbytes = (int)Math.Round((double)_hbits / 8);
            int length = hbytes * _vbits * 255;
            binaryWriter.Write(_data, 0, length);
            binaryWriter.Flush();
            binaryWriter.Close();
        }

        public void DisplayChar(char character)
        {
            int hbytes = (int)Math.Round((double)_hbits / 8);

            for (int r = 0; r < _vbits; r++)
            {
                string output = "";
                byte value = _data[(byte)character * hbytes * _vbits + r ];
                for (int c = 0; c < 8; c++)
                {
                    byte check = (byte)(value & (128 >> c));
                    if (check != 0)
                    {
                        output = output + "#";
                    }
                    else
                    {
                        output = output + ".";
                    }
                }
                Console.WriteLine(output);
            }
        }

        public void DisplayString(string text)
        {
            int hbytes = (int)Math.Round((double)_hbits / 8);

            foreach (char character in text)
            {
                for (int r = 0; r < _vbits; r++)
                {
                    string output = "";
                    byte value = _data[(byte)character * hbytes * _vbits + r];
                    for (int c = 0; c < 8; c++)
                    {
                        byte check = (byte)(value & (128 >> c));
                        if (check != 0)
                        {
                            output = output + "#";
                        }
                        else
                        {
                            output = output + ".";
                        }
                    }
                    Console.WriteLine(output);
                }
            }
        }
    }
}

