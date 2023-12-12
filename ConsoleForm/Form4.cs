using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ConsoleLibrary;
using Console = ConsoleLibrary.Console;

namespace ConsoleFrom
{
    public partial class Form4 : Form
    {
        ConsoleLibrary.Terminal terminal;
        KeyboardMatrix _matrix;

        public Form4()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            terminal = new ConsoleLibrary.Terminal(32, 32, 2, 1);

            RasterFont rasterFont = new RasterFont();
            string path = @"C:\SOURCE\GIT\cs.net\Console\FontLibrary";
            string fileName = "IBM_PC_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            terminal.Font = rasterFont;
            terminal.Set(0, 0);
            terminal.ForegroundColor = TextAdaptor.ConsoleColor.Black;
            terminal.BackgroundColor = TextAdaptor.ConsoleColor.White;
            terminal.Write("HELLO THIS SHOULD WRAP AROUND");
            _matrix = new KeyboardMatrix();
            panel1.Select();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //this.SuspendLayout();
            Graphics g = e.Graphics;
            Bitmap b = terminal.Bitmap;
            g.DrawImageUnscaled(b, 0, 0);
            //this.ResumeLayout();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 400; i++)
                {
                    terminal.Write((byte)'e');
                }
                terminal.Generate();
                panel1.Invalidate();
            }
            else
            {
                terminal.Write("Hello");
                panel1.Invalidate();
            }
        }

        private void panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            byte key = _matrix.ToASCII(e.KeyValue, e.Shift, e.Control, e.Alt);
            if (key > 0)
            {
                terminal.Write(key, TextAdaptor.ConsoleColor.Blue, TextAdaptor.ConsoleColor.Green);
                panel1.Invalidate();
            }
        }
    }

    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint, true);
        }
    }

}
