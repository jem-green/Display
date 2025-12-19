using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;

namespace DisplayTest
{

    // A test form to display a True Graphics Display 24-bit colour

    public partial class DisplayTrueGraphics : Form
    {
        DisplayLibrary.TrueGraphicsDisplay trueDisplay;
        KeyboardMatrix _matrix;

        public DisplayTrueGraphics()
        {
            InitializeComponent();

            trueDisplay = new DisplayLibrary.TrueGraphicsDisplay(128, 128, 2, 1);
            trueDisplay.Generate();
            trueDisplay.Set(0, 0);
            pictureBox1.Select();

            SolidColour colour = new SolidColour(0, 0, 255);
            for (int i = 0; i < 128; i++)
            {
                trueDisplay.Put(i, 0, colour);
                trueDisplay.Put(i, 127, colour);
                trueDisplay.Put(0, i, colour);
                trueDisplay.Put(127, i, colour);
            }

            colour = new SolidColour(255, 0, 0);
            trueDisplay.Put(0, 0, colour);
            trueDisplay.Put(127, 0, colour);
            trueDisplay.Put(127, 127, colour);
            trueDisplay.Put(0, 127, colour);

            trueDisplay.PartialGenerate(0,0,127,127);
            pictureBox1.Invalidate();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap b = trueDisplay.Bitmap;
            g.DrawImageUnscaled(b, 0, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            SolidColour colour = new SolidColour(0,255,0);
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 128; i++)
                {
                    trueDisplay.Put(i, i, colour);
                }
                trueDisplay.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                for (int i = 0; i < 128; i++)
                {
                    trueDisplay.Put(127-i, i, colour);
                }
                trueDisplay.Generate();
                pictureBox1.Invalidate();
            }
        }
    }
}
