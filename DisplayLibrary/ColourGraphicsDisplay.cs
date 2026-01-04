

using System;
using System.Drawing.Imaging;
using static DisplayLibrary.Storage;
using static System.Windows.Forms.DataFormats;

namespace DisplayLibrary
{
    /// <summary>
    /// Support for 2-bit graphics mode, 4 colours from a palette of 16 colours
    /// </summary>
    public class ColourGraphicsDisplay : ColourGraphicsMode, IStorage, IMode, IGraphic
    {
        #region Fields

        int _x;
        int _y;

        #endregion
        #region Constructors

        public ColourGraphicsDisplay(int width, int height) : base(width, height)
        {
            _x = 0;
            _y = 0;
        }

        public ColourGraphicsDisplay(int width, int height, int scale) : base(width, height)
        {
            _x = 0;
            _y = 0;
            _scale = scale;
        }

        public ColourGraphicsDisplay(int width, int height, int scale, int aspect) : base(width, height)
        {
            _x = 0;
            _y = 0;
            _scale = scale;
            _aspect = aspect;
        }

        #endregion
        #region Properties

        public int X
        {
            set
            {
                _x = value;
            }
            get
            {
                return (_x);
            }
        }

        public int Y
        {
            set
            {
                _y = value;
            }
            get
            {
                return (_y);
            }
        }

        #endregion
        #region Methods

        public void Set(int x, int y)
        {
            // need to do some boundary checks
            if ((x > _width) || (y > _height))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                _x = x;
                _y = y;
            }
        }

        public IColour Fetch()
        {
            return Fetch(_x, _y);
        }

        public IColour Fetch(int x, int y)
        {
            
            // need to do some boundary checks
            if ((x > _width) || (y > _height))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
            	int index = y * _width + x;
            	byte colour = _memory[index];

                if (x % 4 == 1)
                {
                    colour = (byte)((colour & 0xC0) >> 6);
                }
                else if (x % 4 == 2)
                {
                    colour = (byte)((colour & 0x30) >> 4);
                }
                else if (x % 4 == 3)
                {
                    colour = (byte)((colour & 0x0C) >> 2);
                }
                else
                {
                    colour = (byte)((colour & 0x03));
                }

                IColour c = new SolidColour().From2Bit(colour);
                return(c);
            }
        }

        public void Put(IColour colour)
        {
            Put(_x, _y, colour);
        }

        public void Put(int x, int y, IColour colour)
        {
            // need to do some boundary checks

            if ((x > _width) || (y > _height))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                int index = (int)((y * _width + x ) / 4.0);
                byte newColour;
                if (x % 4 == 1)
                {
                    byte existing = _memory[index];
                    newColour = (byte)((existing & 0xC0) | (colour.To2Bit() << 6));
                }
                else if (x % 4 == 2)
                {
                    // even pixel
                    byte existing = _memory[index];
                    newColour = (byte)((existing & 0x30) | (colour.To2Bit() << 4));
                }
                else if (x % 4 == 3)
                {
                    // even pixel
                    byte existing = _memory[index];
                    newColour = (byte)((existing & 0x0C) | (colour.To2Bit() << 2));
                }
                else
                {
                    // odd pixel
                    byte existing = _memory[index];
                    newColour = (byte)((existing & 0x03) | (colour.To2Bit()));
                }
                _memory[index] = newColour;
            }
        }

        public void Save(string path, string filename)
        {
            // Save the bitmap to a file

            int pos = filename.LastIndexOf(".");
            string extension = ".png";
            if (pos > 0)
            {
                extension = filename.Substring(pos, filename.Length - pos);
                filename = filename.Substring(0, pos);
            }

            if (extension == ".bmp")
                Save(path, filename, ImageFormat.Bmp);
            else if (extension == ".jpg")
                Save(path, filename, ImageFormat.Jpeg);
            else if (extension == ".png")
                Save(path, filename, ImageFormat.Png);
        }

        public void Save(string path, string filename, ImageFormat format)
        {
            int pos = filename.LastIndexOf(".");
            string extension = String.Empty;
            if (pos > 0)
            {
                filename = filename.Substring(0, pos);
            }

            if (format == ImageFormat.Bmp)
            {
                extension = ".bmp";
            }
            else if (format == ImageFormat.Jpeg)
            {
                extension = ".jpg";
            }
            else if (format == ImageFormat.Png)
            {
                extension = ".png";
            }
            else
            {
                throw new NotImplementedException();
            }

            // Save the bitmap to a file
            string filenamePath = System.IO.Path.Combine(path, filename + extension);
            _bitmap.Save(filenamePath, format);
        }

        #endregion
        #region Private
        #endregion
    }
}