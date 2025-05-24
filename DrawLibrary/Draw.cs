using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DisplayLibrary;
using RasterFontLibrary;
using static DisplayLibrary.Storage;

namespace DrawLibrary
{
    public class Draw
    {
        #region Fields

        private int _x;
        private int _y;

        public struct Node
        {
            public int X;
            public int Y;

            public Node(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class Edge
        {
            public double YMin;
            public double YMax;
            public double XMin;
            public double XMax;
            public double Slope;

            public Edge(Node p1, Node p2)
            {
                if (p1.Y < p2.Y)
                {
                    YMin = p1.Y;
                    YMax = p2.Y;
                    XMin = p1.X;
                    XMax = p2.X;
                }
                else
                {
                    YMin = p2.Y;
                    YMax = p1.Y;
                    XMin = p2.X;
                    XMax = p1.X;
                }
                Slope = (double)(p2.X - p1.X) / (double)(p2.Y - p1.Y);
            }
        }

        private IGraphic _display;

        #endregion
        #region Constructors

        public Draw(IGraphic display)
        {
            _display = display;
            _x = 0;
            _y = 0;
        }

        #endregion
        #region Properties

        public int X
        {
            set
            {
                _x = value;
            }
            get
            {
                return (_x);
            }
        }

        public int Y
        {
            set
            {
                _y = value;
            }
            get
            {
                return (_y);
            }
        }

        #endregion
        #region Methods

        public void LoadRasterFont(string path, string fontFile)
        {
            // Load the font file and parse the data
            // This is a placeholder for the actual implementation
            // You would need to read the font file and populate the _fontData dictionary

            throw new NotImplementedException("Font loading not implemented yet.");
        }

        public void DrawRasterText(string text, RasterFont font, int x, int y, Colour colour)
        {
            // Draw the text using the loaded font data
            // This is a placeholder for the actual implementation
            // You would need to iterate through the characters in the text and draw them using the font data

            int x1 = x;
            int y1 = y;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c < font.Characters.Length)
                {
                    RasterFont.Character character = font.Characters[c];
                    if (character.Width == 0)
                    {
                        // Potentially do this at load
                        character.Width = font.Horizontal;
                    }
                    for (int j = 0; j < character.Width; j++)
                    {
                        for (int k = 0; k < font.Vertical; k++)
                        {
                            if ((character.Image[k * character.Width + j] & 1) != 0)
                            {
                                Pixel(x + j, y + k, colour);
                            }
                        }
                    }
                    x += character.Width;
                }
            }
            _display.Generate();
            //_display.PartialGenerate(x1,y1,x,y1 + font.Vertical);

        }

        #endregion
        #region Private

        /// <summary>
        /// Draw a pixel at the specified coordinates using the specified color
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        private void Pixel(int x, int y, Colour colour)
        {
            // Need to consider what to do about clipping
            // Are we going to allow for shapes to be drawn outside the screen?
            // Also not sure where the dithering / anti-aliasing will go

            _display.Write(x, y, colour);

        }

        #endregion
    }
}
