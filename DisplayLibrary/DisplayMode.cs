namespace DisplayLibrary
{
    public class DisplayMode : IMode
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

        public DisplayMode(int width, int height)
        {
            _width = width;
            _height = height;
        }

        #endregion
        #region Properties

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

        #endregion
        #region Methods

        public virtual void Generate()
        {
        }

        public virtual void PartialGenerate(int x1, int y1, int x2, int y2)
        {
        }

        #endregion
        #region Private

        #endregion
    }
}
