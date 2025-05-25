
namespace DisplayLibrary
{
    internal interface IText
    {
        public RasterFont Font
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

        public void Write(byte character);

        public byte Read();

        public byte Read(int column, int row);

        public void Write(byte character, Colour foreground, Colour background);

        public void Write(string text);

        public void Write(string text, Colour foreground, Colour background);

        public void Scroll();

        public void Scroll(int rows);

        void Clear();

        void Clear(Colour colour);

        void Generate();
        void PartialGenerate(int x1, int y1, int x2, int y2);

        public void Save(string filename);

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
