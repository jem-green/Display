using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DisplayLibrary;
using Console = DisplayLibrary.Console;

namespace DisplayForm
{
    public partial class DisplayMonochrome : Form
    {

        DisplayLibrary.MonochromeTextDisplay monochrome;

        public DisplayMonochrome()
        {
            InitializeComponent();
            monochrome = new DisplayLibrary.MonochromeTextDisplay(32, 32, 2);

            RasterFont rasterFont = new RasterFont();
            string path = @"C:\SOURCE\GIT\cs.net\Display\FontLibrary";
            string fileName = "IBM_PC_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            monochrome.Clear(); // Need this to set the memory to 0
            monochrome.Font = rasterFont;
            monochrome.Set(0, 0);
            monochrome.ForegroundColour = (byte)TextMode.ConsoleColour.Green;
            monochrome.BackgroundColour = (byte)TextMode.ConsoleColour.Black;
            monochrome.Write("HELLO THIS SHOULD WRAP AROUND",0,0);
            pictureBox1.Select();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap b = monochrome.Bitmap;
            g.DrawImageUnscaled(b, 0, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 400; i++)
                {
                    monochrome.Write((byte)'e',0,0);
                }
                monochrome.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                monochrome.Write("Hello", 0, 0);
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            int key = e.KeyValue;
            monochrome.Write((byte)key, 0, 0);
            pictureBox1.Invalidate();
        }
    }
}
