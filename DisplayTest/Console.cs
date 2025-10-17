﻿using System;
using DisplayLibrary;

namespace DisplayTest
{
    public class Console : MonochromeTextMode, IStorage, IText
    {
        #region Fields

        int _x;
        int _y;
        

        #endregion
        #region Constructors
		
		public Console(int width, int height) : base(width, height)
        {
            _x = 0;
            _y = 0;
        }

        public Console(int width, int height, int scale) : base(width,height,scale)
        {
            _x = 0;
            _y = 0;
        }

        public Console(int width, int height, int scale, int aspect) : base(width, height, scale, aspect)
        {
            _x = 0;
            _y = 0;
        }

        #endregion
        #region Properties
        public ROMFont Font
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

        public byte Read(int column, int row)
        {
            // need to do some boundary checks
            if ((column > _width) || (row > _height))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                byte character = _memory[_x + _y * _width];
                return (character);
            }
        }

        public byte Read()
        {
            // need to do some boundary checks
            byte character = _memory[_x + _y * _width];
            return (character);
        }

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

        public void Write(byte character, IColour foreground, IColour background)
        {
            _memory[_x + _y * _width] = character;
            // Would have to call a partial generate here

            PartialGenerate(_x, _y, 1, 1);
            
            _x++;
            if (_x >= _width)
            {
                _x = 0;
                _y++;
                if (_y >= _height)
                {
                    _y = _height;
                    // the display needs to coll at the point.
                    Scroll();
                }
            }
        }

        public void Write(string text)
        {
            Write(text, _foreground, _background);
        }

        public void Write(string text, IColour foreground, IColour background)
        {
            char[] chars = text.ToCharArray();
            // Need to do some boundary checks
            for (int i = 0; i < chars.Length; i++)
            {
                _memory[_x + _y * _width] = (byte)chars[i];
                // Would have to call a partial generate here

                PartialGenerate(_x, _y, 1, 1);

                _x++;
                if (_x >= _width)
                {
                    _x = 0;
                    _y++;
                    if (_y >= _height)
                    {
                        _y = _height;
                        // the display needs to scroll at the point.
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

        public void Save(string path, string filename)
        {
            throw new NotImplementedException("");
        }

        #endregion
        #region Private
        #endregion
    }
}
