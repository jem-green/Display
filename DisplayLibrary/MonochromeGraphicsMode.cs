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
    /// Support for 1 bit graphics mode
    /// </summary>
    internal class MonochromeGraphicsMode : GraphicsMode, IStorage, IMode
    {
        #region Fields

        //protected int _scale = 1;
        //protected int _aspect = 1;
        //protected Bitmap _bitmap;
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
        #endregion
    }
}