using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Solution
{
    /// <summary>
    /// Class with main function.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="arguments">Program arguments.</param>
        [STAThread]
        public static void Main(string[] arguments)
        {
           // if (arguments.Length == 2 && int.TryParse(arguments[1], out int fieldSize))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow(/*fieldSize*/2));
            }
            //else
            {
                MessageBox.Show("Wrong arguments", "Error");
            }
        }
    }
}
