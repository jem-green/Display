using System;
using System.Drawing;
using System.Windows.Forms;
using ConsoleLibrary;
using Console = ConsoleLibrary.Console;

namespace ConsoleFrom
{
    public partial class Form1 : Form
    {
        ConsoleLibrary.Terminal terminal;
        KeyboardMatrix _matrix;

        public Form1()
        {
            InitializeComponent();

            terminal = new ConsoleLibrary.Terminal(10, 10, 2, 2);
            RasterFont rasterFont = new RasterFont();
            // rasterFont.Load("spectrum.bin");
            //rasterFont.Load("IBM_VGA_8x16.bin");
            rasterFont.Load("IBM_PC_V1_8x8.bin");

            terminal.Font = rasterFont;
            terminal.Set(0, 0);
            terminal.ForegroundColor = TextAdaptor.ConsoleColor.Black;
            terminal.BackgroundColor = TextAdaptor.ConsoleColor.White;
            terminal.Write("HELLO THIS SHOULD WRAP AROUND");
            _matrix = new KeyboardMatrix();

            pictureBox1.Select();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap b = terminal.Bitmap;
            g.DrawImageUnscaled(b, 0, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 400; i++)
                {
                    terminal.Write((byte)'e');
                }
                terminal.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                terminal.Write("Hello");
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            byte key = _matrix.ToASCII(e.KeyValue, e.Shift, e.Control, e.Alt);
            if (key > 0)
            {
                terminal.Write(key, TextAdaptor.ConsoleColor.Blue, TextAdaptor.ConsoleColor.Green);
                pictureBox1.Invalidate();
            }
        }
    }
}
