using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLibrary
{
    public class MonochromeAdaptor : TextAdaptor
    {
        #region Fields

        #endregion
        #region Constructors

        public MonochromeAdaptor(int width, int height) : base(width, height)
        {
            _memory = new byte[_width * _height];
        }

        public MonochromeAdaptor(int width, int height, int scale) : base(width, height, scale)
        {
            _memory = new byte[_width * _height];
        }

        public MonochromeAdaptor(int width, int height, int scale, int aspect) : base(width, height, scale, aspect)
        {
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

        public override Bitmap Generate()
        {
            // Need to ge

            Bitmap bitmap = new Bitmap(_width * _font.Horizontal * _scale * _aspect, _height * _font.Vertical * _scale, PixelFormat.Format8bppIndexed);
            BitmapData bmpCanvas = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = bitmap.Width * bitmap.Height;
            byte[] rgbValues = new byte[size];
            // might be quicker to fill the array with background in one go

            int hbits = _font.Horizontal;
            int vbits = _font.Vertical;

            int hscale = _scale * _aspect;
            int vscale = _scale;

            int hbytes = (int)Math.Round((double)hbits / 8);

            // work across character by character

            for (int row = _top; row < _height; row++)
            {
                for (int column = _left; column < _width; column++)
                {
                    byte character = _memory[column + row * _width];
                    for (int r = 0; r < vbits; r++)
                    {
                        byte value = _font.Image[(byte)character * hbytes * vbits + r];
                        for (int c = 0; c < hbits; c++) // columns
                        {
                            byte val = (byte)(value & (128 >> c));

                            if (val != 0)
                            {
                                if ((_scale == 1) && (_aspect == 1))
                                {
                                    int pos = (row * vbits + r) * _width * hbits + column * hbits + c;
                                    rgbValues[pos] = (byte)_foreground;
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (row * vbits * vscale + r * vscale + i) * _width * hbits * hscale + column * hbits * hscale + c * _scale + j;
                                            rgbValues[pos] = (byte)_foreground;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((_scale == 1) && (_aspect == 1))
                                {
                                    int pos = (row * vbits + r) * _width * hbits + column * hbits + c;
                                    rgbValues[pos] = (byte)_background;
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (row * vbits * vscale + r * vscale + i) * _width * hbits * hscale + column * hbits * hscale + c * _scale + j;
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
