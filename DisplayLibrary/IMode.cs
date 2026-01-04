using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace DisplayLibrary
{
    public interface IMode
    {
        #region Fields

        #endregion
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

        public IColour ForegroundColour
        {
            set;
        }

        public IColour BackgroundColour
        {
            set;
        }

        public void Clear();

        public void Clear(IColour background);

        public void PartialGenerate(int x1, int y1, int x2, int y2);

        public void Generate();

        #endregion

    }
}
