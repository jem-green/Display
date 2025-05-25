

using System;
using System.Drawing.Imaging;
using static DisplayLibrary.Storage;

namespace DisplayLibrary
{
    public class MonochromeGraphicsDisplay : ColourGraphicsMode, IStorage, IGraphic, IDisplay
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
		
        public Colour Read()
        {
            return Read(_x, _y);
        }

        public Colour Read(int x, int y)
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
                Colour c = new Colour((byte)((colour >> 16) & 0xFF), (byte)((colour >> 8) & 0xFF), (byte)(colour & 0xFF));
                return(c);
            }
        }

        public void Write(Colour colour)
        {
            Write(colour.R, colour.G, colour.B);
        }

        public void Write(byte r, byte g, byte b)
        {
            Write(_x, _y, r,g,b);
        }

        public void Write(int x, int y, Colour colour)
        {
            Write(x, y, colour.R, colour.G, colour.B);
        }

        public void Write(int x, int y, byte r, byte g, byte b)
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
                //color = (r*6/256)*36 + (g*6/256)*6 + (b*6/256)

                int index = y * _width + x;
                r = (byte)(r & 0b11100000);
                byte colour = r;
                g = (byte)((g >> 3) & 0b00011100);
                colour= (byte)(colour | g);
                b = (byte)((b >> 6) & 0b00000011);
                colour = (byte)(colour | b);
                _memory[index] = colour;
            }
        }

        public void Save(string filename)
        {
            // Save the bitmap to a file
            _bitmap.Save(filename, ImageFormat.Png);
        }

        public void Save(string filename, ImageFormat format)
        {
            // Save the bitmap to a file
            _bitmap.Save(filename, format);
        }

        #endregion
        #region Private
        #endregion
    }
}