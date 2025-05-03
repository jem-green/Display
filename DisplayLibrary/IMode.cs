using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DisplayLibrary.DisplayMode;

namespace DisplayLibrary
{
    internal interface IMode
    {
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
        void Generate();
        void PartialGenerate(int column, int row);
    }
}
