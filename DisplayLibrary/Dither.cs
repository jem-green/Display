using System;

namespace DisplayLibrary
{
    public class Dither : IEquatable<Solid>, IColour
    {
        #region Fields

        // Want an easy way to convert RGB to different colour formats
        // Want to specify a single colour for the dither algorithm


        private byte _red = 0;
        private byte _green = 0;
        private byte _blue = 0;
        private byte _colour4 = 0;      // 4-bit colour
        private byte _colour8 = 0;      // 8-bit colour
        private ushort _colour16 = 0;   // 16-bit colour
        private ulong _colour32 = 0;    // 32-bit colour

        #endregion
        #region Constructors

        public Dither(byte red, byte green, byte blue)
        {
            _red = red;
            _green = green;
            _blue = blue;

            // Convert to 4-bit colour

            byte c = _red;
            byte g = (byte)((_green >> 3) & 0b00011100);
            c = (byte)(c | g);
            byte b = (byte)((_blue >> 6) & 0b00000011);
            _colour4 = (byte)(c | b);

            // Convert to 5-6-5 format

            _colour8 = (byte)((_red >> 3) << 11 | (_green >> 2) << 5 | (_blue >> 3));

            // Convert to 16-bit colour

            _colour16 = (ushort)((_red >> 3) << 11 | (_green >> 2) << 5 | (_blue >> 3));

            // Convert to 32-bit colour (b-g-r format to match bitmap format)

            _colour32 = (ulong)((_blue >> 3) | ((_green >> 2) << 8) | ((_red >> 3) << 16));


        }

        public Dither(string rgb)
        {
            if (rgb.Length != 7)
            {
                throw new ArgumentException("RGB string must be 6 characters long");
            }
            else
            {
                if (rgb[0] != '#')
                {
                    throw new ArgumentException("RGB string must start with #");
                }
                else
                {
                    _red = Convert.ToByte(rgb.Substring(1, 2), 16);
                    _green = Convert.ToByte(rgb.Substring(3, 2), 16);
                    _blue = Convert.ToByte(rgb.Substring(5, 2), 16);

                    // Convert to 4-bit colour

                    byte c = _red;
                    byte g = (byte)((_green >> 3) & 0b00011100);
                    c = (byte)(c | g);
                    byte b = (byte)((_blue >> 6) & 0b00000011);
                    _colour4 = (byte)(c | b);

                    // Convert to 5-6-5 format

                    _colour8 = (byte)((_red >> 3) << 11 | (_green >> 2) << 5 | (_blue >> 3));

                    // Convert to 16-bit colour

                    _colour16 = (ushort)((_red >> 3) << 11 | (_green >> 2) << 5 | (_blue >> 3));

                    // Convert to 32-bit colour (b-g-r format to match bitmap format)

                    _colour32 = (ulong)((_blue >> 3) | ((_green >> 2) << 8) | ((_red >> 3) << 16));

                }
            }
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

        bool IEquatable<Solid>.Equals(Solid other)
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
