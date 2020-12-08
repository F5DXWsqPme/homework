namespace Solution
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class with main function.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main function in program.
        /// </summary>
        /// <param name="arguments">Program arguments.</param>
        private static void Main(string[] arguments)
        {
            if (arguments.Length != 1)
            {
                Console.WriteLine("Wrong arguments.");
                return;
            }

            Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");
            Console.WriteLine($"Argument:          {arguments[0]}");

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var (success, hash) = Hasher.EvaluateHash(arguments[0]);

            stopwatch.Stop();

            var elapsedTime = stopwatch.Elapsed;

            if (!success)
            {
                Console.WriteLine("Directory not found.");
                return;
            }

            Console.WriteLine($"Evaluation time:   {elapsedTime}");

            var resultArray = hash.ToArray();

            StringBuilder hexFormat = new StringBuilder(resultArray.Length * 2);

            foreach (byte element in resultArray)
            {
                hexFormat.AppendFormat("{0:x2}", element);
            }

            Console.WriteLine($"Hash:              {hexFormat}");
        }
    }
}
