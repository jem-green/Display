
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

namespace ROMFontLibrary
{
    public class ROMFont  //: IEnumerable<ROMFont.Character>
    {
        /*
        Font file
        =========

                  1         2
        012345678901234567890
        ~+~--+........
         |   |
         |   +- Image Start
         +- Header (2)

        +-----------------------+ <- 0
        | Header (2)            | 
        +-----------------------+ <- 2 [_data]
        | Image data            |     
        |          ...          |  
        +-----------------------+

        // Header
        // ------
        //
        // 01234567890
        // ++
        // ||
        // |+- vbits[1] = 0x00;    // Vertical bits per character 
        // +- hbits[1] = 0x00;     // Horizontal bits per character
        //
        // Image Data
        // ----------
        // 
        // 01234567890...
        // ~+..¬
        //  |
        //  +- Image data for the character [varies]
        //
        */

        #region Fields

        byte _chars;        // Number of characters in the font 255
        byte _hbits;        // Horizontal bits per character
        byte _vbits;        // Vertical bits per character

        // Ideally there should be a byte array for the lookup table, but this may not needed
        // The space needs to be pre-allocated for ASCII

        // The image data for the font

        byte[] _data;

        #endregion
        #region Constructors

        public ROMFont()
        {
            // Assume ASCII
            _chars = 255;
            _hbits = 0;
            _vbits = 0;
        }

        #endregion
        #region Properties

        public byte Horizontal
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

        public byte Vertical
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

        public int Count
        {
            get
            {
                return (_chars);
            }
        }

        #endregion
        #region Methods

        public void Load(string path, string filename)
        {
            if (path.Length == 0)
            {
                path = ".";
            }
            string filenamePath = Path.Combine(path, filename + ".bin");

            BinaryReader binaryReader = new BinaryReader(File.Open(filenamePath, FileMode.Open, FileAccess.Read));

            binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);

            _hbits = binaryReader.ReadByte();
            _vbits = binaryReader.ReadByte();
            int hbytes = (int)Math.Round((double)_hbits / 8);
            int length = hbytes * _vbits * 255;
            _data = new byte[length];
            _data = binaryReader.ReadBytes(length);
            binaryReader.Close();

        }

        public void Save(string path, string filename)
        {
            string filePath = Path.Combine(path, filename + ".bin");
            if (File.Exists(filePath) == true)
            {
                File.Delete(filePath);
            }

            BinaryWriter binaryWriter = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write));
            binaryWriter.Write((byte)_hbits);
            binaryWriter.Write((byte)_vbits);
            int hbytes = (int)Math.Round((double)_hbits / 8);
            int length = hbytes * _vbits * 255;
            binaryWriter.Write(_data, 0, length);
            binaryWriter.Flush();
            binaryWriter.Close();
        }

        #endregion
        #region Private
        #endregion
        #region Enumerable
        #endregion
    }
}
