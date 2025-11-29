using System;

using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DisplayLibrary
{
    public abstract class GraphicsMode : Storage, IStorage, IMode
    {
        #region Fields

        protected SolidColour _foreground;
        protected SolidColour _background;
        protected int _scale = 1;
        protected int _aspect = 1;
        protected Bitmap _bitmap;

        #endregion
        #region Constructors

        public GraphicsMode(int width, int height) : base(width, height)
        {
            _background = new SolidColour(0, 0, 0);
            _foreground = new SolidColour(255, 255, 255);
            _type = Type.graphic;
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