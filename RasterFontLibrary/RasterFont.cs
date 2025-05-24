using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RasterFontLibrary
{
    public class RasterFont
    {
        #region Fields

        byte _chars;
        byte _hbits;
        byte _vbits;
        byte[] _image;

        Character[] _characters;

        public struct Character
        {
            private byte _index;
            private byte _width;
            private int _offset;
            private byte[] _image;

            public Character()
            {
                _index = 0;
                _width = 0;
                _offset = 0;
                _image = new byte[0];
            }

            public byte Index
            {
                get { return _index; }
                set { _index = value; }
            }

            public byte Width
            {
                get { return _width; }
                set { _width = value; }
            }

            public int Offset
            {
                get { return _offset; }
                set { _offset = value; }
            }

            public byte[] Image
            {
                get { return _image; }
                set { _image = value; }
            }

            public void AddImage(BitMap image)
            {
                // Convert the bit array to a byte array
                // Get the number of bytes needed to store the image
                int length = (7 + image.Width) / 8 * image.Height;

                _image = new byte[length];
                for (int i = 0; i < image.Height; i++)
                {
                    // Get the byte for the current row
                    byte row = 0;
                    for (int j = 0; j < image.Width; j++)
                    {
                        if (image.BitArray[i * image.Width + j])
                        {
                            row |= (byte)(1 << j);
                        }
                    }
                    // Store the byte in the image array
                    _image[i] = row;
                }
            }

            public void AddImage(byte[] image)
            {
                _image = new byte[image.Length];
                for (int i = 0; i < image.Length; i++)
                {
                    _image[i] = image[i];
                }
            }
        }

        public struct BitMap
        {
            private bool[] _bitArray;
            private byte _width = 8;
            private byte _height = 8;

            public BitMap()
            {
                _width = 8;
                _height = 8;
                _bitArray = new bool[0];
            }

            public BitMap(int width, int height)
            {
                _width = (byte)width;
                _height = (byte)height;
                //int length = width * height;
                //_bitArray = new bool[length];
                _bitArray = new bool[0];
            }

            public bool[] BitArray
            {
                get
                {
                    return (_bitArray);
                }
                set
                {
                    _bitArray = value;
                }
            }

            public byte Width
            {
                get
                {
                    return (_width);
                }
                set
                {
                    _width = value;
                }
            }

            public byte Height
            {
                get
                {
                    return (_height);
                }
                set
                {
                    _height = value;
                }
            }

            public void AddRow(byte row)
            {
                if (_width <= 8)
                {
                    Array.Resize(ref _bitArray, _bitArray.Length + _width);
                    for (int i = 0; i < _width; i++)
                    {
                        _bitArray[_bitArray.Length - _width + i] = (row & (1 << i)) != 0;
                    }
                }
            }

            public void AddRow(byte[] row)
            {
                if (_width <= 8 * row.Length)
                {
                    Array.Resize(ref _bitArray, _bitArray.Length + row.Length * _width);
                    for (int i = 0; i < row.Length; i++)
                    {
                        for (int j = 0; j < _width; j++)
                        {
                            _bitArray[_bitArray.Length - row.Length * _width + i * _width + j] = (row[i] & (1 << j)) != 0;
                        }
                    }
                }
            }
        }   

        public RasterFont()
        {
            // Assume ASCII, and set-up the lookup tab
            //_characters = new Character[255]; 
            _chars = 0;
            _hbits = 0;
            _vbits = 0;
        }


        #endregion
        #region Properties

        public byte Horizontal
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

        public byte Vertical
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

        public byte[] Image
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

        public Character[] Characters
        {
            set
            {
                _characters = value;
            }
            get
            {
                return (_characters);
            }
        }

        #endregion
        #region Methods

        public void Load(string path, string filePath)
        {
            if (path.Length == 0)
            {
                path = ".";
            }
            string fileNamePath = Path.Combine(path, filePath);
            BinaryReader binaryReader = new BinaryReader(File.Open(fileNamePath, FileMode.Open, FileAccess.Read));

            binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);

            _hbits = binaryReader.ReadByte();
            _vbits = binaryReader.ReadByte();
            int hbytes = (int)Math.Round((double)_hbits / 8);
            int length = hbytes * _vbits * 255;
            _image = new byte[length];
            _image = binaryReader.ReadBytes(length);

            binaryReader.Close();
            binaryReader.Dispose();

        }

        public void Save(string path, string filePath)
        {
            if (path.Length == 0)
            {
                path = ".";
            }
            string fileNamePath = Path.Combine(path, filePath);
            BinaryWriter binaryWriter = new BinaryWriter(File.Open(fileNamePath, FileMode.Open, FileAccess.Read));

            binaryWriter.BaseStream.Seek(0, SeekOrigin.Begin);

            binaryWriter.Write(_hbits);
            binaryWriter.Write(_vbits);
            int hbytes = (int)Math.Round((double)_hbits / 8);
            int length = hbytes * _vbits * 255;
            binaryWriter.Write(_image, 0, length);
            binaryWriter.Close();
            binaryWriter.Dispose();
        }

        public void Add(Character c)
        {
            // Add a character to the font

            if (_chars < 255)
            {
                Array.Resize(ref _characters, _chars + 1);

                _characters[_chars].Index = c.Index;
                _characters[_chars].Width = c.Width;
                _chars++;
            }
        }

        #endregion
        #region Private
        #endregion
    }
}
