using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DisplayLibrary;
using Console = DisplayLibrary.Console;

namespace DisplayForm
{
    public partial class UserControlColour : Form
    {
        public UserControlColour()
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
