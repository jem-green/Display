using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DisplayLibrary
{
    public class MonochromeTextMode : TextMode
    {
        #region Fields

        protected int _scale = 1;
        protected int _aspect = 1;
        protected Bitmap _bitmap;
            
        #endregion
        #region Constructors

        public MonochromeTextMode(int width, int height) : base(width, height)
        {
            _memory = new byte[_width * _height];
        }

        public MonochromeTextMode(int width, int height, int scale) : base(width, height)
        {
            _memory = new byte[_width * _height];
            _scale = scale;
        }

        public MonochromeTextMode(int width, int height, int scale, int aspect) : base(width, height)
        {
            _memory = new byte[_width * _height];
            _scale = scale;
            _aspect = aspect;
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
                return (_aspect);
            }
        }

        public Bitmap Bitmap
        {
            get
            {
                return (_bitmap);
            }
        }

        public int Scale
        {
            get
            {
                return (_scale);
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
            for (int i = 0; i < _memory.Length; i++)
            {
                _memory[i] = (byte)character;
            }
        }

        public Bitmap PartialGenerate(int column, int row)
        {
            // need to know which position has been updated
            // at the moment this is handled by the parent class
  
            int hscale = _scale * _aspect;
            int vscale = _scale;
            if (_bitmap is null)
            {
                _bitmap = new Bitmap(_width * _font.Horizontal * hscale, _height * _font.Vertical * vscale, PixelFormat.Format1bppIndexed);
                IndexedBitmapHelper.SetPalette(_bitmap, _foreground, _background);
            }
            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width / 8 * _bitmap.Height;
            byte[] rgbValues = new byte[size];

            // Copy the 256 bit values from bitmap

            Marshal.Copy(ptr, rgbValues, 0, rgbValues.Length);

            int rows = _height;
            int columns = _width;
            int hbits = _font.Horizontal;
            int vbits = _font.Vertical;

            int hbytes = (int)Math.Round((double)hbits / 8);

            byte character = _memory[column + row * _width];
            for (int r = 0; r < vbits; r++)
            {
                byte value = _font.Image[(byte)character * hbytes * vbits + r];

                for (int c = 0; c < hbits; c++) // columns
                {
                    byte val = (byte)(value & (128 >> c));

                    int pos = (row * vbits + r) * columns * hbytes + column * hbytes;
                    rgbValues[pos] = value;

                    //if (val != 0)
                    //{
                    //    if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                    //    {
                    //        int pos = (row * vbits + r) * columns * hbits + column * hbits + c;
                    //        rgbValues[pos] = (byte)_foreground;
                    //    }
                    //    else
                    //    {
                    //        for (int i = 0; i < vscale; i++)
                    //        {
                    //            for (int j = 0; j < hscale; j++)
                    //            {
                    //                int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits * hscale + column * hbits * hscale + c * hscale + j;
                    //                rgbValues[pos] = (byte)_foreground;
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                    //    {
                    //        int pos = (row * vbits + r) * columns * hbits + column * hbits + c;
                    //        rgbValues[pos] = (byte)_background;
                    //    }
                    //    else
                    //    {
                    //        for (int i = 0; i < vscale; i++)
                    //        {
                    //            for (int j = 0; j < hscale; j++)
                    //            {
                    //                int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits * hscale + column * hbits * hscale + c * hscale + j;
                    //                rgbValues[pos] = (byte)_background;
                    //            }
                    //        }
                    //    }
                    //}
                }
            }


            // Copy the 256 bit values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, size);
            _bitmap.UnlockBits(bmpCanvas);
            return (_bitmap);
        }

        public override void Generate()
        {
            int hscale = _scale * _aspect;
            int vscale = _scale;
            _bitmap = new Bitmap(_width * _font.Horizontal * hscale, _height * _font.Vertical * vscale, PixelFormat.Format1bppIndexed);
            IndexedBitmapHelper.SetPalette(_bitmap, _foreground, _background);
            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width / 8 * _bitmap.Height;
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
                    for (int r = 0; r < vbits; r++) // rows
                    {
                        byte value = _font.Image[(byte)character * hbytes * vbits + r];

                        //int pos = (row * vbits + r) * columns * hbytes + column * hbytes;
                        //rgbValues[pos] = value;

                        for (int c = 0; c < hbits; c++) // columns
                        {
                            byte val = (byte)(value & (128 >> c));

                            if (val != 0)
                            {
                                if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                                {
                                    int pos = (row * vbits + r) * columns * hbits / 8 + column * hbits / 8 + c / 8;
                                    rgbValues[pos] = (byte)(rgbValues[pos] | (128 >> c));
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits / 8 * hscale + column * hbits / 8 * hscale + c / 8 * hscale + j;
                                            rgbValues[pos] = (byte)(rgbValues[pos] | (128 >> c));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                                {
                                    int pos = (row * vbits + r) * columns * hbits / 8 + column * hbits / 8 + c / 8;
                                    //rgbValues[pos] = (byte)(rgbValues[pos] & (255 >> c));
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits / 8 * hscale + column * hbits / 8 * hscale + c / 8 * hscale + j;
                                            rgbValues[pos] = (byte)(rgbValues[pos] & (1 >> c));
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

    // Add the following helper class to resolve the CS0103 error.  
    // This class provides the missing 'IndexedBitmapHelper' functionality.  
    internal static class IndexedBitmapHelper
    {
        public static void SetPalette(Bitmap bitmap, TextMode.ConsoleColor foreground, TextMode.ConsoleColor background)
        {
            if (bitmap.PixelFormat != PixelFormat.Format1bppIndexed)
            {
                throw new ArgumentException("Bitmap must be 1bpp indexed format.", nameof(bitmap));
            }

            ColorPalette palette = bitmap.Palette;
            palette.Entries[0] = MapConsoleColorToColor(background);
            palette.Entries[1] = MapConsoleColorToColor(foreground);
            bitmap.Palette = palette;
        }

        private static Color MapConsoleColorToColor(TextMode.ConsoleColor consoleColor)
        {
            return consoleColor switch
            {
                TextMode.ConsoleColor.Black => Color.Black,
                TextMode.ConsoleColor.DarkBlue => Color.DarkBlue,
                TextMode.ConsoleColor.DarkGreen => Color.DarkGreen,
                TextMode.ConsoleColor.DarkCyan => Color.DarkCyan,
                TextMode.ConsoleColor.DarkRed => Color.DarkRed,
                TextMode.ConsoleColor.DarkMagenta => Color.DarkMagenta,
                TextMode.ConsoleColor.DarkYellow => Color.Olive,
                TextMode.ConsoleColor.Gray => Color.Gray,
                TextMode.ConsoleColor.DarkGray => Color.DarkGray,
                TextMode.ConsoleColor.Blue => Color.Blue,
                TextMode.ConsoleColor.Green => Color.Green,
                TextMode.ConsoleColor.Cyan => Color.Cyan,
                TextMode.ConsoleColor.Red => Color.Red,
                TextMode.ConsoleColor.Magenta => Color.Magenta,
                TextMode.ConsoleColor.Yellow => Color.Yellow,
                TextMode.ConsoleColor.White => Color.White,
                _ => throw new ArgumentOutOfRangeException(nameof(consoleColor), "Invalid console color value.")
            };
        }
    }
}
