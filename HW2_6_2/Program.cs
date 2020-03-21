using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Global namespace.
/// </summary>
namespace HW2_6_2
{
    /// <summary>
    /// Class with main function.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public static void Main(string[] args)
        {
            var eventLoop = new EventLoop();
            using var game = new Game(eventLoop);

            eventLoop.Run();
        }
    }
}
