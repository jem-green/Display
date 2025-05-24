using System;
using System.IO;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using RasterFontLibrary;


namespace FontTest
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            //CharacterCreateTest();
            CharacterDrawTest();
        }

        public static void CharacterCreateTest()
        {
            RasterFont rf = new RasterFont();
            rf.Horizontal = 8;
            rf.Vertical = 8;
            RasterFont.Character c = new RasterFont.Character();
            c.Index = 65;   // Need to consider what we do about mapping the index to the character
            c.Width = 8;
            RasterFont.BitMap i = new RasterFont.BitMap(8,8);

            i.AddRow(0);    // 0 0 0 0 0 0 0 0
            i.AddRow(60);   // 0 0 1 1 1 1 0 0
            i.AddRow(66);   // 0 1 0 0 0 0 1 0
            i.AddRow(66);   // 0 1 0 0 0 0 1 0
            i.AddRow(124);  // 0 1 1 1 1 1 0 0
            i.AddRow(66);   // 0 1 0 0 0 0 1 0
            i.AddRow(66);   // 0 1 0 0 0 0 1 0
            i.AddRow(0);    // 0 0 0 0 0 0 0 0
            c.AddImage(i);

            // or

            c.AddImage(new byte[]{ 0, 60, 66, 66, 124, 66, 66, 0});

            rf.Add(c);
        }

        public static void CharacterDrawTest()
        {
            RasterFont rf = new RasterFont();
            rf.Horizontal = 8;
            rf.Vertical = 8;
            RasterFont.Character c = new RasterFont.Character();
            c.Index = 65;   // Need to consider what we do about mapping the index to the character
            c.Width = 8;
            RasterFont.BitMap i = new RasterFont.BitMap(8, 8);

            i.AddRow(0);    // 0 0 0 0 0 0 0 0
            i.AddRow(60);   // 0 0 1 1 1 1 0 0
            i.AddRow(66);   // 0 1 0 0 0 0 1 0
            i.AddRow(66);   // 0 1 0 0 0 0 1 0
            i.AddRow(124);  // 0 1 1 1 1 1 0 0
            i.AddRow(66);   // 0 1 0 0 0 0 1 0
            i.AddRow(66);   // 0 1 0 0 0 0 1 0
            i.AddRow(0);    // 0 0 0 0 0 0 0 0
            c.AddImage(i);

            rf.Add(c);

            DisplayLibrary.VibrantGraphicsDisplay vgd = new DisplayLibrary.VibrantGraphicsDisplay(800, 600);
            DrawLibrary.Draw draw = new DrawLibrary.Draw(vgd);
            vgd.Clear();

            draw.DrawRasterText("\0",rf,400, 300, new DisplayLibrary.Colour(255, 255, 255));

            string filename = "CharacterDrawTest";
            vgd.Generate();
            vgd.Save(filename + ".png");

        }

        //public static void CreateTest()
        //{
        //    RasterFont rf = new RasterFont();
        //    rf.Load(@"C:\SOURCE\GIT\cs.net\RasterFont\FontConverter\ROM\", "spectrum_x1.png");

        //VectorFont vf = new VectorFont();
        //vf.Chars = 0;   // The fastest option is to have 255 chars, and not search
        //vf.Horizontal = 32;
        //vf.Vertical = 32;

        //// Test drawing a 'A' character

        //VectorFont.Character c = new Character();
        //c.Width = 32;
        //VectorFont.Stroke s = new VectorFont.Stroke(VectorFont.Stroke.StrokeType.Move,new VectorFont.Coordinate(0, 32));
        //c.AddStroke(s);
        //s = new VectorFont.Stroke(VectorFont.Stroke.StrokeType.Move, new VectorFont.Coordinate(16, 0));
        //c.AddStroke(s);
        //s = new VectorFont.Stroke(VectorFont.Stroke.StrokeType.Move, new VectorFont.Coordinate(32, 32));
        //c.AddStroke(s);
        //s = new VectorFont.Stroke(VectorFont.Stroke.StrokeType.Move, new VectorFont.Coordinate(8, 16));
        //c.AddStroke(s);

        //vf.Add(c);
        //}

        public static void VFCharacterDrawTest()
        {

            //VectorFont vf = new VectorFont();
            //vf.Chars = 0;   // The fastest option is to have 255 chars, and not search
            //vf.Horizontal = 32;
            //vf.Vertical = 32;

            // Test drawing a 'A' character

            //VectorFont.Character c = new Character();
            //c.Width = 32;
            //VectorFont.Stroke s = new VectorFont.Stroke(VectorFont.Stroke.StrokeType.Move, new VectorFont.Coordinate(0, 32));
            //c.AddStroke(s);
            //s = new VectorFont.Stroke(VectorFont.Stroke.StrokeType.Draw, new VectorFont.Coordinate(16, 0));
            //c.AddStroke(s);
            //s = new VectorFont.Stroke(VectorFont.Stroke.StrokeType.Draw, new VectorFont.Coordinate(32, 32));
            //c.AddStroke(s);
            //s = new VectorFont.Stroke(VectorFont.Stroke.StrokeType.Draw, new VectorFont.Coordinate(8, 16));
            //c.AddStroke(s);
            //vf.Add(c);

            //DisplayLibrary.VibrantGraphicsDisplay vgd = new DisplayLibrary.VibrantGraphicsDisplay(800, 600);
            //DrawLibrary.Draw draw = new DrawLibrary.Draw(vgd);
            //vgd.Clear();

            //VectorFont.Coordinate from = c.Strokes[0].Coordinate;
            //VectorFont.Coordinate to;

            //foreach (VectorFont.Stroke stroke in c.Strokes)
            //{
            //    if (stroke.Type == VectorFont.Stroke.StrokeType.Move)
            //    {
            //        Console.WriteLine("Move to: " + stroke.Type + " " + stroke.Coordinate.X + "," + stroke.Coordinate.Y);
            //        from = stroke.Coordinate;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Draw to: " + stroke.Type + " " + stroke.Coordinate.X + "," + stroke.Coordinate.Y);
            //        to = stroke.Coordinate;
            //        draw.DrawLine(400 + to.X, 300 + to.Y, 400 + from.X, 300 + from.Y, new DisplayLibrary.Colour(255, 255, 255));
            //        from = to;
            //    }
            //}

            //string filename = "VFCharacterDrawTest";
            //vgd.Generate();
            //vgd.Save(filename + ".png");

        }

        //    public static void HFMultiDraw()
        //    {
        //        DisplayLibrary.VibrantGraphicsDisplay vgd = new DisplayLibrary.VibrantGraphicsDisplay(800, 600);
        //        DrawLibrary.Draw draw = new DrawLibrary.Draw(vgd);
        //        vgd.Clear();

        //        DirectoryInfo di = new DirectoryInfo(@"C:\SOURCE\GIT\cs.net\VectorFont\HersheyFontLibrary\JHF");
        //        FileInfo[] fi = di.GetFiles("*.jhf");

        //        foreach (FileInfo file in fi)
        //        {
        //            HersheyFontLibrary.HersheyFont font = new HersheyFontLibrary.HersheyFont();
        //            string path = file.DirectoryName;
        //            string filename = file.Name;

        //            font.Name = filename;
        //            font.Path = path;
        //            font.Load(path, filename);

        //            vgd.Clear();

        //            int x = 20;
        //            int y = 20;
        //            int offset = 20;
        //            int vertical = 32;
        //            int horizontal = 32;

        //            foreach (Glyph glyph in font)
        //            {
        //                HersheyFontLibrary.Coordinate from = new HersheyFontLibrary.Coordinate(0, 0);
        //                if (glyph.Count > 0)
        //                {
        //                    from = glyph[0].Coordinate;
        //                }

        //                HersheyFontLibrary.Coordinate to = new HersheyFontLibrary.Coordinate(0, 0);
        //                bool penUp = false;
        //                bool lastPenUp = true;

        //                foreach (HersheyFontLibrary.Stroke stroke in glyph)
        //                {
        //                    penUp = stroke.PenUp;

        //                    if (lastPenUp == false && penUp == true)
        //                    {
        //                        Console.WriteLine("Pen up");
        //                    }
        //                    else if (lastPenUp == true && penUp == false)
        //                    {
        //                        Console.WriteLine("Move to: " + stroke.PenUp + " " + stroke.Coordinate.X + "," + stroke.Coordinate.Y);
        //                        from = stroke.Coordinate;
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("Draw to: " + stroke.PenUp + " " + stroke.Coordinate.X + "," + stroke.Coordinate.Y);
        //                        to = stroke.Coordinate;
        //                        draw.DrawLine(x + to.X, y + to.Y, x + from.X, y + from.Y, new DisplayLibrary.Colour(255, 255, 255));
        //                        from = to;
        //                    }
        //                    lastPenUp = penUp;
        //                }

        //                x = x + horizontal; //+ - glyph.Left + glyph.Right + horizontal;
        //                if (x > 800 - offset)
        //                {
        //                    x = offset;
        //                    y = y + vertical;
        //                }
        //            }

        //            vgd.Generate();
        //            vgd.Save(filename + ".png");
        //        }
        //    }

        //    public static void HFCreateTest()
        //    {
        //        // Test drawing a 'A' character
        //        HersheyFont font = new HersheyFont();
        //        Glyph glyph = new Glyph();
        //        glyph.Index = 65;   // Need to consider what we do about mapping the index to the character
        //        glyph.Left = 0;
        //        glyph.Right = 10;
        //        glyph.Add(new HersheyFontLibrary.Stroke(new HersheyFontLibrary.Coordinate(5, 10)));
        //        glyph.Add(new HersheyFontLibrary.Stroke(new HersheyFontLibrary.Coordinate(10, 0)));
        //        glyph.Add(new HersheyFontLibrary.Stroke(new HersheyFontLibrary.Coordinate(3, 5)));
        //        font.Add(glyph);
        //    }

        //    public static void HFDrawCharacterTest()
        //    {
        //        // Test drawing a 'A' character
        //        HersheyFont font = new HersheyFont();
        //        Glyph glyph = new Glyph();
        //        glyph.Index = 65;   // Need to consider what we do about mapping the index to the character
        //        glyph.Left = 0;
        //        glyph.Right = 10;
        //        glyph.Add(new HersheyFontLibrary.Stroke(new HersheyFontLibrary.Coordinate(5, 10)));
        //        glyph.Add(new HersheyFontLibrary.Stroke(new HersheyFontLibrary.Coordinate(10, 0)));
        //        glyph.Add(new HersheyFontLibrary.Stroke(new HersheyFontLibrary.Coordinate(3, 5)));
        //        font.Add(glyph);

        //        DisplayLibrary.VibrantGraphicsDisplay vgd = new DisplayLibrary.VibrantGraphicsDisplay(800, 600);
        //        DrawLibrary.Draw draw = new DrawLibrary.Draw(vgd);
        //        vgd.Clear();

        //        HersheyFontLibrary.Coordinate from = glyph[0].Coordinate;
        //        HersheyFontLibrary.Coordinate to;
        //        bool penUp = false;
        //        bool lastPenUp = true;

        //        foreach (HersheyFontLibrary.Stroke stroke in glyph)
        //        {
        //            penUp = stroke.PenUp;

        //            if (lastPenUp == false && penUp == true)
        //            {
        //                Console.WriteLine("Pen up");
        //            }
        //            else if (lastPenUp == true && penUp == false)
        //            {
        //                Console.WriteLine("Move to: " + stroke.PenUp + " " + stroke.Coordinate.X + "," + stroke.Coordinate.Y);
        //                from = stroke.Coordinate;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Draw to: " + stroke.PenUp + " " + stroke.Coordinate.X + "," + stroke.Coordinate.Y);
        //                to = stroke.Coordinate;
        //                draw.DrawLine(400 + to.X, 300 + to.Y, 400 + from.X, 300 + from.Y, new DisplayLibrary.Colour(255, 255, 255));
        //                from = to;
        //            }

        //            lastPenUp = penUp;
        //        }

        //        string filename = "HERSHEY";
        //        vgd.Generate();
        //        vgd.Save(filename + ".png");

        //    }

        //    public static void HFLoadTest()
        //    {
        //        // Test drawing a 'A' character
        //        string path = @"C:\SOURCE\GIT\cs.net\VectorFont\HersheyFontLibrary\JHF";
        //        string filename = "ROMANS.jfh";
        //        HersheyFont font = new HersheyFont();
        //        font.Name = filename;
        //        font.Path = path;
        //        font.Load(path, filename);
        //        // Now save the file and compare it to the original
        //        filename = "HERSHEY.jfh";
        //        font.Save(path, filename);
        //    }

        //    public static void HFDrawTest()
        //    {
        //        // Test drawing a 'A' character

        //        string path = @"C:\SOURCE\GIT\cs.net\VectorFont\HersheyFontLibrary\JHF";
        //        string filename = "ROMANS.jfh";
        //        HersheyFont font = new HersheyFont();
        //        font.Name = filename;
        //        font.Path = path;
        //        font.Load(path, filename);
        //        Glyph glyph = font[1];
        //        HersheyFontLibrary.Coordinate from = glyph[0].Coordinate;
        //        HersheyFontLibrary.Coordinate to;
        //        bool penUp = false;
        //        bool lastPenUp = true;

        //        DisplayLibrary.VibrantGraphicsDisplay vgd = new DisplayLibrary.VibrantGraphicsDisplay(800, 600);
        //        DrawLibrary.Draw draw = new DrawLibrary.Draw(vgd);
        //        vgd.Clear();

        //        foreach (HersheyFontLibrary.Stroke stroke in glyph)
        //        {
        //            penUp = stroke.PenUp;

        //            if (lastPenUp == false && penUp == true)
        //            {
        //                Console.WriteLine("Pen up");
        //            }
        //            else if (lastPenUp == true && penUp == false)
        //            {
        //                Console.WriteLine("Move to: " + stroke.PenUp + " " + stroke.Coordinate.X + "," + stroke.Coordinate.Y);
        //                from = stroke.Coordinate;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Draw to: " + stroke.PenUp + " " + stroke.Coordinate.X + "," + stroke.Coordinate.Y);
        //                to = stroke.Coordinate;
        //                draw.DrawLine(400 + to.X, 300 + to.Y, 400 + from.X, 300 + from.Y, new DisplayLibrary.Colour(255, 255, 255));
        //                from = to;
        //            }

        //            lastPenUp = penUp;
        //        }

        //        vgd.Generate();
        //        vgd.Save(filename + ".png");
        //    }

        //}


        //    public void ReadFnt()
        //    {

        //                public enum FontFamily
        //{
        //    Default = 0,
        //    Roman = 1,
        //    Modern = 2,
        //    Script = 3,
        //    Decorative = 4
        //}

        //public enum FontType
        //{
        //    Device = 0,
        //    Raster = 1,
        //    TrueType = 2,
        //    Vector = 3
        //}

        //public enum FontWeight
        //{
        //    Thin = 100,
        //    ExtraLight = 200,
        //    Light = 300,
        //    Normal = 400,
        //    Medium = 500,
        //    SemiBold = 600,
        //    Bold = 700,
        //    ExtraBold = 800,
        //    Black = 900
        //}

        //public enum FontPitch
        //{
        //    Default = 0,
        //    Fixed = 1,
        //    Variable = 2
        //}

        //public static void Main(string[] args)
        //{
        //    string path = @"C:\SOURCE\GIT\cs.net\VectorFont\MicrosoftVectorFont\FNT";
        //    string filename = "ROMANS";
        //    string extension = ".fnt";
        //    string filenamePath = path + Path.DirectorySeparatorChar + filename + extension;
        //    using (BinaryReader binaryReader = new BinaryReader(File.Open(filenamePath, FileMode.Open, FileAccess.Read)))
        //    {
        //        binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
        //        Int16 dfVersion = binaryReader.ReadInt16();
        //        Int32 dfSize = binaryReader.ReadInt32();
        //        byte[] dfCopyright = binaryReader.ReadBytes(60);
        //        string dfCopyrightString = System.Text.Encoding.ASCII.GetString(dfCopyright);
        //        byte[] dfType = binaryReader.ReadBytes(2);
        //        FontType dfFontType = dfType[0] == 0 ? FontType.Device : dfType[0] == 1 ? FontType.Raster : dfType[0] == 2 ? FontType.TrueType : FontType.Vector;
        //        Int16 dfPoints = binaryReader.ReadInt16();
        //        Int16 dfVertRes = binaryReader.ReadInt16();
        //        Int16 dfHorizRes = binaryReader.ReadInt16();
        //        Int16 dfAscent = binaryReader.ReadInt16();
        //        Int16 dfInternalLeading = binaryReader.ReadInt16();
        //        Int16 dfExternalLeading = binaryReader.ReadInt16();
        //        byte dfItalic = binaryReader.ReadByte();
        //        bool dfFontTyoe = dfItalic == 1;
        //        byte dfUnderline = binaryReader.ReadByte();
        //        bool dfFontUnderline = dfUnderline == 1;
        //        byte dfStrikeOut = binaryReader.ReadByte();
        //        bool dfFontStrikeOut = dfStrikeOut == 1;
        //        Int16 dfWeight = binaryReader.ReadInt16();
        //        byte dfCharset = binaryReader.ReadByte();
        //        Int16 dfPixWidth = binaryReader.ReadInt16();
        //        Int16 dfPixHeight = binaryReader.ReadInt16();
        //        byte[] dfPitchAndFamily = binaryReader.ReadBytes(1);
        //        FontPitch dfFontPitch = (FontPitch)(dfPitchAndFamily[0] & 0x03);
        //        FontFamily dfFontFamily = (FontFamily)((dfPitchAndFamily[0] & 0xF0) >> 4);
        //        Int16 dfAvgWidth = binaryReader.ReadInt16();
        //        Int16 dfMaxWidth = binaryReader.ReadInt16();
        //        byte dfFirstChar = binaryReader.ReadByte();
        //        byte dfLastChar = binaryReader.ReadByte();
        //        byte dfDefaultChar = binaryReader.ReadByte();
        //        byte dfBreakChar = binaryReader.ReadByte();
        //        Int16 dfWidthBytes = binaryReader.ReadInt16();
        //        Int32 dfDevice = binaryReader.ReadInt32();
        //        Int32 dfFace = binaryReader.ReadInt32();
        //        Int32 dfBitsPointer = binaryReader.ReadInt32();
        //        Int32 dfBitsOffset = binaryReader.ReadInt32();
        //        int length = 0;
        //        if (dfFontType == FontType.Raster)
        //        {
        //            // Read the raster font data
        //            int chars = dfLastChar - dfFirstChar + 1;
        //            length = chars * dfWidthBytes * dfPixHeight; // Needs to be rounded up to the nearest word
        //            length = (length + 1) & ~1; // Round up to the nearest word
        //        }
        //        byte[] dfCharOffset = binaryReader.ReadBytes(2);





        //    }

        //}



        //}
    }
}
