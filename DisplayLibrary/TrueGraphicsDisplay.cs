

using System;
using System.Drawing.Imaging;
using static DisplayLibrary.Storage;

namespace DisplayLibrary
{
    /// <summary>
    /// Support for 24-bit graphics mode
    /// </summary>
    public class TrueGraphicsDisplay : TrueGraphicsMode, IGraphic, IMode, IStorage
    {
        #region Fields

        int _x;
        int _y;

        #endregion
        #region Constructors

        public TrueGraphicsDisplay(int width, int height) : base(width, height)
        {
            _x = 0;
            _y = 0;
        }

        public TrueGraphicsDisplay(int width, int height, int scale) : base(width, height)
        {
            _x = 0;
            _y = 0;
            _scale = scale;
        }

        public TrueGraphicsDisplay(int width, int height, int scale, int aspect) : base(width, height)
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
            	int index = (y * _width + x) * 3;
                byte red = _memory[index];
                byte green = _memory[index + 1];
                byte blue = _memory[index + 2];
                SolidColour c = new SolidColour(red, green, blue);
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
                //colour = (r*6/256)*36 + (g*6/256)*6 + (b*6/256)

                int index = (y * _width + x) * 3;
                _memory[index] = colour.Blue;
                _memory[index + 1] = colour.Green;
                _memory[index + 2] = colour.Red;
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