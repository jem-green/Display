using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace DisplayLibrary
{
    public class TextMode : DisplayMode
    {
        #region Fields

        protected RasterFont _font;
        protected ConsoleColour _foreground;
        protected ConsoleColour _background;

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

        public ConsoleColour ForegroundColor
        {
            set
            {
                _foreground = value;
            }
        }

        public ConsoleColour BackgroundColor
        {
            set
            {
                _background = value;
            }
        }

        #endregion
        #region Methods

        #endregion
        #region Private

        #endregion
    }
}
