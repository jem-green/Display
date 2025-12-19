using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;

namespace DisplayTest
{
    // A test form to display a Colour Graphics Display 4-bit colour


    public partial class DisplayEnhancedGraphics : Form
    {
        DisplayLibrary.EnhancedGraphicsDisplay enhancedDisplay;
        KeyboardMatrix _matrix;

        public DisplayEnhancedGraphics()
        {
            InitializeComponent();

            enhancedDisplay = new DisplayLibrary.EnhancedGraphicsDisplay(128, 128, 2, 1);
            enhancedDisplay.Generate();
            enhancedDisplay.Set(0, 0);
            pictureBox1.Select();

            SolidColour colour = new SolidColour(0, 0, 255);
            for (int i = 0; i < 128; i++)
            {
                enhancedDisplay.Put(i, 0, colour);
                enhancedDisplay.Put(i, 127, colour);
                enhancedDisplay.Put(0, i, colour);
                enhancedDisplay.Put(127, i, colour);
            }

            colour = new SolidColour(255, 0, 0);
            enhancedDisplay.Put(0, 0, colour);
            enhancedDisplay.Put(127, 0, colour);
            enhancedDisplay.Put(127, 127, colour);
            enhancedDisplay.Put(0, 127, colour);

            enhancedDisplay.PartialGenerate(0, 0, 127, 127);



            //colourDisplay.Generate();
            pictureBox1.Invalidate();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap b = enhancedDisplay.Bitmap;
            g.DrawImageUnscaled(b, 0, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            SolidColour colour = new SolidColour(0,255,0);
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 128; i++)
                {
                    enhancedDisplay.Put(i, i, colour);
                }
                enhancedDisplay.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                for (int i = 0; i < 128; i++)
                {
                    enhancedDisplay.Put(127-i, i, colour);
                }
                enhancedDisplay.Generate();
                pictureBox1.Invalidate();
            }
        }
    }
}
