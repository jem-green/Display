﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;
using Console = DisplayLibrary.Console;

namespace DisplayForm
{
    public partial class DisplayColour : Form
    {
        DisplayLibrary.ColourTextDisplay colourDisplay;
        KeyboardMatrix _matrix;

        public DisplayColour()
        {
            InitializeComponent();

            colourDisplay = new DisplayLibrary.ColourTextDisplay(32, 32, 2, 1);

            ROMFont rasterFont = new ROMFont();
            string path = @"C:\SOURCE\GIT\cs.net\Display\FontLibrary";
            string fileName = "IBM_PC_V1_8x8.bin";
            string fileNamePath = Path.Combine(path, fileName);
            rasterFont.Load(fileNamePath);

            colourDisplay.Font = rasterFont;
            colourDisplay.Set(0, 0);
            colourDisplay.ForegroundColour = new Colour(0, 0, 0); //(byte)TextMode.ConsoleColour.Black;
            colourDisplay.BackgroundColour = new Colour(255, 255, 255);//(byte)TextMode.ConsoleColour.White;
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
                //colourDisplay.Write(key, (byte)TextMode.ConsoleColour.Blue, (byte)TextMode.ConsoleColour.Green);
                colourDisplay.Write(key, new Colour(0,0,255), new Colour(0,255,0));
                pictureBox1.Invalidate();
            }
        }
    }
}
