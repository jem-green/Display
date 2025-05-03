using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayLibrary
{
    internal class IBMColorGraphicsAdaptor
    {
        // This is slightly different as it supports both
        // text and graphics just need to work out how you
        // switch modes and provide a single interface

        #region Fields

        private DisplayMode _displayMode = DisplayMode.t40x25c16;
        private List<KeyValuePair<int,IMode>> _modes = new List<KeyValuePair<int, IMode>>();
        private IMode _mode;

        public enum DisplayMode : int
        {
            t40x25c16 = 0,
            t80x25c16 = 1,
            g160x100c16 = 2,
            g320x200c4 = 3,
            g640x200c2 = 4,
        }

        #endregion
        #region Constructors
        public IBMColorGraphicsAdaptor()
        {
            /* CGA supports:
             * 
             * Graphics modes:
             * 
             * 160×100 in 16 colors, chosen from a 16-color palette, utilizing a specific configuration of the 80x25 text mode.
             * 320×200 in 4 colors, chosen from 3 fixed palettes, with high- and low-intensity variants, with color 1 chosen from a 16-color palette.
             * 640×200 in 2 colors, one black, one chosen from a 16-color palette
             * 
             * Text modes:
             * 
             * 40×25 with 8×8 pixel font (effective resolution of 320×200)
             * 80×25 with 8×8 pixel font (effective resolution of 640×200)
             * 
             */

            // Could predefine all the modes here

            KeyValuePair<int, IMode> mode = new KeyValuePair<int, IMode>((int)DisplayMode.t40x25c16, new TextMode(40, 25));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IMode>((int)DisplayMode.t80x25c16, new TextMode(80, 25));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IMode>((int)DisplayMode.g160x100c16, new GraphicsMode(160, 100));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IMode>((int)DisplayMode.g320x200c4, new GraphicsMode(320, 200));
            _modes.Add(mode);
            mode = new KeyValuePair<int, IMode>((int)DisplayMode.g640x200c2, new GraphicsMode(640, 200));
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

        IMode GetAdaptor(DisplayMode displayMode)
        {
            switch (displayMode)
            {
                case DisplayMode.t40x25c16:
                    return new TextMode(40, 25);
                case DisplayMode.t80x25c16:
                    return new TextMode(80, 25);
                case DisplayMode.g160x100c16:
                    return new GraphicsMode(160, 100);
                case DisplayMode.g320x200c4:
                    return new GraphicsMode(320, 200);
                case DisplayMode.g640x200c2:
                    return new GraphicsMode(640, 200);
                default:
                    throw new ArgumentOutOfRangeException(nameof(displayMode), displayMode, null);
            }
        }
        #endregion

    }
}
