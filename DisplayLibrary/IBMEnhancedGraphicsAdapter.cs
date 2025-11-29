using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayLibrary
{
    internal class IBMEnhancedGraphicsAdapter
    {
        #region Fields

        private DisplayMode _displayMode = DisplayMode.t40x25c16;
        private List<KeyValuePair<int,IStorage>> _modes = new List<KeyValuePair<int, IStorage>>();
        private IStorage _mode;

        public enum DisplayMode : int
        {
            t40x25c16 = 0,
            t80x25c16 = 1,
            g160x100c16 = 2,
            g320x200c4 = 3,
            g640x200c2 = 4,

            t80x25c16a = 5,
            t80x43c16 = 6,
            g640x350c16 = 7,
            g640x350c2 = 8, 
            g640x200c16 = 9,
            g320x200c16 = 10
        }

        // Need some way to set the number of colours
        // at the mode level

        #endregion
        #region Constructors
        public IBMEnhancedGraphicsAdapter()
        {
            /*
             * EGA supports:
             *
             * Graphics modes:
             *
             * 640 × 350 in 16 colors (from a 6 bit palette of 64 colors), pixel aspect ratio of 1:1.37.
             * 640 × 350 in 2 colors, pixel aspect ratio of 1:1.37.
             * 640 × 200 in 16 colors, pixel aspect ratio of 1:2.4.
             * 320 × 200 in 16 colors, pixel aspect ratio of 1:1.2.
             *
             * Text modes:
             *
             * 40 × 25 in 16 colors, with 8 × 8 pixel font (effective resolution of 320 × 200)
             * 80 × 25 in 16 colors, with 8 × 8 pixel font (effective resolution of 640 × 200)
             * 80 × 25 in 16 colors, with 8 × 14 pixel font (effective resolution of 640 × 350)
             * 80 × 43 in 16 colors, with 8 × 8 pixel font (effective resolution of 640 × 344)
             * 
             */

            // Could predefine all the modes here

            KeyValuePair<int, IStorage> mode;
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.t40x25c16, new VibrantTextMode(40, 25));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.t80x25c16, new VibrantTextMode(80, 25));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.t80x25c16, new VibrantTextMode(80, 25));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.t80x43c16, new VibrantTextMode(80, 43));
            _modes.Add(mode);


            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.g640x350c16, new VibrantGraphicsMode(640, 350));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.g640x350c2, new MonochromeGraphicsMode(640, 200));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.g640x200c16, new VibrantGraphicsMode(640, 200));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IStorage>((int)DisplayMode.g320x200c16, new VibrantGraphicsMode(320, 200));
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
                    return new VibrantTextMode(40, 25);
                case DisplayMode.t80x25c16:
                    return new VibrantTextMode(80, 25);
                case DisplayMode.t80x25c16a:
                    return new VibrantTextMode(80, 25);
                case DisplayMode.t80x43c16:
                    return new VibrantTextMode(80, 43);
                case DisplayMode.g640x350c16:
                    return new VibrantGraphicsMode(640, 350);
                case DisplayMode.g640x350c2:
                    return new MonochromeGraphicsMode(640, 350);
                case DisplayMode.g640x200c16:
                    return new VibrantGraphicsMode(640, 200);
                case DisplayMode.g320x200c16:
                    return new VibrantGraphicsMode(320, 200);
                default:
                    throw new ArgumentOutOfRangeException(nameof(displayMode), displayMode, null);
            }
        }
        #endregion

    }
}
