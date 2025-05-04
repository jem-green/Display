using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;
using Console = DisplayLibrary.Console;

namespace DisplayForm
{
    public partial class TerminalColour : Form
    {
        DisplayLibrary.Terminal terminal;
        KeyboardMatrix _matrix;

        public TerminalColour()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            terminal = new DisplayLibrary.Terminal(32, 32, 2, 1);

            RasterFont rasterFont = new RasterFont();
            string path = @"C:\SOURCE\GIT\cs.net\Display\FontLibrary";
            string fileName = "IBM_PC_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            terminal.Font = rasterFont;
            terminal.Set(0, 0);
            terminal.ForegroundColor = TextMode.ConsoleColour.Black;
            terminal.BackgroundColor = TextMode.ConsoleColour.White;
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
                terminal.Write(key, TextMode.ConsoleColour.Blue, TextMode.ConsoleColour.Green);
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
