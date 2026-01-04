using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;

namespace DisplayTest
{
    // A test form to display a Colour Graphics Display 4-bit colour


    public partial class DisplayMonochromeGraphics : Form
    {
        DisplayLibrary.MonochromeGraphicsDisplay monochromeDisplay;
        KeyboardMatrix _matrix;

        public DisplayMonochromeGraphics()
        {
            InitializeComponent();

            monochromeDisplay = new DisplayLibrary.MonochromeGraphicsDisplay(128, 128, 1, 1);
            monochromeDisplay.Generate();
            monochromeDisplay.Set(0, 0);
            //monochromeDisplay.Background = new SolidColour(255, 255, 255); // White background
            //monochromeDisplay.Foreground = new SolidColour(0, 0, 0);       // Black foreground

            pictureBox1.Select();

            SolidColour colour = new SolidColour(0, 0, 255);    // Blue border
            for (int i = 0; i < 128; i++)
            {
                monochromeDisplay.Put(i, 0, colour);
                monochromeDisplay.Put(i, 127, colour);
                monochromeDisplay.Put(0, i, colour);
                monochromeDisplay.Put(127, i, colour);
            }

            colour = new SolidColour(255, 0, 0);
            monochromeDisplay.Put(0, 0, colour);
            monochromeDisplay.Put(127, 0, colour);
            monochromeDisplay.Put(127, 127, colour);
            monochromeDisplay.Put(0, 127, colour);

            monochromeDisplay.PartialGenerate(0, 0, 127, 127);

            //colourDisplay.Generate();
            pictureBox1.Invalidate();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap b = monochromeDisplay.Bitmap;
            g.DrawImageUnscaled(b, 0, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            SolidColour colour = new SolidColour(0,255,0);
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 128; i++)
                {
                    monochromeDisplay.Put(i, i, colour);
                }
                monochromeDisplay.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                for (int i = 0; i < 128; i++)
                {
                    monochromeDisplay.Put(127-i, i, colour);
                }
                monochromeDisplay.Generate();
                pictureBox1.Invalidate();
            }
        }
    }
}
