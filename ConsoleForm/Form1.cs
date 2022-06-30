using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleLibrary;
using Console = ConsoleLibrary.Console;

namespace ConsoleFrom
{
    public partial class Form1 : Form
    {

        //ConsoleLibrary.Console console;
        ConsoleLibrary.Terminal terminal;

        public Form1()
        {
            InitializeComponent();
            //console = new ConsoleLibrary.Console(32, 32, 2);
            //RasterFont rasterFont = new RasterFont();
            //rasterFont.Load("spectrum.bin");
            //console.Font = rasterFont;
            //console.Set(0, 0);
            //console.ForegroundColor = Adaptor.ConsoleColor.Black;
            //console.BackgroundColor = Adaptor.ConsoleColor.White;
            //console.Write("HELLO THIS SHOULD WRAP AROUND");
            //pictureBox1.Select();

            terminal = new ConsoleLibrary.Terminal(32, 32, 2);
            RasterFont rasterFont = new RasterFont();
            rasterFont.Load("spectrum.bin");
            terminal.Font = rasterFont;
            terminal.Set(0, 0);

            terminal.ForegroundColor = Adaptor.ConsoleColor.Black;
            terminal.BackgroundColor = Adaptor.ConsoleColor.White;
            terminal.Write("HELLO THIS SHOULD WRAP AROUND");
            pictureBox1.Select();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //Bitmap b = console.Paint();
            Bitmap b = terminal.Paint();
            g.DrawImageUnscaled(b, 0, 0);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 400; i++)
            {
                //console.Write((byte)'e');
                terminal.Write((byte)'e');
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            int key = e.KeyValue;
            //console.Write((byte)key);
            terminal.Write((byte)key, Adaptor.ConsoleColor.Blue, Adaptor.ConsoleColor.Green);
            pictureBox1.Invalidate();
        }
    }
}
