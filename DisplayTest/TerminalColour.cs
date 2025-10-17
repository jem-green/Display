using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;
using Console = DisplayTest.Console;

namespace DisplayTest
{
    public partial class TerminalColour : Form
    {
        DisplayTest.Terminal terminal;
        KeyboardMatrix _matrix;

        public TerminalColour()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            terminal = new DisplayTest.Terminal(32, 32, 2, 1);

            ROMFont rasterFont = new ROMFont();
            string path = @"C:\SOURCE\GIT\cs.net\Display\DisplayTest";
            string fileName = "IBM_XT286_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            terminal.Font = rasterFont;
            terminal.Set(0, 0);
            terminal.ForegroundColour = new Solid(0, 0, 0);
            terminal.BackgroundColour = new Solid(255,255,255);
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
                //terminal.Write(key, (byte)TextMode.ConsoleColour.Blue, (byte)TextMode.ConsoleColour.Green);
                terminal.Write(key, new Solid(0,0,255), new Solid(0,255,0));

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
