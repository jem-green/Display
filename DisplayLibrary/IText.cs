
using System.Windows.Forms;

namespace DisplayLibrary
{
    public struct Position
    {
        private int _column;
        private int _row;

        public Position(int column, int row)
        {
            _column = column;
            _row = row;
        }

        public Position()
        {
            _column = 0;
            _row = 0;
        }

        public int Column
        {
            get { return _column; }
            set { _column = value; }
        }

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

    }

    public interface IText
    {
        public ROMFont Font
        {
            set;
        }

        public int Row
        {
            set;
            get;
        }

        public int Column
        {
            set;
            get;
        }

        // Set the cursor position

        public void Set(int column, int row);

        //public Position Get();

        // Set characters at the current cursor position with specified character

        public void Put(int column, int row, byte character);

        public void Put(byte character);

        public void Put(byte character, IColour foreground, IColour background);

        public void Put(int column, int row, byte character, IColour foreground, IColour background);

        // Fetch characters at the current cursor position

        public byte Fetch();

        public byte Fetch(int column, int row);

        // Read characters and strings at the current cursor position

        public byte Read();

        public byte Read(int column, int row);

        //public string Read(int length);
        //public string Read(int column, int row, int length);

        // Write characters and strings at the current cursor position

        public void Write(byte character);

        public void Write(byte character, IColour foreground, IColour background);

        public void Write(string text);

        public void Write(string text, IColour foreground, IColour background);

        public void Scroll();

        public void Scroll(int rows);

        void Clear();

        void Clear(IColour colour);

        void Generate();
        void PartialGenerate(int x1, int y1, int x2, int y2);

        public void Save(string path, string filename);

        int Left
        {
            set;
            get;
        }

        int Top
        {
            set;
            get;
        }

        int Width
        {
            set;
            get;
        }

        int Height
        {
            set;
            get;
        }

        byte[] Memory
        {
            set;
            get;
        }
    }
}
