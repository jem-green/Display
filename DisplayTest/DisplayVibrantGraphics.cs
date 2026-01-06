using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;

namespace DisplayTest
{

    // A test form to display a Vibrant Graphics Display 8-bit colour

    public partial class DisplayVibrantGraphics : Form
    {
        DisplayLibrary.VibrantGraphicsDisplay vibrantDisplay;
        KeyboardMatrix _matrix;

        public DisplayVibrantGraphics()
        {
            InitializeComponent();

            vibrantDisplay = new DisplayLibrary.VibrantGraphicsDisplay(128, 128, 2, 1);
            vibrantDisplay.Generate();
            vibrantDisplay.Set(0, 0);
            pictureBox1.Select();

            SolidColour colour = new SolidColour(0, 0, 255);    // Red border
            for (int i = 0; i < 128; i++)
            {
                vibrantDisplay.Put(i, 0, colour);
                vibrantDisplay.Put(i, 127, colour);
                vibrantDisplay.Put(0, i, colour);
                vibrantDisplay.Put(127, i, colour);
            }

            colour = new SolidColour(255, 0, 0);    // Blue corners
            vibrantDisplay.Put(0, 0, colour);
            vibrantDisplay.Put(127, 0, colour);
            vibrantDisplay.Put(127, 127, colour);
            vibrantDisplay.Put(0, 127, colour);

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
            SolidColour colour = new SolidColour(0,255,0);  // Green diagonal
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 128; i++)
                {
                    vibrantDisplay.Put(i, i, colour);
                }
                vibrantDisplay.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                for (int i = 0; i < 128; i++)
                {
                    vibrantDisplay.Put(127-i, i, colour);
                }
                vibrantDisplay.Generate();
                pictureBox1.Invalidate();
            }
        }
    }
}
