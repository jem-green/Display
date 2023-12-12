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
using ConsoleLibrary;
using Console = ConsoleLibrary.Console;

namespace ConsoleFrom
{
    public partial class DisplayMonochrome : Form
    {

        ConsoleLibrary.MonochromeDisplay monochrome;

        public DisplayMonochrome()
        {
            InitializeComponent();
            monochrome = new ConsoleLibrary.MonochromeDisplay(32, 32, 1);

            RasterFont rasterFont = new RasterFont();
            string path = @"C:\SOURCE\GIT\cs.net\Console\FontLibrary";
            string fileName = "IBM_PC_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            monochrome.Font = rasterFont;
            monochrome.Set(0, 0);
            monochrome.ForegroundColor = TextAdaptor.ConsoleColor.Green;
            monochrome.BackgroundColor = TextAdaptor.ConsoleColor.Black;
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
