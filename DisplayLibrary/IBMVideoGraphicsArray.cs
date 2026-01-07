using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayLibrary
{
    internal class IBMVideoGraphicsArray
    {
        // This is slightly different as it supports both
        // text and graphics just need to work out how you
        // switch modes and provide a single interface

        #region Fields

        private DisplayMode _displayMode = DisplayMode.t40x25c16;
        private List<KeyValuePair<int, IStorage>> _modes = new List<KeyValuePair<int, IStorage>>();
        private IStorage _mode;

        public enum DisplayMode : int
        {
            t40x25c16 = 0,
            t80x25c16 = 1,
            t80x43c16 = 2,
            t80x50c16 = 3,
            g320x200c4 = 4,
            g320x200c16 = 5,
            g320x200c256 = 6,
            g640x200c2 = 7,
            g640x200c16 = 8,
            g640x350c2 = 9,
            g640x350c16 = 10,
            g640x480c2 = 11,
            g640x480c16 = 12
        }

        #endregion
        #region Constructors
        public IBMVideoGraphicsArray()
        {
            /* VGA supports:
             * 
             * Graphics modes:
             * 
             * 320 × 200 in 4 or 16 colours (CGA/EGA compatibility).
             * 320 × 200 in 256 colours (Mode 13h).
             * 640 × 200 and 640 × 350 in 16 colours or monochrome (CGA/EGA compatibility).
             * 640 × 480 in 16 colours or monochrome.
             * 
             * Text modes:
             * 
             * 80 × 25 in 16 colours, rendered with a 9 × 16 pixel font, with an effective resolution of 720 × 400.
             * 40 × 25 in 16 colours, with a 9 × 16 font, with an effective resolution of 360 × 400.
             * 80 × 50 in 16 colours, with an 8 × 8 font grid, with an effective resolution of 640 × 344 or 640 × 400 pixels.
             * 
             */

            // Could predefine all the modes here

            KeyValuePair<int, IStorage> mode;
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.t40x25c16, new EnhancedTextMode(40, 25));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.t80x25c16, new EnhancedTextMode(80, 25));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.g320x200c4, new EnhancedGraphicsMode(320, 200));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.g640x200c2, new EnhancedGraphicsMode(640, 200));
            _modes.Add(mode);

            // or for CGA

            _mode = GetAdaptor(_displayMode);

            // Need to consider what fonts to use for the text modes
            // I suspect as the depended on the adaptor manufacture then this is in the
            // the derived class (BIOS)

        }
        #endregion
        #region Properties


        /// <summary>
        /// The mode of the adaptor. This is used to switch between text and graphics modes.
        /// </summary>
        public DisplayMode Mode
        {
            set
            {
                _displayMode = value;
                // Could create the new adaptor here as CGA only has 1 memory buffer
                _mode = GetAdaptor(_displayMode);
            }
            get
            {
                return _displayMode;
            }
        }

        public byte[] Memory
        {
            set
            {
                //_adaptors[(int)_mode].Value.Memory = value;
                _mode.Memory = value;
            }
            get
            {
                //return (_adaptors[(int)_mode].Value.Memory);
                return (_mode.Memory);
            }
        }

        #endregion
        #region Private

        IStorage GetAdaptor(DisplayMode displayMode)
        {
            switch (displayMode)
            {
                case DisplayMode.t40x25c16:
                    {
                        IStorage mode = new EnhancedTextMode(40, 25);
                        return (mode);
                    }
                case DisplayMode.t80x25c16:
                    return new EnhancedTextMode(80, 25);
                case DisplayMode.g320x200c4:
                    return new EnhancedGraphicsMode(320, 200);
                case DisplayMode.g640x200c2:
                    return new EnhancedGraphicsMode(640, 200);
                default:
                    throw new ArgumentOutOfRangeException(nameof(displayMode), displayMode, null);
            }
        }
        #endregion

    }
}
