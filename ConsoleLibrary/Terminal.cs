using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace ConsoleLibrary
{
    public class Terminal : ColourAdaptor
    {
        #region Fields

        int _x;
        int _y;

        #endregion
        #region Constructors

        public Terminal(int width, int height) : base(width, height)
        {
            _x = 0;
            _y = 0;
        }

        public Terminal(int width, int height, int scale) : base(width,height,scale)
        {
            _x = 0;
            _y = 0;
        }

        public Terminal(int width, int height, int scale, int aspect) : base(width, height, scale, aspect)
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
            Write(character, _foreground, _background);
        }

        public void Write(byte character, ColourAdaptor.ConsoleColor foreground, ColourAdaptor.ConsoleColor background)
        {
            _memory[(_x + _y * _width) * 2] = character;
            _memory[(_x + _y * _width) * 2 + 1] = (byte)(((byte)background << 4) | (byte)foreground) ;

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
                    Generate();
                }
            }
        }

        public void Write(string text)
        {
            Write(text, _foreground, _background);
        }

        public void Write(string text, ColourAdaptor.ConsoleColor foreground, ColourAdaptor.ConsoleColor background)
        {
            char[] chars = text.ToCharArray();
            // Need to do some boundary checks
            for (int i = 0; i < chars.Length; i++)
            {
                _memory[(_x + _y * _width) * 2] = (byte)chars[i];
                _memory[(_x + _y * _width) * 2 + 1] = (byte)(((byte)background << 4) | (byte)foreground);

                // Would have to call a partial generate here
                // but may make sense to only do this at the end

                //PartialGenerate(_x, _y);

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
                        //Generate();
                    }
                }
            }
            Generate();
        }

        public void Scroll()
        {
            Scroll(1);
        }

        public void Scroll(int rows)
        {
            Buffer.BlockCopy(_memory, _width * 2, _memory, 0, _width * (_height - 1) * 2);
            // fill the space
            for (int i = 0; i < _width; i++)
            {
                _memory[(_width * (_height - 1) + i) * 2] = 32;
                _memory[(_width * (_height - 1) + i) * 2 + 1] = (byte)(((byte)_background << 4) | (byte)_foreground);
            }
            _y--;
        }

        #endregion
        #region Private
        #endregion
    }
}
