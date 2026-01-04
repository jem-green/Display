

using System;
using System.Drawing.Imaging;
using static DisplayLibrary.Storage;

namespace DisplayLibrary
{
    /// <summary>
    /// Support for 4-bit graphics mode, 16 colours
    /// </summary>
    public class EnhancedGraphicsDisplay : EnhancedGraphicsMode, IStorage, IMode, IGraphic
    {
        #region Fields

        int _x;
        int _y;

        #endregion
        #region Constructors

        public EnhancedGraphicsDisplay(int width, int height) : base(width, height)
        {
            _x = 0;
            _y = 0;
        }

        public EnhancedGraphicsDisplay(int width, int height, int scale) : base(width, height)
        {
            _x = 0;
            _y = 0;
            _scale = scale;
        }

        public EnhancedGraphicsDisplay(int width, int height, int scale, int aspect) : base(width, height)
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
                int index = (y * _width + x) / 2;
                byte colour = _memory[index];

                if (x % 2 == 1)
                {
                    // even pixel
                    colour = (byte)((colour & 0xF0) >> 4);
                }
                else
                {
                    // odd pixel
                    colour = (byte)(colour & 0x0F);
                }
                IColour c = new SolidColour().FromNybble(colour);
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
                int index = y * _width / 2 + x / 2;
                byte newColour;
                if (x % 2 == 1)
                {
                    // even pixel
                    byte existing = _memory[index];
                    newColour = (byte)((existing & 0x0F) | (colour.ToNybble() << 4 & 0xF0));
                }
                else
                {
                    // odd pixel
                    byte existing = _memory[index];
                    newColour = (byte)((existing & 0xF0) | (colour.ToNybble() & 0x0F));
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