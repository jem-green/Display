

using System;
using System.Drawing.Imaging;
using static DisplayLibrary.Storage;

namespace DisplayLibrary
{
    public class VibrantGraphicsDisplay : VibrantGraphicsMode, IGraphic, IMode, IStorage
    {
        #region Fields

        int _x;
        int _y;

        #endregion
        #region Constructors

        public VibrantGraphicsDisplay(int width, int height) : base(width, height)
        {
            _x = 0;
            _y = 0;
        }

        public VibrantGraphicsDisplay(int width, int height, int scale) : base(width, height)
        {
            _x = 0;
            _y = 0;
            _scale = scale;
        }

        public VibrantGraphicsDisplay(int width, int height, int scale, int aspect) : base(width, height)
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
            	int index = (y * _width + x) * 3;
                byte r = _memory[index];
                byte g = _memory[index + 1];
                byte b = _memory[index + 2];
                Solid c = new Solid(r, g, b);
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
                //color = (r*6/256)*36 + (g*6/256)*6 + (b*6/256)

                int index = (y * _width + x) * 3;
                _memory[index] = colour.Blue;
                _memory[index + 1] = colour.Green;
                _memory[index + 2] = colour.Red;
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