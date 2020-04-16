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
/// Global namespace.
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

            this.Resize += new System.EventHandler(this.ResizeEvent);
        }

        /// <summary>
        /// Timer event.
        /// </summary>
        /// <param name="sender">Sender control (timer).</param>
        /// <param name="arguments">Event arguments.</param>
        private void UpdateEvent(object sender, EventArgs arguments)
        {
            this.RedrawElements();
        }

        /// <summary>
        /// Resize event.
        /// </summary>
        /// <param name="sender">Sender control (Main window).</param>
        /// <param name="arguments">Event arguments.</param>
        private void ResizeEvent(object sender, EventArgs arguments)
        {
            this.RedrawElements();
        }

        /// <summary>
        /// Redraw elements function.
        /// </summary>
        private void RedrawElements()
        {
            this.progressBarSecond.Value = (DateTime.Now.Second * 1000) + DateTime.Now.Millisecond;

            var size = this.groupBoxClock.Size;
            var center = new Point(size.Width / 2, size.Height / 2);

            int borderSize = 20;
            int clockBorderSize = 10;
            int innerCircleSize = Math.Min(size.Width, size.Height) - borderSize;
            int clockCircleSize = innerCircleSize - clockBorderSize;

            using (var clockGraphics = this.groupBoxClock.CreateGraphics())
            {
                clockGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                var grayBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
                var darkBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkGray);

                clockGraphics.FillRectangle(grayBrush, new Rectangle(0, 0, size.Width, size.Height));

                var circleRectangle = new Rectangle(
                    center.X - (innerCircleSize / 2),
                    center.Y - (innerCircleSize / 2),
                    innerCircleSize,
                    innerCircleSize);
                clockGraphics.FillEllipse(darkBrush, circleRectangle);

                grayBrush.Dispose();
                darkBrush.Dispose();
            }
        }
    }
}
