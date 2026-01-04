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
    public class MonochromeGraphicsMode : GraphicsMode, IStorage, IMode
    {
        #region Fields

        ColorPalette _colourPalette;

        #endregion
        #region Constructors

        public MonochromeGraphicsMode(int width, int height) : base(width, height)
        {
            _memory = new byte[(int)(0.5 +_width / 8.0) * _height];
            _hbits = 1;
        }

        public MonochromeGraphicsMode(int width, int height, int scale) : base(width, height)
        {
            _memory = new byte[(int)(0.5 + _width / 8.0) * _height];
            _scale = scale;
            _hbits = 1;
        }

        public MonochromeGraphicsMode(int width, int height, int scale, int aspect) : base(width, height)
        {
            _memory = new byte[(int)(0.5 + _width / 8.0) * _height];
            _scale = scale;
            _aspect = aspect;
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
            _memory = new byte[(int)(0.5 + _width / 8.0) * _height];
            for (int i = 0; i < _memory.Length; i++)
            {
                _memory[i] = background.ToBit();
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
                _bitmap = new Bitmap(_width * hscale, _height * vscale, PixelFormat.Format1bppIndexed);
            }
            BuildColourIndex();
            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _width * hscale, _height * vscale), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = (int)(_bitmap.Width * _bitmap.Height / 8.0);
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
				throw new NotImplementedException();
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
            	_bitmap = new Bitmap(_width * hscale, _height * vscale, PixelFormat.Format1bppIndexed);
			}
            BuildColourIndex();
            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _width * hscale, _height * vscale), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width * _bitmap.Height / 8;
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
				throw new NotImplementedException();
			}

            // Copy the 256 bit values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, size);
            _bitmap.UnlockBits(bmpCanvas);
        }

        #endregion
        #region Private

        private void BuildColourIndex()
        {
            using (var tempBitmap = new Bitmap(1, 1, PixelFormat.Format1bppIndexed))
            {
                _colourPalette = tempBitmap.Palette;
            }
            _colourPalette.Entries[0] = Color.FromArgb(0, 0, 0);        // Black
            _colourPalette.Entries[1] = Color.FromArgb(255, 255, 255);  // White

        }

        #endregion
    }
}