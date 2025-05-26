using System.Drawing;

namespace DisplayLibrary
{
    internal interface IMode
    {
        #region Methods

        public int Aspect
        {
            set;
            get;
        }

        public Bitmap Bitmap
        {
            get;
        }

        public int Scale
        {
            get;
            set;
        }

        public void Clear();

        public void Clear(Colour background);

        public void PartialGenerate(int x1, int y1, int x2, int y2);

        public void Generate();

        #endregion

    }
}
