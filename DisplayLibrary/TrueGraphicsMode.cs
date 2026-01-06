using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DisplayLibrary
{
    /// <summary>
    /// Support for 24 bit graphics mode
    /// </summary>
    public class TrueGraphicsMode : GraphicsMode, IStorage, IMode
    {
        #region Fields

       
        #endregion
        #region Constructors

        public TrueGraphicsMode(int width, int height) : base(width, height)
        {
            int size = (_width * _height) * 3;
            _memory = new byte[size];
            _hbits = 24;
        }

        public TrueGraphicsMode(int width, int height, int scale) : base(width, height)
        {
            int size = (_width * _height) * 3;
            _memory = new byte[size];
            _scale = scale;
            _hbits = 24;
        }

        public TrueGraphicsMode(int width, int height, int scale, int aspect) : base(width, height)
        {
            int size = (_width * _height) * 3;
            _memory = new byte[size];
            _scale = scale;
            _aspect = aspect;
            _hbits = 24;
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
                _memory[i] = background.Blue;
                _memory[i + 1] = background.Green;
                _memory[i + 2] = background.Red;
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

            _bitmap = new Bitmap(_width * hscale, _height * vscale, PixelFormat.Format24bppRgb);

            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _width * hscale, _height * vscale), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width * _bitmap.Height * 3;
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
                for (int y = y1; y <= y2; y++)
                {
                    for (int x = x1; x <= x2; x++)
                    {
                        int sourceIndex = (y * _width + x) * 3;

                        for (int v = 0; v < vscale; v++)
                        {
                            for (int h = 0; h < hscale; h++)
                            {
                                int destX = x * hscale + h;
                                int destY = y * vscale + v;
                                int destIndex = (destY * _width * hscale + destX) * 3;
                                rgbValues[destIndex] = _memory[sourceIndex];
                                rgbValues[destIndex + 1] = _memory[sourceIndex + 1];
                                rgbValues[destIndex + 2] = _memory[sourceIndex + 2];
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
            	_bitmap = new Bitmap(_width * hscale, _height * vscale, PixelFormat.Format24bppRgb);
			}

            BitmapData bmpCanvas = _bitmap.LockBits(new Rectangle(0, 0, _width * hscale, _height * vscale), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            // Get the address of the first line.

            IntPtr ptr = bmpCanvas.Scan0;

            // Declare an array to hold the bytes of the bitmap.

            int size = _bitmap.Width * _bitmap.Height * 3;
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
                        int sourceIndex = (y * _width + x) * 3;
                        // Cannot get a single byte so need to copy all three

                        for (int v = 0; v < vscale; v++)
                        {
                            for (int h = 0; h < hscale; h++)
                            {
                                int destX = x * hscale + h;
                                int destY = y * vscale + v;

                                int destIndex = (destY * _width * hscale + destX) * 3;

                                rgbValues[destIndex] = _memory[sourceIndex];
                                rgbValues[destIndex + 1] = _memory[sourceIndex + 1];
                                rgbValues[destIndex + 2] = _memory[sourceIndex + 2];
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