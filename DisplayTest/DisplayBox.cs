using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DisplayLibrary;

namespace DisplayTest
{
    public partial class DisplayBox : UserControl
    {
        #region Fields

        DisplayTest.Terminal terminal;

        #endregion
        #region Constructor

        public DisplayBox()
        {
            InitializeComponent();

            terminal = new DisplayTest.Terminal(32, 32, 2, 1);

            ROMFont rasterFont = new ROMFont();
            string path = @"C:\SOURCE\GIT\cs.net\Display\DisplayTest";
            string fileName = "IBM_XT286_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            terminal.Font = rasterFont;
            terminal.Set(0, 0);
            //terminal.ForegroundColour = (byte)TextMode.ConsoleColour.Black;
            terminal.ForegroundColour = new SolidColour(0, 0, 0);
            //terminal.BackgroundColour = (byte)TextMode.ConsoleColour.White;
            terminal.BackgroundColour = new SolidColour(255, 255, 255);
            terminal.Write("HELLO THIS SHOULD WRAP AROUND");
            pictureBox1.Select();
        }

        #endregion
        #region Properties
        #endregion
        #region Methods

        public void Write(byte character)
        {
            terminal.Write(character);
        }

        public void Write(string text)
        {
            terminal.Write(text);
        }

        #endregion


    }
}
