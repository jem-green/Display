using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;
using Console = DisplayTest.Console;

namespace DisplayTest
{
    public partial class DisplayVibrantGraphics : Form
    {
        DisplayLibrary.TrueGraphicsDisplay vibrantDisplay;
        KeyboardMatrix _matrix;

        public DisplayVibrantGraphics()
        {
            InitializeComponent();

            vibrantDisplay = new DisplayLibrary.TrueGraphicsDisplay(128, 128, 2, 1);
            vibrantDisplay.Generate();
            vibrantDisplay.Set(0, 0);
            pictureBox1.Select();

            SolidColour colour = new SolidColour(255, 0, 0);
            for (int i = 0; i < 128; i++)
            {
                vibrantDisplay.Write(i, 0, colour);
                vibrantDisplay.Write(i, 127, colour);
                vibrantDisplay.Write(0, i, colour);
                vibrantDisplay.Write(127, i, colour);
            }
            vibrantDisplay.PartialGenerate(0,0,127,127);
            pictureBox1.Invalidate();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap b = vibrantDisplay.Bitmap;
            g.DrawImageUnscaled(b, 0, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            SolidColour colour = new SolidColour(0,255,0);
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 128; i++)
                {
                    vibrantDisplay.Write(i, i, colour);
                }
                vibrantDisplay.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                for (int i = 0; i < 128; i++)
                {
                    vibrantDisplay.Write(127-i, i, colour);
                }
                vibrantDisplay.Generate();
                pictureBox1.Invalidate();
            }
        }
    }
}
