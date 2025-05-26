using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DisplayLibrary
{
    public class MonochromeTextMode : TextMode, IStorage, IMode
    {
        #region Fields
            
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

        #endregion
        #region Methods

        public override void Clear()
        {
            Clear('\0');
        }

        public void Clear(char character)
        {
            _memory = new byte[_width * _height];
            for (int i = 0; i < _memory.Length; i++)
            {
                _memory[i] = (byte)character;
            }
        }

        public override void Clear(Colour colour)
        {
            Clear('\0');
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

            byte character = _memory[column + row * columns];

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
                            rgbValues[pos] = _foreground.ToByte();
                        }
                        else
                        {
                            for (int i = 0; i < vscale; i++)
                            {
                                for (int j = 0; j < hscale; j++)
                                {
                                    int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits * hscale + column * hbits * hscale + c * hscale + j;
                                    rgbValues[pos] = _foreground.ToByte();
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                        {
                            int pos = (row * vbits + r) * columns * hbits + column * hbits + c;
                            rgbValues[pos] = _background.ToByte();
                        }
                        else
                        {
                            for (int i = 0; i < vscale; i++)
                            {
                                for (int j = 0; j < hscale; j++)
                                {
                                    int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits * hscale + column * hbits * hscale + c * hscale + j;
                                    rgbValues[pos] = _background.ToByte();
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
                    byte character = _memory[column + row * columns];

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
                                    rgbValues[pos] = _foreground.ToByte();
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits * hscale + column * hbits * hscale + c * hscale + j;
                                            rgbValues[pos] = _foreground.ToByte();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((hscale == 1) && (vscale == 1) && (_aspect == 1))
                                {
                                    int pos = (row * vbits + r) * columns * hbits + column * hbits + c;
                                    rgbValues[pos] = _background.ToByte();
                                }
                                else
                                {
                                    for (int i = 0; i < vscale; i++)
                                    {
                                        for (int j = 0; j < hscale; j++)
                                        {
                                            int pos = (row * vbits * vscale + r * vscale + i) * columns * hbits * hscale + column * hbits * hscale + c * hscale + j;
                                            rgbValues[pos] = _background.ToByte();
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

    //// Add the following helper class to resolve the CS0103 error.  
    //// This class provides the missing 'IndexedBitmapHelper' functionality.  
    //internal static class IndexedBitmapHelper
    //{
    //    public static void SetPalette(Bitmap bitmap, TextMode.ConsoleColour foreground, TextMode.ConsoleColour background)
    //    {
    //        if (bitmap.PixelFormat != PixelFormat.Format1bppIndexed)
    //        {
    //            throw new ArgumentException("Bitmap must be 1bpp indexed format.", nameof(bitmap));
    //        }

    //        ColorPalette palette = bitmap.Palette;
    //        palette.Entries[0] = MapConsoleColorToColor(background);
    //        palette.Entries[1] = MapConsoleColorToColor(foreground);
    //        bitmap.Palette = palette;
    //    }

    //    private static Color MapConsoleColorToColor(TextMode.ConsoleColour consoleColor)
    //    {
    //        return consoleColor switch
    //        {
    //            TextMode.ConsoleColour.Black => Color.Black,
    //            TextMode.ConsoleColour.DarkBlue => Color.DarkBlue,
    //            TextMode.ConsoleColour.DarkGreen => Color.DarkGreen,
    //            TextMode.ConsoleColour.DarkCyan => Color.DarkCyan,
    //            TextMode.ConsoleColour.DarkRed => Color.DarkRed,
    //            TextMode.ConsoleColour.DarkMagenta => Color.DarkMagenta,
    //            TextMode.ConsoleColour.DarkYellow => Color.Olive,
    //            TextMode.ConsoleColour.Gray => Color.Gray,
    //            TextMode.ConsoleColour.DarkGray => Color.DarkGray,
    //            TextMode.ConsoleColour.Blue => Color.Blue,
    //            TextMode.ConsoleColour.Green => Color.Green,
    //            TextMode.ConsoleColour.Cyan => Color.Cyan,
    //            TextMode.ConsoleColour.Red => Color.Red,
    //            TextMode.ConsoleColour.Magenta => Color.Magenta,
    //            TextMode.ConsoleColour.Yellow => Color.Yellow,
    //            TextMode.ConsoleColour.White => Color.White,
    //            _ => throw new ArgumentOutOfRangeException(nameof(consoleColor), "Invalid console color value.")
    //        };
    //    }
    
}
