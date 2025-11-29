using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayLibrary
{
    public interface IColour
    {
        public byte Red
        {
            get;
            set;
        }

        public byte Green
        {
            get;
            set;
        }

        public byte Blue
        {
            get;
            set;
        }

        public byte To2Bit();

        public byte ToNybble();

        public byte ToByte();

        public ushort ToUInt16();

        public ulong ToUInt32();

    }
}