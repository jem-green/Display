using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;

namespace DisplayTest
{
    // A test form to display a Colour Graphics Display 4-bit colour


    public partial class DisplayColourGraphics : Form
    {
        DisplayLibrary.ColourGraphicsDisplay colourDisplay;
        KeyboardMatrix _matrix;

        public DisplayColourGraphics()
        {
            InitializeComponent();

            colourDisplay = new DisplayLibrary.ColourGraphicsDisplay(128, 128, 2, 1);
            colourDisplay.Generate();
            colourDisplay.Set(0, 0);
            pictureBox1.Select();

            SolidColour colour = new SolidColour(0, 0, 255);    // Blue border
            for (int i = 0; i < 128; i++)
            {
                colourDisplay.Put(i, 0, colour);
                colourDisplay.Put(i, 127, colour);
                colourDisplay.Put(0, i, colour);
                colourDisplay.Put(127, i, colour);
            }

            colour = new SolidColour(255, 0, 0);    // Red corners
            colourDisplay.Put(0, 0, colour);
            colourDisplay.Put(127, 0, colour);
            colourDisplay.Put(127, 127, colour);
            colourDisplay.Put(0, 127, colour);


            colourDisplay.PartialGenerate(0, 0, 127, 127);



            //colourDisplay.Generate();
            pictureBox1.Invalidate();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap b = colourDisplay.Bitmap;
            g.DrawImageUnscaled(b, 0, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            SolidColour colour = new SolidColour(0,255,0);
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 128; i++)
                {
                    colourDisplay.Put(i, i, colour);
                }
                colourDisplay.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                for (int i = 0; i < 128; i++)
                {
                    colourDisplay.Put(127-i, i, colour);
                }
                colourDisplay.Generate();
                pictureBox1.Invalidate();
            }
        }
    }
}
