using System;

using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DisplayLibrary
{
    public abstract class GraphicsMode : Mode, IStorage, IMode
    {
        #region Fields

        protected byte _hbits = 24;

        #endregion
        #region Constructors

        public GraphicsMode(int width, int height) : base(width, height)
        {
            _foreground = new SolidColour(255, 255, 255);
            _background = new SolidColour(0, 0, 0);
            _type = Type.graphic;
            _bitmap = null;
        }

        #endregion
        #region Properties

        public byte Bits
        {
            get
            {
                return (_hbits);
            }
        }

        #endregion
        #region Methods

        #endregion
        #region Private

        #endregion
    }
}