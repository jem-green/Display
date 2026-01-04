using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace DisplayLibrary
{
    /// Summary
    /// Monochrome Text Display supporting 16 foreground and 16 background colours, from a palette of 16 colours.
    /// </summary>
    public class MonochromeTextDisplay : MonochromeTextMode, IStorage, IMode, IText
    {
        #region Fields

        int _x; // Column
        int _y; // Row

        #endregion
        #region Constructors

        public MonochromeTextDisplay(int width, int height) : base(width, height)
        {
            _x = 0;
            _y = 0;
        }

        public MonochromeTextDisplay(int width, int height, int scale) : base(width,height,scale)
        {
            _x = 0;
            _y = 0;
        }

        public MonochromeTextDisplay(int width, int height, int scale, int aspect) : base(width, height, scale, aspect)
        {
            _x = 0;
            _y = 0;
        }

        #endregion
        #region Properties

        public int Row
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

        public int Column
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

        #endregion
        #region Methods

       
		
        public byte Fetch()
        {
            // need to do some boundary checks
            byte character = _memory[_x + _y * _width];
            return (character);
        }

        public byte Fetch(int column, int row)
        {
            // need to do some boundary checks
            if ((column > _width) || (row > _height))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
            	byte character = _memory[column + row * _width];
            	return (character);
            }
        }

        // Sets the current cursor position
		
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

        public void Put(byte character)
        {
            Put(_x, _y, character, _foreground, _background);
        }

        public void Put(int column, int row, byte character)
        {
            Put(column, row, character, _foreground, _background);
        }
		
        public void Put(byte character, IColour foreground, IColour background)
        {
            Put(_x, _y, character, _foreground, _background);
        }
		
        public void Put(int column, int row, byte character, IColour foreground, IColour background)
        {
            // Ignore any colour information for monochrome display
			
            Set(column, row);
            _memory[_x + _y * _width] = character;
            // Would have to call a partial generate here

            PartialGenerate(_x, _y, 1, 1);
        }

        public byte Read()
        {
            return (Read(_x,_y));
        }

        public byte Read(int column, int row)
        {
            // need to do some boundary checks
            if ((column > _width) || (row > _height))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
            	byte character = _memory[column + row * _width];
            	return (character);
            }
        }

        public void Write(byte character)
        {
            Write(character, _foreground, _background);
        }

        public void Write(byte character, IColour foreground, IColour background)
        {
            // Ignore any colour information for monochrome display

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
                    // the display needs to scroll at the point.
                    Scroll();
                    Generate();
                }
            }
        }

        public void Write(string text)
        {
            Write(text, _foreground, _background);
        }

        public void Write(string text, IColour foreground, IColour background)
        {
            // Ignore any colour information for monochrome display

            char[] chars = text.ToCharArray();
            // Need to do some boundary checks
            for (int i = 0; i < chars.Length; i++)
            {
                _memory[_x + _y * _width] = (byte)chars[i];
                // Would have to call a partial generate here
                // but may make sense to only do this at the end

                //PartialGenerate(_x, _y, 1, 1);

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
            Generate();
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
            // Save the display to a file
            throw new NotImplementedException("Save method not implemented for MonochromeTextDisplay.");
        }

        #endregion
        #region Private
        #endregion
    }
}
