
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace DisplayLibrary
{
    public class ROMFont
    {
        #region Fields

        byte[] _data;
        int _hbits;
        int _vbits;

        #endregion
        #region Constructors
        #endregion
        #region Properties

        public byte[] Data
        {
            set
            {
                _data = value;
            }
            get
            {
                return (_data);
            }
        }

        public int Horizontal
        {
            set
            {
                _hbits = value;
            }
            get
            {
                return (_hbits);
            }
        }

        public int Vertical
        {
            set
            {
                _vbits = value;
            }
            get
            {
                return (_vbits);
            }
        }

        #endregion
        #region Methods

        public void Load(string filePath)
        {
            BinaryReader binaryReader = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read));

            binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);

            _hbits = binaryReader.ReadByte();
            _vbits = binaryReader.ReadByte();
            int hbytes = (int)Math.Round((double)_hbits / 8);
            int length = hbytes * _vbits * 255;
            _data = new byte[length];
            _data = binaryReader.ReadBytes(length);

            binaryReader.Close();
            binaryReader.Dispose();

        }

        #endregion
        #region Private
        #endregion
    }
}
