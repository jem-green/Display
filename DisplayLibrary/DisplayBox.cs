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

namespace DisplayLibrary
{
    public partial class DisplayBox : UserControl
    {
        #region Fields

        DisplayLibrary.Terminal terminal;

        #endregion
        #region Constructor

        public DisplayBox()
        {
            InitializeComponent();

            terminal = new DisplayLibrary.Terminal(32, 32, 2, 1);

            RasterFont rasterFont = new RasterFont();
            string path = @"C:\SOURCE\GIT\cs.net\Display\FontLibrary";
            string fileName = "IBM_PC_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            terminal.Font = rasterFont;
            terminal.Set(0, 0);
            terminal.ForegroundColour = (byte)TextMode.ConsoleColour.Black;
            terminal.BackgroundColour = (byte)TextMode.ConsoleColour.White;
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
