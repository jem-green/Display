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
using Console = DisplayTest.Console;

namespace DisplayTest
{
    public partial class DisplayMonochromeText : Form
    {

        DisplayLibrary.MonochromeTextDisplay monochrome;

        public DisplayMonochromeText()
        {
            InitializeComponent();
            monochrome = new DisplayLibrary.MonochromeTextDisplay(16, 16, 2, 1);

            ROMFont rasterFont = new ROMFont();
            string path = @"C:\SOURCE\GIT\cs.net\Display\DisplayTest";
            string fileName = "IBM_XT286_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            monochrome.Clear(); // Need this to set the memory to 0
            monochrome.Font = rasterFont;
            monochrome.Set(0, 0);
            //monochrome.ForegroundColour = (byte)TextMode.ConsoleColour.Green;
            monochrome.ForegroundColour = new SolidColour(0, 255, 0); // Green foreground
            //monochrome.BackgroundColour = (byte)TextMode.ConsoleColour.Black;
            monochrome.BackgroundColour = new SolidColour(0, 0, 0); // Black background
            monochrome.Write("HELLO THIS SHOULD WRAP AROUND");
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
                    monochrome.Write((byte)'e');
                }
                monochrome.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                monochrome.Write("Hello");
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            int key = e.KeyValue;
            monochrome.Write((byte)key);
            pictureBox1.Invalidate();
        }
    }
}
