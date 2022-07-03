using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace ConsoleLibrary
{
    public class Adaptor
    {
        #region Fields

        protected int _width = 0;
        protected int _height = 0;
        protected byte[] _memory;
        protected RasterFont _font;
        protected int _scale;
        protected int _aspect;
        protected ConsoleColor _foreground;
        protected ConsoleColor _background;

        public enum ConsoleColor: byte
        {
            Black = 0,
            DarkBlue = 1,
            DarkGreen = 2,
            DarkCyan = 3,
            DarkRed = 4,
            DarkMagenta = 5,
            DarkYellow = 6,
            Gray = 7,
            DarkGray = 8,
            Blue = 9,
            Green = 10,
            Cyan = 11,
            Red = 12,
            Magenta = 13,
            Yellow = 14,
            White = 15
        }

        public enum Mode
        {
            text = 1,
            graphic = 2
        }

        #endregion
        #region Constructors

        public Adaptor(int width, int height)
        {
            _width = width;
            _height = height;
            _scale = 1;
            _memory = new byte[_width * _height * 2];
        }

        public Adaptor(int width, int height, int scale)
        {
            _width = width;
            _height = height;
            _scale = scale;
            _aspect = 1;
            _memory = new byte[_width * _height * 2];
        }

        public Adaptor(int width, int height, int scale, int aspect)
        {
            _width = width;
            _height = height;
            _scale = scale;
            _aspect = aspect;
            _memory = new byte[_width * _height * 2];
        }

        #endregion
        #region Properties

        public int Aspect
        {
            set
            {
                _aspect = value;
            }
            get
            {
                return (_scale);
            }
        }

        public ConsoleColor ForegroundColor
        {
            set
            {
                _foreground = value;
            }
        }

        public ConsoleColor BackgroundColor
        {
            set
            {
                _background = value;
            }
        }

        public int Width
        {
            set
            {
                _width = value;
            }
            get
            {
                return (_width);
            }
        }

        public int Height
        {
            set
            {
                _height = value;
            }
            get
            {
                return (_height);
            }
        }
		
		public int Scale
        {
            get
            {
                return (_scale);
            }
        }

        public byte[] Memory
        {
            set
            {
                _memory = value;
            }
            get
            {
                return (_memory);
            }
        }

        #endregion
        #region Methods

        #endregion
        #region Private

        #endregion
    }
}
