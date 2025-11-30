using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace DisplayLibrary
{
    /// <summary>
    /// Support for 1 bit graphics mode, 2 colours from a palette of 16 colours
    /// </summary>
    internal class MonochromeGraphicsMode : GraphicsMode, IStorage, IMode
    {
        #region Fields

        ColorPalette _colourPalette;

        #endregion
        #region Constructors

        public MonochromeGraphicsMode(int width, int height) : base(width, height)
        {
            _memory = new byte[_width * _height];
        }

        public MonochromeGraphicsMode(int width, int height, int scale) : base(width, height)
        {
            _memory = new byte[_width * _height];
            _scale = scale;
        }

        public MonochromeGraphicsMode(int width, int height, int scale, int aspect) : base(width, height)
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
            Clear(_background);
        }
		
		public override void Clear(IColour background)
        {
            _memory = new byte[_width * _height];
            for (int i = 0; i < _memory.Length; i++)
            {
                _memory[i] = background.ToNybble();
            }
        }

        public override void PartialGenerate(int x1, int y1, int x2, int y2)
        {
            // need to know which position has been updated
            // at the moment this is handled by the parent class

            int hscale = _scale * _aspect;
            int vscale = _scale;
            if (_bitmap is null)
            {
                _bitmap = new Bitmap(Width * hscale, Height * vscale, PixelFormat.Format8bppIndexed);
            }
            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width * _bitmap.Height;
            byte[] rgbValues = new byte[size];

            // Copy the 256 bit values from bitmap

            Marshal.Copy(ptr, rgbValues, 0, rgbValues.Length);

            // Copy the memory to the rgbValues array

            _memory.CopyTo(rgbValues, 0);

            // Copy the 256 bit values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, size);
            _bitmap.UnlockBits(bmpCanvas);
        }
		
        public override void Generate()
        {
            int hscale = _scale * _aspect;
            int vscale = _scale;

            _bitmap = new Bitmap(Width * hscale, Height * vscale, PixelFormat.Format8bppIndexed);

            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width * _bitmap.Height;
            byte[] rgbValues = new byte[size];

            // Copy the 256 bit values from bitmap

            Marshal.Copy(ptr, rgbValues, 0, rgbValues.Length);

            // Copy the memory to the rgbValues array

            _memory.CopyTo(rgbValues, 0);

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