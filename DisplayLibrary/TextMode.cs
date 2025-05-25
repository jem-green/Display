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

        protected RasterFont _font;
        protected Colour _foreground;
        protected Colour _background;
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

        public Colour ForegroundColour
        {
            set
            {
                _foreground = value;
            }
        }

        public Colour BackgroundColour
        {
            set
            {
                _background = value;
            }
        }

        #endregion
        #region Methods

        public abstract void Clear();

        public abstract void Clear(Colour background);

        public abstract void PartialGenerate(int x1, int y1, int x2, int y2);

        public abstract void Generate();

        #endregion

        #region Private

        #endregion
    }
}
