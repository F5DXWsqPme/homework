using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        private CalculatorState state;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            int displaySize = 12;

            this.state = new CalculatorState(displaySize);
            this.outputLabel.Text = this.state.GetOutputString();
        }

        /// <summary>
        /// Button click callback.
        /// </summary>
        /// <param name="sender">Sender object (NoFocusButton).</param>
        /// <param name="arguments">Callback arguments.</param>
        private void ButtonClicked(object sender, EventArgs arguments)
        {
            NoFocusButton button = sender as NoFocusButton;

            this.state.Update(button.Text);
            this.outputLabel.Text = this.state.GetOutputString();
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
