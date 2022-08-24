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

        protected int _left = 0;
        protected int _top = 0;
        protected int _width = 0;
        protected int _height = 0;
        protected byte[] _memory;
        protected Mode _mode;

        public enum Mode
        {
            text = 1,
            graphic = 2
        }

        #endregion
        #region Constructors

        public Adaptor(int width, int height)
        {
            _width = width;
            _height = height;
            _memory = new byte[_width * _height * 2];
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

        #endregion
        #region Private

        #endregion
    }
}
