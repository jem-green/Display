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
    public partial class ConsoleMonochrome : Form
    {

        DisplayLibrary.Console console;

        public ConsoleMonochrome()
        {
            InitializeComponent();
            console = new DisplayLibrary.Console(32, 32, 2);

            RasterFont rasterFont = new RasterFont();
            string path = @"C:\SOURCE\GIT\cs.net\Display\FontLibrary";
            string fileName = "IBM_PC_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            console.Font = rasterFont;
            console.Set(0, 0);
            console.ForegroundColor = TextMode.ConsoleColor.Green;
            console.BackgroundColor = TextMode.ConsoleColor.Black;
            console.Write("HELLO THIS SHOULD WRAP AROUND");
            pictureBox1.Select();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap b = console.Bitmap;
            g.DrawImageUnscaled(b, 0, 0);
            //g.DrawImage(b, 0, 0,pictureBox1.Width-vScrollBar1.Width, pictureBox1.Height-8);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 400; i++)
                {
                    console.Write((byte)'e');
                }
                console.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                console.Write("Hello");
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            int key = e.KeyValue;
            console.Write((byte)key);
            pictureBox1.Invalidate();
        }
    }
}
