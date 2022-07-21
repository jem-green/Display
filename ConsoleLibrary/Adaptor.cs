using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace ConsoleLibrary
{
    public class Adaptor : IAdaptor
    {
        #region Fields

        protected int _width = 0;
        protected int _height = 0;
        protected byte[] _memory;
        protected int _scale;
        protected int _aspect;
        protected Mode _mode;

        public enum Mode
        {
            text = 1,
            graphic = 2
        }

        #endregion
        #region Constructors

        public Adaptor(int width, int height, int scale, int aspect)
        {
            _width = width;
            _height = height;
            _scale = scale;
            _aspect = aspect;
            _memory = new byte[_width * _height * 2];
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
                return (_scale);
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
		
		public int Scale
        {
            get
            {
                return (_scale);
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

        public virtual Bitmap Paint()
        {
            return (null);
        }

        #endregion
        #region Private

        #endregion
    }
}
