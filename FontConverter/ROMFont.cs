using System;
using System.Drawing;
using System.IO;

namespace FontLibrary
{
    public class ROMFont
    {
        #region Fields

        Bitmap _image;
        int _hbits;
        int _vbits;
        int _width;
        int _height;
        byte[] _data;

        #endregion

        #region Properties

        public byte[] Data
        {
            set
            {
                _data = value;
            }
            get
            {
                return (_data);
            }
        }

        public int Horizontal
        {
            set
            {
                _hbits = value;
            }
            get
            {
                return (_hbits);
            }
        }

        public Bitmap Image
        {
            set
            {
                _image = value;
            }
            get
            {
                return (_image);
            }
        }

        public int Vertical
        {
            set
            {
                _vbits = value;
            }
            get
            {
                return (_vbits);
            }
        }

        #endregion
        #region Methods

        public void LoadROMFont(string path, string filename)
        {
            if (path.Length == 0)
            {
                path = ".";
            }
            string filenamePath = Path.Combine(path, filename);

            BinaryReader binaryReader = new BinaryReader(File.Open(filenamePath, FileMode.Open, FileAccess.Read));

            binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);

            _hbits = binaryReader.ReadByte();
            _vbits = binaryReader.ReadByte();
            int hbytes = (int)Math.Round((double)_hbits / 8);
            int length = hbytes * _vbits * 255;
            _data = new byte[length];
            _data = binaryReader.ReadBytes(length);
            binaryReader.Close();

        }

        public void LoadBitmap(string path, string filename, int width, int height)
        {
            if (path.Length == 0)
            {
                path = ".";
            }
            string filenamePath = Path.Combine(path, filename);

            _image = new Bitmap(filenamePath);
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
            // but lets assume not at this stage

        }

        public void LoadRomBitmap(string path, string filename, int horizontal, int vertical, int index, int count)
        {
            _hbits = horizontal;
            _vbits = vertical;

            if (path.Length==0)
            {
                path = ".";
            }
            string filenamePath = Path.Combine(path, filename);

            BinaryReader binaryReader = new BinaryReader(File.Open(filenamePath, FileMode.Open, FileAccess.Read));
            int length = (int)binaryReader.BaseStream.Length;
            binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
            byte[] temp = new byte[length];
            temp = binaryReader.ReadBytes(length);
            binaryReader.Close();

            // Copy the ROM data to the correct location

            int hbytes = (int)Math.Round((double)_hbits / 8);
            int chars = (int)(length / _vbits / hbytes);
            int l = hbytes * _vbits * 256;
            _data = new byte[l];
            int offset = index * _vbits * hbytes;
            temp.CopyTo(_data, offset);

        }

        public void Convert(int horizontal, int vertical)
        {
            _hbits = horizontal;
            _vbits = vertical;

            int hbytes = (int)Math.Round((double)_hbits / 8);

            _data = new byte[256 * hbytes * _vbits];

            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    for (int y = 0; y < _vbits; y++)
                    {
                        byte value = 0;

                        for (int x = 0; x < _hbits; x++)
                        {
                            Color colour = _image.GetPixel(column * _hbits + x, row * _vbits + y);
                            if ((colour.R == 0) && (colour.G ==0) && (colour.B ==0) && (colour.A == 255))
                            {
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

        public void SaveROMFont(string path, string filename)
        {
            string filePath = Path.Combine(path, filename);
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
                        output += "#";
                    }
                    else
                    {
                        output += ".";
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
                            output += "#";
                        }
                        else
                        {
                            output += ".";
                        }
                    }
                    Console.WriteLine(output);
                }
            }
        }

        #endregion
    }
}

