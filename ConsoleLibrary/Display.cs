using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace ConsoleLibrary
{
    public class Display
    {
        #region Fields

        protected int _width = 0;
        protected int _height = 0;
        protected byte[] _memory;
        protected RasterFont _font;
        protected int _scale;

        public enum Mode
        {
            text = 1,
            graphic = 2
        }

        #endregion
        #region Constructors

        public Display(int width, int height)
        {
            _width = width;
            _height = height;
            _scale = 1;
            _memory = new byte[_width * _height];
        }

        public Display(int width, int height, int scale)
        {
            _width = width;
            _height = height;
            _scale = scale;
            _memory = new byte[_width * _height];
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

        public void Clear()
        {
            Clear('\0');
        }

        public void Clear(char character)
        {
            //_buffer = new byte[_width * _height];
            //or
            for (int i = 0; i < _memory.Length; i++)
            {
                _memory[i] = (byte)character;
            }
        }

        public Bitmap Paint()
        {
            // Need to ge

            Bitmap bmp = new Bitmap(_width * _font.Horizontal * _scale, _height * _font.Vertical * _scale, PixelFormat.Format8bppIndexed);

            BitmapData bmpCanvas = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = bmp.Width * bmp.Height;
            byte[] rgbValues = new byte[size];

            int rows = _height;
            int columns = _width;
            int hbits = _font.Horizontal;
            int vbits = _font.Vertical;

            int hbytes = (int)Math.Round((double)hbits / 8);

            // work across character by character

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    byte character = _memory[column + row*columns];
                    for (int r = 0; r < vbits; r++)
                    {
                        byte value = _font.Image[(byte)character * hbytes * vbits + r];
                        for (int c = 0; c < hbits; c++) // columns
                        {
                            byte val = (byte)(value & (128 >> c));

                            if (val != 0)
                            {
                                if (_scale == 1)
                                {
                                    int pos = (row * hbits + r) * columns * vbits + column * vbits + c;
                                    rgbValues[pos] = 255;
                                }
                                else
                                {
                                    for (int i = 0; i < _scale; i++)
                                    {
                                        for (int j = 0; j < _scale; j++)
                                        {
                                            int pos = (row * hbits * _scale + r * _scale + i) * columns * vbits * _scale + column * vbits * _scale + c * _scale + j;
                                            rgbValues[pos] = 255;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        
            // Copy the 256 bit values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, size);

            bmp.UnlockBits(bmpCanvas);

            return (bmp);
        }

        #endregion
        #region Private
        #endregion
    }
}
