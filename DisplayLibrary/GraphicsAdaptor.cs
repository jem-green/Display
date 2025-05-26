using System;

using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DisplayLibrary
{
    public class GraphicsAdaptor : Adaptor
    {
        #region Fields

        protected int _scale = 1;
        protected int _aspect = 1;
        protected Bitmap _bitmap;

        public enum ConsoleColor : byte
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

        public GraphicsAdaptor(int width, int height) : base(width, height)
        {
            _mode = Mode.graphic;
        }

        #endregion
        #region Properties

        //public ConsoleColor ForegroundColor
        //{
        //    set
        //    {
        //        _foreground = value;
        //    }
        //}

        //public ConsoleColor BackgroundColor
        //{
        //    set
        //    {
        //        _background = value;
        //    }
        //}

        #endregion
        #region Methods

        #endregion
        #region Private

        #endregion
    }
}