using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleLibrary.Adaptor;

namespace ConsoleLibrary
{
    internal interface IAdaptor
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
    }
}
