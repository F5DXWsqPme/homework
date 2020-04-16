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

            this.RedrawElements();
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
        /// Create rectangle around circle.
        /// </summary>
        /// <param name="center">Circle center.</param>
        /// <param name="radius">Circle radius.</param>
        /// <returns>Rectangle.</returns>
        private Rectangle CircleRectangle(Point center, int radius)
        {
            return new Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
        }

        /// <summary>
        /// Redraw elements function.
        /// </summary>
        private void RedrawElements()
        {
            this.progressBarSecond.Value = (DateTime.Now.Second * 1000) + DateTime.Now.Millisecond;

            var clockGraphics = this.groupBoxClock.GetGraphics();

            clockGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var size = this.groupBoxClock.Size;
            var center = new Point(size.Width / 2, size.Height / 2);
            int borderSize = 20;
            int clockBorderSize = 10;
            int innerCircleSize = Math.Min(size.Width, size.Height) - borderSize;
            int clockCircleSize = innerCircleSize - clockBorderSize;

            clockGraphics.FillRectangle(Brushes.Gray, new Rectangle(0, 0, size.Width, size.Height));
            clockGraphics.FillEllipse(Brushes.DarkGray, this.CircleRectangle(center, innerCircleSize / 2));

            int hourCircleRadius = clockCircleSize / 60;
            int hourCircleCenterDistance = (clockCircleSize / 2) - (2 * hourCircleRadius);
            double angle = 0;
            for (int i = 0; i < 12; i++, angle += 2 * Math.PI / 12)
            {
                var circleCenter = new Point(
                    (int)(center.X + (hourCircleCenterDistance * Math.Sin(angle))),
                    (int)(center.Y + (hourCircleCenterDistance * Math.Cos(angle))));

                clockGraphics.FillEllipse(Brushes.White, this.CircleRectangle(circleCenter, hourCircleRadius));
            }

            float hourWidth = 7;
            var hourPen = new Pen(Brushes.Red, hourWidth);
            int hourLength = (int)(clockCircleSize * 0.5 / 2);
            double hourAngle = 2 * Math.PI * DateTime.Now.Hour / 12;
            var hourPoint = new Point(
                (int)(center.X + (hourLength * Math.Sin(hourAngle))),
                (int)(center.Y - (hourLength * Math.Cos(hourAngle))));

            clockGraphics.DrawLine(hourPen, center, hourPoint);
            hourPen.Dispose();

            float minuteWidth = 4;
            var minutePen = new Pen(Brushes.Aqua, minuteWidth);
            int minuteLength = (int)(clockCircleSize * 0.75 / 2);
            double minuteAngle = 2 * Math.PI * DateTime.Now.Minute / 60;
            var minutePoint = new Point(
                (int)(center.X + (minuteLength * Math.Sin(minuteAngle))),
                (int)(center.Y - (minuteLength * Math.Cos(minuteAngle))));

            clockGraphics.DrawLine(minutePen, center, minutePoint);
            minutePen.Dispose();

            float secondWidth = 2;
            var secondPen = new Pen(Brushes.Yellow, secondWidth);
            int secondLength = (int)(clockCircleSize * 0.9 / 2);
            double secondAngle = 2 * Math.PI * DateTime.Now.Second / 60;
            var secondPoint = new Point(
                (int)(center.X + (secondLength * Math.Sin(secondAngle))),
                (int)(center.Y - (secondLength * Math.Cos(secondAngle))));

            clockGraphics.DrawLine(secondPen, center, secondPoint);
            secondPen.Dispose();

            int centerDotRadius = innerCircleSize / 30;

            clockGraphics.FillEllipse(Brushes.LightCyan, this.CircleRectangle(center, centerDotRadius));

            this.Refresh();
        }

        /// <summary>
        /// Group box with double bufferization.
        /// </summary>
        private class DoubleBufferedGroupBox : GroupBox
        {
            private BufferedGraphicsContext graphicsContext;
            private BufferedGraphics graphicsBuffer;

            /// <summary>
            /// Initializes a new instance of the <see cref="DoubleBufferedGroupBox"/> class.
            /// </summary>
            public DoubleBufferedGroupBox()
            {
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

                this.Paint += new PaintEventHandler(this.PaintEvent);
                this.Resize += new EventHandler(this.ResizeEvent);

                this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

                this.graphicsContext = BufferedGraphicsManager.Current;
                this.graphicsContext.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
                this.graphicsBuffer = this.graphicsContext.Allocate(
                    this.CreateGraphics(),
                    new Rectangle(0, 0, this.Width, this.Height));
            }

            /// <summary>
            /// Gets buffered graphics for drawing.
            /// </summary>
            /// <returns>Buffered graphics.</returns>
            public Graphics GetGraphics()
            {
                return this.graphicsBuffer.Graphics;
            }

            /// <summary>
            /// Redraw event.
            /// </summary>
            /// <param name="sender">Sender object.</param>
            /// <param name="arguments">Event arguments.</param>
            private void PaintEvent(object sender, PaintEventArgs arguments)
            {
                this.graphicsBuffer.Render(arguments.Graphics);
            }

            /// <summary>
            /// Resize event.
            /// </summary>
            /// <param name="sender">Sender object.</param>  !
            /// <param name="arguments">Event arguments.</param>
            private void ResizeEvent(object sender, EventArgs arguments)
            {
                this.graphicsContext.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
                if (this.graphicsBuffer != null)
                {
                    this.graphicsBuffer.Dispose();
                }

                this.graphicsBuffer = this.graphicsContext.Allocate(
                    this.CreateGraphics(),
                    new Rectangle(0, 0, this.Width, this.Height));
            }
        }
    }
}
