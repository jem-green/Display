using System;

namespace DisplayLibrary
{
    public class Colour : IEquatable<Colour>
    {
        #region Fields

        private byte _red = 0;
        private byte _green = 0;
        private byte _blue = 0;
        private byte _console = 0;
        private byte _colour = 0;

        #endregion
        #region Constructors

        public Colour(byte red, byte green, byte blue)
        {
            _red = red;
            _green = green;
            _blue = blue;

            // Convert to 4-bit colour

            byte c = _red;
            byte g = (byte)((_green >> 3) & 0b00011100);
            c = (byte)(c | g);
            byte b = (byte)((_blue >> 6) & 0b00000011);
            _console = (byte)(c | b);

            // Convert to 5-6-5 format

            _colour = (byte)((_red >> 3) << 11 | (_green >> 2) << 5 | (_blue >> 3));

        }

        public Colour(string rgb)
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
                    _console = (byte)(c | b);

                    // Convert to 5-6-5 format

                    _colour = (byte)((_red >> 3) << 11 | (_green >> 2) << 5 | (_blue >> 3));

                }
            }
        }

        #endregion
        #region Properties

        public byte R
        {
            get { return _red; }
            set { _red = value; }
        }

        public byte G
        {
            get { return _green; }
            set { _green = value; }
        }

        public byte B
        {
            get { return _blue; }
            set { _blue = value; }
        }

        #endregion
        #region Methods

        public byte ToByte()
        {
            // Return 4-bit colour
            return (_console);
        }

        public int ToInt32()
        {
            // Return 5-6-5 format
            return (_colour);
        }

        #endregion
        #region Private

        bool IEquatable<Colour>.Equals(Colour other)
        {
            if (other == null) return false;
            if (_red == other.R && _green == other.G && _blue == other.B)
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
