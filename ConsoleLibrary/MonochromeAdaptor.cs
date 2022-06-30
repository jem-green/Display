using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLibrary
{
    public class MonochromeAdaptor : Adaptor
    {
        #region Fields

        #endregion
        #region Constructors

        public MonochromeAdaptor(int width, int height) : base(width, height)
        {
            _width = width;
            _height = height;
            _scale = 1;
            _memory = new byte[_width * _height];
        }

        public MonochromeAdaptor(int width, int height, int scale) : base(width, height, scale)
        {
            _width = width;
            _height = height;
            _scale = scale;
            _memory = new byte[_width * _height];
        }
        #endregion
        #region Properties

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

            Bitmap bitmap = new Bitmap(_width * _font.Horizontal * _scale, _height * _font.Vertical * _scale, PixelFormat.Format8bppIndexed);
            BitmapData bmpCanvas = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = bitmap.Width * bitmap.Height;
            byte[] rgbValues = new byte[size];
            // might be quicker to fill the array with background in one go

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
                    byte character = _memory[column + row * columns];
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
                                    rgbValues[pos] = (byte)_foreground;
                                }
                                else
                                {
                                    for (int i = 0; i < _scale; i++)
                                    {
                                        for (int j = 0; j < _scale; j++)
                                        {
                                            int pos = (row * hbits * _scale + r * _scale + i) * columns * vbits * _scale + column * vbits * _scale + c * _scale + j;
                                            rgbValues[pos] = (byte)_foreground;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (_scale == 1)
                                {
                                    int pos = (row * hbits + r) * columns * vbits + column * vbits + c;
                                    rgbValues[pos] = (byte)_background;
                                }
                                else
                                {
                                    for (int i = 0; i < _scale; i++)
                                    {
                                        for (int j = 0; j < _scale; j++)
                                        {
                                            int pos = (row * hbits * _scale + r * _scale + i) * columns * vbits * _scale + column * vbits * _scale + c * _scale + j;
                                            rgbValues[pos] = (byte)_background;
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
            bitmap.UnlockBits(bmpCanvas);
            return (bitmap);
        }

        #endregion
        #region Private
        #endregion
    }
}
