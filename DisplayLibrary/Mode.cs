using System.Drawing;

namespace DisplayLibrary
{
    public class Mode : IMode
    {
        #region Fields

        protected IColour _foreground;
        protected IColour _background;
        protected int _scale = 1;
        protected int _aspect = 1;
        protected Bitmap _bitmap;

        #endregion
        #region Constructors

        public Mode(int width, int height)
        {
            _background = new SolidColour(0, 0, 0);
            _foreground = new SolidColour(255, 255, 255);
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

        public void Clear()
        {
            // Implementation of Clear method without parameters
        }

        public void Clear(IColour background)
        {
            // Implementation of Clear method with background colour
        }

        public void PartialGenerate(int x1, int y1, int x2, int y2)
        {
            // Implementation of PartialGenerate method
        }

        public void Generate()
        {
            // Implementation of Generate method
        }

        #endregion
    }
}
