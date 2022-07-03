using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFrom
{

    /// <summary>
    /// Mapping the PreviewKey key events to the ASCII characters
    /// 
    /// </summary>
    public class KeyboardMatrix
    {
        #region Fields

        Dictionary<int, Key> _keys;

        public struct Key
        {
            int _keyValue;
            byte _normal;
            byte _shift;

            public Key(int keyValue, byte normal ,byte shift)
            {
                _keyValue = keyValue;
                _normal = normal;
                _shift = shift;
            }

            public int KeyValue
            {
                get
                {
                    return (_keyValue);
                }
            }

            public byte Normal
            {
                get
                {
                    return (_normal);
                }
            }

            public byte Shift
            {
                get
                {
                    return (_shift);
                }
            }
        }

        #endregion

        public KeyboardMatrix()
        {
            _keys = new Dictionary<int, Key>();

            _keys.Add(32, new Key(32, 32, 32)); // space

            // Top Row

            _keys.Add(49, new Key(49, 49, 33)); // 1 and !
            _keys.Add(50, new Key(50, 50, 34)); // 2 and "
            _keys.Add(51, new Key(51, 51, 35)); // 3 and £ - not ascii
            _keys.Add(52, new Key(52, 52, 36)); // 4 and $
            _keys.Add(53, new Key(53, 53, 37)); // 5 and $
            _keys.Add(54, new Key(54, 54, 94)); // 6 and ^
            _keys.Add(55, new Key(55, 55, 38)); // 7 and &
            _keys.Add(56, new Key(56, 56, 43)); // 8 and *
            _keys.Add(57, new Key(57, 57, 40)); // 9 and (
            _keys.Add(48, new Key(48, 48, 41)); // 0 and )
            _keys.Add(45, new Key(45, 45, 95)); // - and _
            _keys.Add(61, new Key(61, 61, 43)); // = and +

            // Second Row

            _keys.Add(81, new Key(81, 113, 81));    // Q and q
            _keys.Add(87, new Key(87, 119, 87));    // W and w
            _keys.Add(69, new Key(69, 101,69));     // E and e
            _keys.Add(82, new Key(82, 114,82));     // R and r
            _keys.Add(84, new Key(84, 116,84));     // T and t
            _keys.Add(89, new Key(89, 121,89));     // Y and y
            _keys.Add(85, new Key(85, 117,85));     // U and u
            _keys.Add(73, new Key(73, 105,73));     // I and i
            _keys.Add(79, new Key(79, 111,79));     // O and o
            _keys.Add(80, new Key(80, 112,88));     // P and p
            _keys.Add(91, new Key(91, 91, 123));    // [ and {
            _keys.Add(93, new Key(93, 93, 125));    // ] and }
        }

        public byte ToASCII(int key, bool shift)
        {
            byte ascii = 0;
            try
            {
                if (shift == false)
                {
                    ascii = _keys[key].Normal;
                }
                else
                {
                    ascii = _keys[key].Shift;
                }
            }
            catch
            {
                ascii = (byte)key;
            }
            return (ascii);
        }

    }
}
