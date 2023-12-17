using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayLibrary
{
    internal class ColorGraphicsAdaptor
    {
        // This is sligtly different as it supprts both
        // text and graphics just need to work out how you
        // switch modes and provide a signle interface

        #region Fields

        private AdaptorMode _mode = AdaptorMode.t40x25c16;
        private List<IAdaptor> adaptors = new List<IAdaptor>();

        public enum AdaptorMode : int
        {
            t40x25g16 = 0,
            t40x25c16 = 1,
            t80x25g16 = 2,
            t80x25c16 = 3,
            g160x100c16 = 4,
            g320x200c4 = 5,
            g640x200c2 = 6,
        }

        #endregion

        public ColorGraphicsAdaptor()
        {
            /*
             * Graphics modes:
             * 
             * 160×100 in 16 colors, chosen from a 16-color palette, utilizing a specific configuration of the 80x25 text mode.
             * 320×200 in 4 colors, chosen from 3 fixed palettes, with high- and low-intensity variants, with color 1 chosen from a 16-color palette.
             * 640×200 in 2 colors, one black, one chosen from a 16-color palette
             * 
             * 40×25 with 8×8 pixel font (effective resolution of 320×200)
             * 80×25 with 8×8 pixel font (effective resolution of 640×200)
             * 
             */

            adaptors.Add(new ColourAdaptor(40, 25));

        }

        public AdaptorMode Mode
        {
            set
            {
                _mode = value;
            }
            get
            { 
                return _mode;
            }
        }

        public byte[] Memory
        {
            set
            {
                adaptors[(int)_mode].Memory = value;
            }
            get
            {
                return (adaptors[(int)_mode].Memory);
            }
        }

    }
}
