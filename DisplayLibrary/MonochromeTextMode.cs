using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DisplayLibrary
{
    /// <summary>
    /// Support for 1 bit text mode, 2 colours from a palette of 16 colours
    /// </summary>

    public class MonochromeTextMode : TextMode, IStorage, IMode
    {
        #region Fields

        ColorPalette _colourPalette;

        #endregion
        #region Constructors

        public MonochromeTextMode(int width, int height) : base(width, height)
        {
            _memory = new byte[_width * _height];
            BuildColourIndex();
        }

        public MonochromeTextMode(int width, int height, int scale) : base(width, height)
        {
            _memory = new byte[_width * _height];
            _scale = scale;
            BuildColourIndex();
        }

        public MonochromeTextMode(int width, int height, int scale, int aspect) : base(width, height)
        {
            _memory = new byte[_width * _height];
            _scale = scale;
            _aspect = aspect;
            BuildColourIndex();
        }

        #endregion
        #region Properties

        #endregion
        #region Methods

        public override void Clear()
        {
            Clear('\0');
        }

        public override void Clear(IColour colour)
        {
            Clear('\0');
        }

        public void Clear(char character)
        {
            for (int i = 0; i < _memory.Length; i++)
            {
                _memory[i] = (byte)character;
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
            _bitmap.Palette = _colourPalette;

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

            // work across character by character

            for (int r = row; r < row + height; r++)
            {
                for (int c = column; c < column + width; c++)
                {
                    byte character = _memory[column + row * columns];

                    for (int v = 0; v < vbits; v++) // row
                    {
                        byte value = _font.Data[(byte)character * hbytes * vbits + v];

                        for (int h = 0; h < hbits; h++) // columns
                        {
                            byte val = (byte)(value & (128 >> h));

                            if (val != 0)
                            {
                                if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                                {
                                    int pos = (r * vbits + v) * columns * hbits + c * hbits + h;
                                    rgbValues[pos] = _foreground.ToNybble();
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (r * vbits * vscale + v * vscale + i) * columns * hbits * hscale + c * hbits * hscale + h * hscale + j;
                                            rgbValues[pos] = _foreground.ToNybble();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                                {
                                    int pos = (r * vbits + v) * columns * hbits + c * hbits + h;
                                    rgbValues[pos] = _background.ToNybble();
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (r * vbits * vscale + v * vscale + i) * columns * hbits * hscale + c * hbits * hscale + h * hscale + j;
                                            rgbValues[pos] = _background.ToNybble();
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

        public override void Generate()
        {
            int hscale = _scale * _aspect;
            int vscale = _scale;

            _bitmap = new Bitmap(_width * _font.Horizontal * hscale, _height * _font.Vertical * vscale, PixelFormat.Format8bppIndexed);
            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            _bitmap.Palette = _colourPalette;

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

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    byte character = _memory[c + r * columns];

                    for (int v = 0; v < vbits; v++)
                    {
                        byte value = _font.Data[(byte)character * hbytes * vbits + v];

                        for (int h = 0; h < hbits; h++) // columns
                        {
                            byte val = (byte)(value & (128 >> h));

                            if (val != 0)
                            {
                                if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                                {
                                    int pos = (r * vbits + v) * columns * hbits + c * hbits + h;
                                    rgbValues[pos] = _foreground.ToNybble();
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (r * vbits * vscale + v * vscale + i) * columns * hbits * hscale + c * hbits * hscale + h * hscale + j;
                                            rgbValues[pos] = _foreground.ToNybble();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                                {
                                    int pos = (r * vbits + v) * columns * hbits + c * hbits + h;
                                    rgbValues[pos] = _background.ToNybble();
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (r * vbits * vscale + v * vscale + i) * columns * hbits * hscale + c * hbits * hscale + h * hscale + j;
                                            rgbValues[pos] = _background.ToNybble();
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

        private void BuildColourIndex()
        {
            using (var tempBitmap = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                _colourPalette = tempBitmap.Palette;
            }
            _colourPalette.Entries[0] = Color.FromArgb(0, 0, 0);        // Black
            _colourPalette.Entries[1] = Color.FromArgb(0, 0, 170);      // Blue
            _colourPalette.Entries[2] = Color.FromArgb(0, 170, 0);      // Green
            _colourPalette.Entries[3] = Color.FromArgb(0, 170, 170);    // Cyan
            _colourPalette.Entries[4] = Color.FromArgb(170, 0, 0);      // Red
            _colourPalette.Entries[5] = Color.FromArgb(170, 0, 170);    // Magenta
            _colourPalette.Entries[6] = Color.FromArgb(170, 85, 0);     // Brown
            _colourPalette.Entries[7] = Color.FromArgb(170, 170, 170);  // Light Gray
            _colourPalette.Entries[8] = Color.FromArgb(85, 85, 85);     // Dark Gray
            _colourPalette.Entries[9] = Color.FromArgb(85, 85, 255);    // Light Blue
            _colourPalette.Entries[10] = Color.FromArgb(85, 255, 85);   // Light Green
            _colourPalette.Entries[11] = Color.FromArgb(85, 255, 255);  // Light Cyan
            _colourPalette.Entries[12] = Color.FromArgb(255, 85, 85);   // Light Red
            _colourPalette.Entries[13] = Color.FromArgb(255, 85, 255);  // Light Magenta
            _colourPalette.Entries[14] = Color.FromArgb(255, 255, 85);  // Yellow
            _colourPalette.Entries[15] = Color.FromArgb(255, 255, 255); // White
        }

        #endregion
    }
}
