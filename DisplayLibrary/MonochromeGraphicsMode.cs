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
    internal class MonochromeGraphicsMode : GraphicsMode, IGraphic, IMode, IStorage
    {
        #region Fields

        protected int _scale = 1;
        protected int _aspect = 1;
        protected Bitmap _bitmap;

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
            Clear(_background);
        }
		
		public void Clear(Colour background)
        {
           Clear(background.R, background.G, background.B);
        }

        public void Clear(byte r, byte g, byte b)
        {
            _memory = new byte[_width * _height];
            byte colour = r;
            g = (byte)((g >> 3) & 0b00011100);
            colour = (byte)(colour | g);
            b = (byte)((b >> 6) & 0b00000011);
            colour = (byte)(colour | b);
            for (int i = 0; i < _memory.Length; i++)
            {
                _memory[i] = colour;
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

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            int index = y * _width / 8 + x;
            int bit = x % 8;
            byte colour = (byte)((r << 16) | (g << 8) | b);
            if (colour == 0)
            {
                _memory[index] = (byte)(_memory[index] & (1 << bit));
            }
            else
            {
                _memory[index] = (byte)(_memory[index] | (1 << bit));
            }
        }

        public void GetPixel(int x, int y, out byte r, out byte g, out byte b)
        {
            int index = y * _width + x;
            byte colour = _memory[index];
            r = (byte)((colour >> 16) & 0xFF);
            g = (byte)((colour >> 8) & 0xFF);
            b = (byte)(colour & 0xFF);
        }


        #endregion
        #region Private
        #endregion
    }
}