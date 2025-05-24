using System.Drawing;

namespace DisplayLibrary
{
    public abstract class Storage : IStorage
    {
        #region Fields

        protected int _left = 0;
        protected int _top = 0;
        protected int _width = 0;
        protected int _height = 0;
        protected byte[] _memory;
        protected Type _type;

        public enum Type
        {
            text = 1,
            graphic = 2
        }

        #endregion
        #region Constructors

        public Storage(int width, int height)
        {
            _width = width;
            _height = height;
            _memory = new byte[0];
        }

        #endregion
        #region Properties

        public int Height
        {
            set
            {
                _height = value;
            }
            get
            {
                return (_height);
            }
        }

        public int Left
        {
            set
            {
                _left = value;
            }
            get
            {
                return (_left);
            }
        }

        public byte[] Memory
        {
            set
            {
                _memory = value;
            }
            get
            {
                return (_memory);
            }
        }

        public int Top
        {
            set
            {
                _top = value;
            }
            get
            {
                return (_top);
            }
        }

        public int Width
        {
            set
            {
                _width = value;
            }
            get
            {
                return (_width);
            }
        }

        #endregion
        #region Methods


        #endregion
        #region Private

        #endregion
    }
}
