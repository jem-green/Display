using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayLibrary
{
    public class Colour : IEquatable<Colour>
    {
        #region Fields

        private byte _r = 0;
        private byte _g = 0;
        private byte _b = 0;

        #endregion
        #region Constructors

        public Colour(byte r, byte g, byte b)
        {
            _r = r;
            _g = g;
            _b = b;
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
                    _r = Convert.ToByte(rgb.Substring(1, 2), 16);
                    _g = Convert.ToByte(rgb.Substring(3, 2), 16);
                    _b = Convert.ToByte(rgb.Substring(5, 2), 16);
                }
            }
        }

        #endregion
        #region Properties

        public byte R
        {
            get { return _r; }
            set { _r = value; }
        }

        public byte G
        {
            get { return _g; }
            set { _g = value; }
        }

        public byte B
        {
            get { return _b; }
            set { _b = value; }
        }

        #endregion
        #region Methods

        bool IEquatable<Colour>.Equals(Colour? other)
        {
            if (other == null) return false;
            if (_r == other.R && _g == other.G && _b == other.B)
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
