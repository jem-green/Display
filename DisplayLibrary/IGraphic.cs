
using System.Dynamic;

namespace DisplayLibrary
{
    public interface IGraphic
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

        public void Write(IColour c);
        public void Write(int x, int y, IColour c);

        public IColour Read(int x, int y);

        void Clear();

        void Clear(IColour colour);

        void Generate();

        void PartialGenerate(int x1, int y1, int x2, int y2);

        public void Save(string path, string filename);

    }
}
