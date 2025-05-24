
using System.Dynamic;

namespace DisplayLibrary
{
    public interface IDisplay
    {
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

        void Clear();

        void Clear(Colour colour);

        void Generate();
        void PartialGenerate(int x1, int y1, int x2, int y2);

        public int X
        {
            set;
            get;
        }

        public int Y
        {
            set;
            get;
        }

        public void Set(int x, int y);

        public void Write(int x, int y, Colour c);

        public Colour Read(int x, int y);

        public void Save(string filename);

    }
}
