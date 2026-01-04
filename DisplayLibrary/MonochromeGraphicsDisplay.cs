

using System;
using System.Drawing.Imaging;
using static DisplayLibrary.Storage;

namespace DisplayLibrary
{
    /// <summary>
    /// Support for 1-bit graphics mode, 2 colours
    /// </summary>
    public class MonochromeGraphicsDisplay : MonochromeGraphicsMode, IStorage, IMode, IGraphic
    {
        #region Fields

        int _x;
        int _y;

        #endregion
        #region Constructors

        public MonochromeGraphicsDisplay(int width, int height) : base(width, height)
        {
            _x = 0;
            _y = 0;
        }

        public MonochromeGraphicsDisplay(int width, int height, int scale) : base(width, height)
        {
            _x = 0;
            _y = 0;
            _scale = scale;
        }

        public MonochromeGraphicsDisplay(int width, int height, int scale, int aspect) : base(width, height)
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
                IColour c = new SolidColour().FromBit(colour);
                return (c);
            }
        }

        public void Put(IColour colour)
        {
            Put(_x, _y, colour);
        }

        public void Put(int x, int y, IColour colour)
        {
            // 8-bit colour from wiki
            // http://en.wikipedia.org/wiki/8-bit_color
            // 3 bits red, 3 bits green, 2 bits blue

            // need to do some boundary checks

            if ((x > _width) || (y > _height))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                int index = (int)((y * _width + x) / 8.0);
                byte pixelBit = (byte)(7 - ((x) % 8));
                byte mask = (byte)(1 << pixelBit);

                if (colour.ToBit() != 0)
                {
                    _memory[index] |= mask; // set the bit
                }
                else
                {
                    _memory[index] &= (byte)~mask; // clear the bit
                }
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