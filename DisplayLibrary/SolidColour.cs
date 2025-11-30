using System;
using System.Drawing;
using System.Windows.Forms;

namespace DisplayLibrary
{
    public class SolidColour : IEquatable<SolidColour>, IColour
{
        #region Fields

        private byte _red = 0;
        private byte _green = 0;
        private byte _blue = 0;
        private byte _colour2 = 0;      // 2-bit colour
        private byte _colour4 = 0;      // 4-bit colour
        private byte _colour8 = 0;      // 8-bit colour
        private ushort _colour16 = 0;   // 16-bit colour
        private ulong _colour32 = 0;    // 32-bit colour

        #endregion
        #region Constructors

        public SolidColour(string hex)
        {
            byte red = Convert.ToByte(hex.Substring(1, 2), 16);
            byte green = Convert.ToByte(hex.Substring(3, 2), 16);
            byte blue = Convert.ToByte(hex.Substring(5, 2), 16);
            ColourConverter(red, green, blue);
        }

        public SolidColour(byte red, byte green, byte blue)
        {
            ColourConverter(red, green, blue);
        }

        public SolidColour(byte colour) // 
        {
            // Convert from 3-3-2 format 8-bit to RGB
            //
            // 01234567
            // ~+-~+-~+
            //  |  |  |
            //  |  |  +- Blue (2)
            //  |  +- Green (3)
            //  +- Red (3)

            byte red = (byte)((colour >> 5) & 0x07);
            byte green = (byte)((colour >> 2) & 0x07);
            byte blue = (byte)(colour & 0x03);

            _red = (byte)((red * 255) / 7);
            _green = (byte)((green * 255) / 7);
            _blue = (byte)((blue * 255) / 3);

            //*r = (red << 5) | (red << 2) | (red >> 1); // 3→8 bits
            //*g = (green << 5) | (green << 2) | (green >> 1);
            //*b = (blue << 6) | (blue << 4) | (blue << 2) | blue;
        }

        #endregion
        #region Properties

        public byte Red
        {
            get { return _red; }
            set { _red = value; }
        }

        public byte Green
        {
            get { return _green; }
            set { _green = value; }
        }

        public byte Blue
        {
            get { return _blue; }
            set { _blue = value; }
        }

        #endregion
        #region Methods

        public byte To2Bit()
        {
            // Return 1-bit colour
            return (_colour2);
        }

        public byte ToNybble()
        {
            // Return 4-bit colour
            return (_colour4);
        }

        public byte ToByte()
        {
            // Return 8-bit colour
            // Return 5-6-5 format
            return (_colour8);
        }

        public ushort ToUInt16()
        {
            // Return 16-bit colour
            return (_colour16);
        }

        public ulong ToUInt32()
        {
            // Return 32-bit colour
            // Return b-g-r format to match bitmap format
            return (_colour32);
        }

        #endregion
        #region Private

        private void ColourConverter(byte red, byte green, byte blue)
        {
            _red = red;
            _green = green;
            _blue = blue;

            // Convert to 2-bit colour

            _colour2 = (byte)((_red >> 7) << 1 | (_green >> 6) | (_blue >> 7));

            // Convert to 4-bit colour

            byte intensity = (byte)(((_red >> 6) | (_green >> 6) | (_blue >> 6)) & 0x01);
            _colour4 = (byte)((intensity << 3) | ((_red >> 7) << 2) | ((_green >> 7) << 1) | (_blue >> 7));

            // Convert to 3-3-2 format 8-bit colour

            _colour8 = (byte)((_red >> 5) << 5 | (_green >> 5) << 2 | (_blue >> 6));

            // Convert to 5-6-5 format 16-bit colour

            _colour16 = (byte)((_red >> 3) << 11 | (_green >> 2) << 5 | (_blue >> 3));

            // Convert to 32-bit colour (b-g-r format to match bitmap format)

            _colour32 = (ulong)((_blue >> 3) | ((_green >> 2) << 8) | ((_red >> 3) << 16));
        }

        bool IEquatable<SolidColour>.Equals(SolidColour other)
        {
            if (other == null) return false;
            if (_red == other.Red && _green == other.Green && _blue == other.Blue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
