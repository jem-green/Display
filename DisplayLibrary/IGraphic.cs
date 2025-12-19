
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

        // Set current position

        public void Set(int x, int y);

        // Put colour at current position

        public void Put(IColour c);
        public void Put(int x, int y, IColour c);

        // Fetch colour at current position

        public IColour Fetch();
        public IColour Fetch(int x, int y);

        void Clear();

        void Clear(IColour colour);

        void Generate();

        void PartialGenerate(int x1, int y1, int x2, int y2);

        public void Save(string path, string filename);

    }
}
