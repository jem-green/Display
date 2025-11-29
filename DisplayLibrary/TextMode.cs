using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using DisplayLibrary;

namespace DisplayLibrary
{
    public abstract class TextMode : Storage, IStorage, IMode
    {
        #region Fields

        protected ROMFont _font;
        protected IColour _foreground;
        protected IColour _background;
        protected int _scale = 1;
        protected int _aspect = 1;
        protected Bitmap _bitmap;

        public enum ConsoleColour : byte
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

        #endregion
        #region Constructors

        public TextMode(int width, int height) : base(width,height)
        {
            _type = Type.text;
            _foreground = new SolidColour(255, 255, 255);
            _background = new SolidColour(0, 0, 0);
            _bitmap = null;
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
                return (_aspect);
            }
        }

        public Bitmap Bitmap
        {
            get
            {
                return (_bitmap);
            }
        }

        public int Scale
        {
            get
            {
                return (_scale);
            }
            set
            {
                _scale = value;
            }
        }

        public IColour ForegroundColour
        {
            set
            {
                _foreground = value;
            }
        }

        public IColour BackgroundColour
        {
            set
            {
                _background = value;
            }
        }

        #endregion
        #region Methods

        public abstract void Clear();

        public abstract void Clear(IColour background);

        public abstract void PartialGenerate(int x1, int y1, int x2, int y2);

        public abstract void Generate();

        #endregion
        #region Private

        #endregion
    }
}
