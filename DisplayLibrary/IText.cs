
namespace DisplayLibrary
{
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

        public void Set(int column, int row);

        public byte Read();

        public byte Read(int column, int row);

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
