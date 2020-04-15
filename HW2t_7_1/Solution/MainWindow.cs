using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Global namespace
/// </summary>
namespace Solution
{
    /// <summary>
    /// Main window class.
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Button click callback.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="args">Callback arguments.</param>
        private void ButtonClicked(object sender, EventArgs args)
        {
        }

        /// <summary>
        /// Button without focus.
        /// </summary>
        private class NoFocusButton : Button
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="NoFocusButton"/> class.
            /// </summary>
            public NoFocusButton()
            {
                this.SetStyle(ControlStyles.Selectable, false);
            }
        }
    }
}
