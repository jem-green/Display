using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new DisplayColour());
            //Application.Run(new DisplayMonochrome());
            Application.Run(new ConsoleMonochrome());
            //Application.Run(new TerminalColour());
            //Application.Run(new UserControlColour());

        }
    }
}
