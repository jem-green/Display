using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;

namespace DisplayTest
{
    // A test form to display a Enhanced Text Display 4-bit colour

    public partial class DisplayEnhancedText : Form
    {
        DisplayLibrary.EnhancedTextDisplay colourDisplay;
        KeyboardMatrix _matrix;

        public DisplayEnhancedText()
        {
            InitializeComponent();

            colourDisplay = new DisplayLibrary.EnhancedTextDisplay(16, 16, 2, 1);

            ROMFont rasterFont = new ROMFont();
            string path = @"C:\SOURCE\GIT\cs.net\Display\DisplayTest";
            string fileName = "IBM_XT286_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            colourDisplay.Font = rasterFont;
            colourDisplay.Set(0, 0);
            colourDisplay.ForegroundColour = new SolidColour(255, 255, 255);    // White foreground 
            colourDisplay.BackgroundColour = new SolidColour(0, 0, 0);          // Black background;
            colourDisplay.Write("HELLO THIS SHOULD WRAP AROUND");
            _matrix = new KeyboardMatrix();
            pictureBox1.Select();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap b = colourDisplay.Bitmap;
            g.DrawImageUnscaled(b, 0, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 400; i++)
                {
                    colourDisplay.Write((byte)'e');
                }
                colourDisplay.Generate();
                pictureBox1.Invalidate();
            }
            else
            {
                colourDisplay.Write("Hello");
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            byte key = _matrix.ToASCII(e.KeyValue, e.Shift, e.Control, e.Alt);
            if (key > 0)
            {
                colourDisplay.Write(key, new SolidColour(0,0,255), new SolidColour(0,255,0));   // Blue foreground, Green background
                pictureBox1.Invalidate();
            }
        }
    }
}
