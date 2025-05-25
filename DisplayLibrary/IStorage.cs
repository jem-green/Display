
namespace DisplayLibrary
{
    public interface IStorage
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

    }
}
