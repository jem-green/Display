using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DisplayLibrary
{
    public class ColourTextMode : TextMode, IStorage, IMode
    {
        #region Fields

        #endregion
        #region Constructors

        public ColourTextMode(int width, int height) : base(width, height)
        {
            _memory = new byte[_width * _height * 2];
        }

        public ColourTextMode(int width, int height, int scale) : base(width, height)
        {
            _memory = new byte[_width * _height * 2];
            _scale = scale;
        }

        public ColourTextMode(int width, int height, int scale, int aspect) : base(width, height)
        {
            _memory = new byte[_width * _height * 2];
            _scale = scale;
            _aspect = aspect;
         }

        #endregion
        #region Properties

        #endregion
        #region Methods

        public override void Clear()
        {
            Clear(_background);
        }

        public override void Clear(IColour background)
        {
            Clear('\0', new Solid(255, 255, 255), background);
        }

        public void Clear(char character, IColour foreground, IColour background)
        {
            _memory = new byte[_width * _height * 2];
            for (int i = 0; i < _memory.Length - 1; i += 2)
            {
                _memory[i] = (byte)character;
                _memory[i + 1] = (byte)((byte)(background.ToNybble() << 4) | (byte)foreground.ToNybble());
            }
        }

        public override void PartialGenerate(int column, int row, int width, int height)
        {
            // need to know which position has been updated
            // at the moment this is handled by the parent class
  
            int hscale = _scale * _aspect;
            int vscale = _scale;

            if (_bitmap is null)
            {
                _bitmap = new Bitmap(_width * _font.Horizontal * hscale, _height * _font.Vertical * vscale, PixelFormat.Format8bppIndexed);
            }

            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width * _bitmap.Height;
            byte[] rgbValues = new byte[size];

            // Copy the 256 bit values from bitmap

            Marshal.Copy(ptr, rgbValues, 0, rgbValues.Length);

            int rows = _height;
            int columns = _width;
            int hbits = _font.Horizontal;
            int vbits = _font.Vertical;

            int hbytes = (int)Math.Round((double)hbits / 8);

            byte character = _memory[(column + row * columns) * 2];
            byte colour = _memory[(column + row * columns) * 2 + 1];
            byte foreground = (byte)(colour & 15);
            byte background = (byte)((colour >> 4) & 15);

            for (int r = 0; r < vbits; r++)
            {
                byte value = _font.Image[(byte)character * hbytes * vbits + r];

                for (int c = 0; c < hbits; c++) // columns
                {
                    byte val = (byte)(value & (128 >> c));

                    if (val != 0)
                    {
                        if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                        {
                            int pos = (row * vbits + r) * columns * hbits + column * hbits + c;
                            rgbValues[pos] = (byte)foreground;
                        }
                        else
                        {
                            for (int i = 0; i < vscale; i++)
                            {
                                for (int j = 0; j < hscale; j++)
                                {
                                    int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits * hscale + column * hbits * hscale + c * hscale + j;
                                    rgbValues[pos] = (byte)foreground;
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                        {
                            int pos = (row * vbits + r) * columns * hbits + column * hbits + c;
                            rgbValues[pos] = (byte)background;
                        }
                        else
                        {
                            for (int i = 0; i < vscale; i++)
                            {
                                for (int j = 0; j < hscale; j++)
                                {
                                    int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits * hscale + column * hbits * hscale + c * hscale + j;
                                    rgbValues[pos] = (byte)background;
                                }
                            }
                        }
                    }
                }
            }

            // Copy the 256 bit values back to the bitmap

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, size);
            _bitmap.UnlockBits(bmpCanvas);
            //return (_bitmap);
        }

        public override void Generate()
        {
            int hscale = _scale * _aspect;
            int vscale = _scale;

            _bitmap = new Bitmap(_width * _font.Horizontal * hscale, _height * _font.Vertical * vscale, PixelFormat.Format8bppIndexed);
            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width * _bitmap.Height;
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
                    byte character = _memory[(column + row * columns) * 2];
                    byte colour = _memory[(column + row * columns) * 2 + 1];
                    byte foreground = (byte)(colour & 15);
                    byte background = (byte)((colour >> 4) & 15);

                    for (int r = 0; r < vbits; r++)
                    {
                        byte value = _font.Image[(byte)character * hbytes * vbits + r];

                        for (int c = 0; c < hbits; c++) // columns
                        {
                            byte val = (byte)(value & (128 >> c));

                            if (val != 0)
                            {
                                if ((hscale==1) && (vscale == 1) && (_aspect == 1))
                                {
                                    int pos = (row * vbits + r) * columns * hbits + column * hbits + c;
                                    rgbValues[pos] = (byte)foreground;
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits * hscale + column * hbits * hscale + c * hscale + j;
                                            rgbValues[pos] = (byte)foreground;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                                {
                                    int pos = (row * vbits + r) * columns * hbits + column * hbits + c;
                                    rgbValues[pos] = (byte)background;
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits * hscale + column * hbits * hscale + c * hscale + j;
                                            rgbValues[pos] = (byte)background;
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
            _bitmap.UnlockBits(bmpCanvas);
        }

        #endregion
        #region Private
        #endregion
    }
}