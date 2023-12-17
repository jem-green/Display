using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace DisplayLibrary
{
    public class RasterFont
    {
        #region Fields

        byte[] _image;
        int _hbits;
        int _vbits;

        #endregion
        #region Constructors
        #endregion
        #region Properties

        public byte[] Image
        {
            set
            {
                _image = value;
            }
            get
            {
                return (_image);
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
            _image = new byte[length];
            _image = binaryReader.ReadBytes(length);

            binaryReader.Close();
            binaryReader.Dispose();

        }

        #endregion
        #region Private
        #endregion
    }
}
