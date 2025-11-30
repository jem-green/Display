

using System;
using System.Drawing.Imaging;
using static DisplayLibrary.Storage;

namespace DisplayLibrary
{
    /// <summary>
    /// Support for 2-bit graphics mode, 4 colours from a palette of 16 colours
    /// </summary>
    public class ColourGraphicsDisplay : EnhancedGraphicsMode, IStorage, IMode, IGraphic
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
		
        public IColour Read()
        {
            return Read(_x, _y);
        }

        public IColour Read(int x, int y)
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

                byte red = 0;
                byte green = 0;
                byte blue = 0;

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

                // Extract RGB bits directly
                red = (byte)((colour & 0x03) != 0 ? 0xFF : 0);
                green = (byte)((colour & 0x02) != 0 ? 0xFF : 0);
                blue = (byte)((colour & 0x01) != 0 ? 0xFF : 0);

                SolidColour c = new SolidColour(red,green,blue);
                return(c);
            }
        }

        public void Write(IColour colour)
        {
            Write(_x, _y, colour);
        }

        public void Write(int x, int y, IColour colour)
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
            Save(path, filename, ImageFormat.Png);
        }

        public void Save(string path, string filename, ImageFormat format)
        {
            // Save the bitmap to a file
            string filenamePath = System.IO.Path.Combine(path, filename);
            _bitmap.Save(filenamePath, format);
        }

        #endregion
        #region Private
        #endregion
    }
}