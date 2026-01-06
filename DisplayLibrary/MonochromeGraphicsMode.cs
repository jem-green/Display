using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DisplayLibrary
{
    /// <summary>
    /// Support for 1 bit graphics mode, 2 colours from a palette of 16 colours
    /// </summary>
    public class MonochromeGraphicsMode : GraphicsMode, IStorage, IMode
    {
        #region Fields

        private ColorPalette _colourPalette;

        #endregion
        #region Constructors

        public MonochromeGraphicsMode(int width, int height) : base(width, height)
        {
            // not sure what to do with odd widths assume we round up
            int size = ((_width + 7)>>3) * _height; // divide by 8 rounded up
            _memory = new byte[size];
            BuildColourIndex();
            _hbits = 1;
        }

        public MonochromeGraphicsMode(int width, int height, int scale) : base(width, height)
        {
            // not sure what to do with odd widths assume we round up
            int size = ((_width + 7) >> 3) * _height; // divide by 8 rounded up
            _memory = new byte[size];
            _scale = scale;
            BuildColourIndex();
            _hbits = 1;
        }

        public MonochromeGraphicsMode(int width, int height, int scale, int aspect) : base(width, height)
        {
            // not sure what to do with odd widths assume we round up
            int size = ((_width + 7) >> 3) * _height; // divide by 8 rounded up
            _memory = new byte[size];
            _scale = scale;
            _aspect = aspect;
            BuildColourIndex();
            _hbits = 1;
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
            byte colour = background.ToBit();
            colour = (byte)(colour == 0 ? 0x00 : 0xFF);
            for (int i = 0; i < _memory.Length; i++)
            {
                _memory[i] = colour;
            }
        }

        public override void PartialGenerate(int x1, int y1, int x2, int y2)
        {
            // need to know which position has been updated
            // at the moment this is handled by the parent class

            if ((x2 > _width - 1) || (y2 > _height - 1) || (x1 < 0) || (y1 < 0) || (x2 < x1) || (y2 < y1))
            {
                throw new IndexOutOfRangeException();
            }

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

            // Need to scale the memory to the rgbValues array

            for (int y = y1; y <= y2; y++)
            {
                int rowBase = y * ((_width + 7) >> 3);   // precompute row offset

                for (int x = x1; x <= x2; x++)
                {
                    int sourceIndex = rowBase + (x >> 3); // faster than /8
                    byte colour = _memory[sourceIndex];

                    // extract bit
                    colour = (byte)((colour >> (7 - (x & 7))) & 0x01);

                    // replicate into scaled buffer
                    int destXBase = x * hscale;
                    int destYBase = y * vscale;

                    for (int v = 0; v < vscale; v++)
                    {
                        int destRow = (destYBase + v) * (_width * hscale);

                        for (int h = 0; h < hscale; h++)
                        {
							int destIndex = destRow + destXBase + h;
                            rgbValues[destIndex] = colour;
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

            // Need to scale the memory to the rgbValues array

            for (int y = 0; y < _height; y++)
            {
                int rowBase = y * ((_width + 7) >> 3);   // precompute row offset

                for (int x = 0; x < _width; x++)
                {
                    int sourceIndex = rowBase + (x >> 3); // faster than /8
                    byte colour = _memory[sourceIndex];

                    // extract bit
                    colour = (byte)((colour >> (7 - (x & 7))) & 0x01);

                    // replicate into scaled buffer
                    int destXBase = x * hscale;
                    int destYBase = y * vscale;

                    for (int v = 0; v < vscale; v++)
                    {
                        int destRow = (destYBase + v) * (_width * hscale);
                        for (int h = 0; h < hscale; h++)
                        {
                            int destIndex = destRow + destXBase + h;
                            rgbValues[destIndex] = colour;
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
            _colourPalette.Entries[1] = Color.FromArgb(255, 255, 255);  // White

        }

        #endregion
    }
}