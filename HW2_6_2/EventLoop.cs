using System;
using System.Threading;

/// <summary>
/// Global namespace.
/// </summary>
namespace HW2_6_2
{
    /// <summary>
    /// Class with implementation message loop.
    /// </summary>
    public class EventLoop
    {
        /// <summary>
        /// Action handler left.
        /// </summary>
        public event Action<object, EventArgs> LeftHandler = (sender, arguments) => { };

        /// <summary>
        /// Action handler right.
        /// </summary>
        public event Action<object, EventArgs> RightHandler = (sender, arguments) => { };

        /// <summary>
        /// Action handler up.
        /// </summary>
        public event Action<object, EventArgs> UpHandler = (sender, arguments) => { };

        /// <summary>
        /// Action handler right.
        /// </summary>
        public event Action<object, EventArgs> DownHandler = (sender, arguments) => { };

        /// <summary>
        /// Action handler timer.
        /// </summary>
        public event Action<object, EventArgs> TimerHandler = (sender, arguments) => { };

        /// <summary>
        /// Gets or sets delta time in milliseconds.
        /// </summary>
        public int DeltaTime { get; set; } = 200;

        /// <summary>
        /// Infinity message loop.
        /// </summary>
        public void Run()
        {
            bool exitFlag = false;

            while (!exitFlag)
            {
                Thread.Sleep(this.DeltaTime);

                this.TimerHandler(this, EventArgs.Empty);

                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        this.LeftHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.RightArrow:
                        this.RightHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.UpArrow:
                        this.UpHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.DownArrow:
                        this.DownHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.Escape:
                        exitFlag = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
