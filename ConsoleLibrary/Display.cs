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

            Bitmap bmp = new Bitmap(_width * _font.Horizontal, _height * _font.Vertical, PixelFormat.Format8bppIndexed);

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
                            byte check = (byte)(value & (128 >> c));
                            if (check != 0)
                            {
                                rgbValues[(row * hbits + r) * columns * vbits + column * vbits + c] = 255;
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
