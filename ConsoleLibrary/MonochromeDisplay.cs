using System;

namespace ConsoleLibrary
{
    public class MonochromeDisplay : MonochromeAdaptor
    {
        #region Fields

        int _x;
        int _y;
        

        #endregion
        #region Constructors
		
		public MonochromeDisplay(int width, int height) : base(width, height)
        {
            _x = 0;
            _y = 0;
        }

        public MonochromeDisplay(int width, int height, int scale) : base(width,height,scale)
        {
            _x = 0;
            _y = 0;
        }

        public MonochromeDisplay(int width, int height, int scale, int aspect) : base(width, height, scale, aspect)
        {
            _x = 0;
            _y = 0;
        }

        #endregion
        #region Properties
        public RasterFont Font
        {
            set
            {
                _font = value;
            }
        }

        public int Row
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

        public int Column
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

        public void Set(int column, int row)
        {
            // need to do some boundary checks
            if ((column > _width) || (row > _height))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                _x = column;
                _y = row;
            }
        }

        public void Write(byte character)
        {
            _memory[_x + _y * _width] = character;
            // Would have to call a partial generate here

            PartialGenerate(_x, _y);
            
            _x++;
            if (_x >= _width)
            {
                _x = 0;
                _y++;
                if (_y >= _height)
                {
                    _y = _height;
                    // the display needs to scoll at the point.
                    Scroll();
                }
            }
        }

        public void Write(string text)
        {
            char[] chars = text.ToCharArray();
            // Need to do some boundary checks
            for (int i = 0; i < chars.Length; i++)
            {
                _memory[_x + _y * _width] = (byte)chars[i];
                // Would have to call a partial generate here

                PartialGenerate(_x, _y);

                _x++;
                if (_x >= _width)
                {
                    _x = 0;
                    _y++;
                    if (_y >= _height)
                    {
                        _y = _height;
                        // the display needs to scoll at the point.
                        Scroll();          
                    }
                }
            }
        }

        public void Scroll()
        {
            Scroll(1);
        }

        public void Scroll(int rows)
        {
            Buffer.BlockCopy(_memory, _width, _memory, 0, _width * (_height - 1));
            // fill the space
            for (int i = 0; i < _width; i++)
            {
                _memory[_width * (_height - 1) + i] = 32;
            }
            _y--;
        }

        #endregion
        #region Private
        #endregion
    }
}
