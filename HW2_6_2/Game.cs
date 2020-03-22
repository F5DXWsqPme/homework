using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Global namespace.
/// </summary>
namespace HW2_6_2
{
    /// <summary>
    /// Class with implementation person move and draw frame.
    /// </summary>
    public class Game : IDisposable
    {
        private readonly Map map;
        private int x;
        private int y;
        private EventLoop eventLoop;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="eventLoop">Main event loop.</param>
        /// <param name="fileName">Name of file with map.</param>
        /// <exception cref="ArgumentException">Throws when file not exist or file wrong.</exception>
        public Game(EventLoop eventLoop, string fileName)
        {
            this.eventLoop = eventLoop;

            this.eventLoop.LeftHandler += this.OnLeft;
            this.eventLoop.RightHandler += this.OnRight;
            this.eventLoop.DownHandler += this.OnDown;
            this.eventLoop.UpHandler += this.OnUp;

            this.map = new Map(fileName);

            this.Draw();
        }

        /// <summary>
        /// Deleting message crackers from event loop
        /// </summary>
        public void Dispose()
        {
            this.eventLoop.LeftHandler -= this.OnLeft;
            this.eventLoop.RightHandler -= this.OnRight;
            this.eventLoop.DownHandler -= this.OnDown;
            this.eventLoop.UpHandler -= this.OnUp;
        }

        private void OnLeft(object sender, EventArgs arguments)
        {
            this.Move(-1, 0);
        }

        private void OnRight(object sender, EventArgs arguments)
        {
            this.Move(1, 0);
        }

        private void OnUp(object sender, EventArgs arguments)
        {
            this.Move(0, -1);
        }

        private void OnDown(object sender, EventArgs arguments)
        {
            this.Move(0, 1);
        }

        private void Move(int deltaX, int deltaY)
        {
            this.map.GetWidthAndHegth(out int width, out int heigth);

            int newX = (width + this.x + deltaX) % width;
            int newY = (heigth + this.y + deltaY) % heigth;

            if (!this.map.IsWall(newX, newY))
            {
                this.x = newX;
                this.y = newY;
                this.Draw();
            }
        }

        private void Draw()
        {
            Console.Clear();

            this.map.GetWidthAndHegth(out int width, out int heigth);

            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == this.y && j == this.x)
                    {
                        Console.Write('@');
                    }
                    else if (this.map.IsWall(j, i))
                    {
                        Console.Write('W');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
