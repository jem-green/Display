using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DisplayLibrary
{
    /// <summary>
    /// Support for 8-bit graphics mode, 256 colours
    /// </summary>
    public class VibrantGraphicsMode : GraphicsMode, IStorage, IMode
    {
        #region Fields

        ColorPalette _colourPalette;

        #endregion
        #region Constructors

        public VibrantGraphicsMode(int width, int height) : base(width, height)
        {
            _memory = new byte[_width * _height];
            BuildColourIndex();
            _hbits = 8;
        }

        public VibrantGraphicsMode(int width, int height, int scale) : base(width, height)
        {
            _memory = new byte[_width * _height];
            _scale = scale;
            BuildColourIndex();
            _hbits = 8;
        }

        public VibrantGraphicsMode(int width, int height, int scale, int aspect) : base(width, height)
        {
            _memory = new byte[_width * _height];
            _scale = scale;
            _aspect = aspect;
            BuildColourIndex();
            _hbits = 8;
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
            for (int i = 0; i < _memory.Length; i += 3)
            {
                // Note : BGR order for 24bpp RGB
                _memory[i] = background.ToByte();         // 3-2-3 Approximation
            }
        }

        public override void PartialGenerate(int x1, int y1, int x2, int y2)
        {
            // need to know which position has been updated
            // at the moment this is handled by the parent class

            int hscale = _scale * _aspect;
            int vscale = _scale;

            _bitmap = new Bitmap(_width * hscale, _height * vscale, PixelFormat.Format8bppIndexed);
            _bitmap.Palette = _colourPalette;

            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _width * hscale, _height * vscale), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width * _bitmap.Height;
            byte[] rgbValues = new byte[size];

            // Copy the 256 bit values from bitmap

            Marshal.Copy(ptr, rgbValues, 0, rgbValues.Length);

            if ((_scale == 1) && (_aspect == 1))
            {

                // Copy the memory to the rgbValues array

                _memory.CopyTo(rgbValues, 0);
            }
            else
            {

                // Need to scale the memory to the rgbValues array
                for (int y = y1; y < y2; y++)
                {
                    for (int x = x1; x < x2; x++)
                    {
                        byte colour = _memory[y * _width + x];
                        for (int v = 0; v < vscale; v++)
                        {
                            for (int h = 0; h < hscale; h++)
                            {
                                int destX = x * hscale + h;
                                int destY = y * vscale + v;
                                int destIndex = (destY * _width * hscale + destX);
                                rgbValues[destIndex] = colour;
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

            if (_bitmap is null)
            {
                _bitmap = new Bitmap(_width * hscale, _height * vscale, PixelFormat.Format8bppIndexed);
            }
            _bitmap.Palette = _colourPalette;

            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _width * hscale, _height * vscale), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width * _bitmap.Height;
            byte[] rgbValues = new byte[size];

            // Copy the 256 bit values from bitmap

            Marshal.Copy(ptr, rgbValues, 0, rgbValues.Length);

            if ((_scale == 1) && (_aspect == 1))
            {

                // Copy the memory to the rgbValues array

                _memory.CopyTo(rgbValues, 0);
            }
            else
            {
                // Need to scale the memory to the rgbValues array
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        byte colour = _memory[y * _width + x];
                        for (int v = 0; v < vscale; v++)
                        {
                            for (int h = 0; h < hscale; h++)
                            {
                                int destX = x * hscale + h;
                                int destY = y * vscale + v;
                                int destIndex = (destY * _width * hscale + destX);
                                rgbValues[destIndex] = colour;
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
            // build 8-bit colour palette
            // based on 3-3-2

            using (var tempBitmap = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                _colourPalette = tempBitmap.Palette;
            }

            int index = 0;

            for (int r = 0; r < 8; r++)             // 3 bits → 0–7
            {
                for (int g = 0; g < 8; g++)         // 3 bits → 0–7
                {
                    for (int b = 0; b < 4; b++)     // 2 bits → 0–3
                    {
                        // Scale to 0–255 range
                        int red = (r * 255) / 7;
                        int green = (g * 255) / 7;
                        int blue = (b * 255) / 3;

                        _colourPalette.Entries[index++] = Color.FromArgb(red, green, blue);
                    }
                }
            }
        }

        #endregion
    }
}