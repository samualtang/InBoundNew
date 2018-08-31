using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SpecialShapeSmoke
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
            //Application.Run(new MainScreen1_new());
            Application.Run(new Form2());
        }
    }
}
