using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Global namespace.
/// </summary>
namespace Solution
{
    /// <summary>
    /// Class with main function.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main function.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
