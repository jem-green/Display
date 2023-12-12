using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ConsoleLibrary;
using Console = ConsoleLibrary.Console;

namespace ConsoleFrom
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            displayBox1.Select();
        }

        private void displayBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void displayBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 400; i++)
                {
                    displayBox1.Write((byte)'e');
                }
                //terminal.Generate();
                displayBox1.Invalidate();
            }
            else
            {
                displayBox1.Write("Hello");
                displayBox1.Invalidate();
            }
        }
    }
}
