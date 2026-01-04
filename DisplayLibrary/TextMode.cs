using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using DisplayLibrary;

namespace DisplayLibrary
{
    public abstract class TextMode : Mode, IStorage, IMode
    {
        #region Fields

        protected ROMFont _font;

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

        public ROMFont Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }


        #endregion
        #region Methods

        public override abstract void Clear();

        public override abstract void Clear(IColour background);

        public override abstract void PartialGenerate(int x1, int y1, int x2, int y2);

        public override abstract void Generate();

        #endregion
        #region Private

        #endregion
    }
}
