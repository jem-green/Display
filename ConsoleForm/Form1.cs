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

            terminal = new ConsoleLibrary.Terminal(32, 32, 2, 1);
            RasterFont rasterFont = new RasterFont();
           // rasterFont.Load("spectrum.bin");
            rasterFont.Load("IBM_VGA_8x16.bin");
            terminal.Font = rasterFont;
            terminal.Set(0, 0);
            terminal.ForegroundColor = MonochromeAdaptor.ConsoleColor.Black;
            terminal.BackgroundColor = MonochromeAdaptor.ConsoleColor.White;
            terminal.Write("HELLO THIS SHOULD WRAP AROUND");

            _matrix = new KeyboardMatrix();

            pictureBox1.Select();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap b = terminal.Paint();
            g.DrawImageUnscaled(b, 0, 0);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 400; i++)
            {
                terminal.Write((byte)'e');
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
