using System;
using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    /// <summary>
    /// Class with main function.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="arguments">Arguments array.</param>
        private static void Main(string[] arguments)
        {
            Matrix[] matrices = new Matrix[2];

            if (arguments.Length != 2)
            {
                Console.WriteLine("Wrong arguments");
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                (bool success, Matrix matrix) = LoadMatrix(arguments[i]);

                if (success)
                {
                    matrices[i] = matrix;
                }
                else
                {
                    Console.WriteLine("Wrong arguments");
                    return;
                }
            }

            if (matrices[0].Fields.GetLength(1) != matrices[1].Fields.GetLength(0))
            {
                Console.WriteLine("Wrong arguments");
                return;
            }

            Console.Write("No operations: ");
            PrintPerformance(() =>
            {
            });

            Console.Write("Without multithreading: ");
            PrintPerformance(() =>
            {
                Matrix result = matrices[0] * matrices[1];
            });

            Matrix.UseMultithreading = true;
            Console.Write("With multithreading: ");
            PrintPerformance(() =>
            {
                Matrix result = matrices[0] * matrices[1];
            });
        }

        private static void PrintPerformance(Action function)
        {
            double minimalTime = 0.2;
            int multiplyer = 10;
            long numberOfIterations = 1;
            TimeSpan deltaTime;

            do
            {
                DateTime startTime = DateTime.Now;

                for (int i = 0; i < numberOfIterations; i++)
                {
                    function();
                }

                DateTime endTime = DateTime.Now;
                deltaTime = endTime - startTime;
                numberOfIterations *= multiplyer;
            }
            while (deltaTime.TotalSeconds < minimalTime);

            numberOfIterations /= multiplyer;

            double nanosecondsRate = 1e9;
            double microsecondsRate = 1e6;
            double milisecondsRate = 1e3;

            double maximalNumber = 1e3;

            double result = deltaTime.TotalSeconds * (nanosecondsRate / numberOfIterations);

            if (result < maximalNumber)
            {
                Console.Write(result.ToString("F"));
                Console.WriteLine(" ns");
                return;
            }

            result = deltaTime.TotalSeconds * (microsecondsRate / numberOfIterations);

            if (result < maximalNumber)
            {
                Console.Write(result.ToString("F"));
                Console.WriteLine(" mcs");
                return;
            }

            result = deltaTime.TotalSeconds * (milisecondsRate / numberOfIterations);

            if (result < maximalNumber)
            {
                Console.Write(result.ToString("F"));
                Console.WriteLine(" ms");
                return;
            }

            result = deltaTime.TotalSeconds * numberOfIterations;

            Console.Write(result.ToString("F"));
            Console.WriteLine(" s");
        }

        private static (bool, Matrix) LoadMatrix(string fileName)
        {
            Matrix matrix;

            if (!System.IO.File.Exists(fileName))
            {
                return (false, null);
            }

            using (System.IO.StreamReader reader = System.IO.File.OpenText(fileName))
            {
                string fileString = reader.ReadToEnd();
                string[] numbersInString = fileString.Split(' ', '\n', '\r', '\t');

                numbersInString = numbersInString.Where((element) => !string.IsNullOrEmpty(element)).ToArray();

                if (numbersInString.Length < 2)
                {
                    return (false, null);
                }

                if (int.TryParse(numbersInString[0], out int width) &&
                    int.TryParse(numbersInString[1], out int height))
                {
                    matrix = new Matrix(width, height);
                }
                else
                {
                    return (false, null);
                }

                if (numbersInString.Length != matrix.Fields.Length + 2)
                {
                    return (false, null);
                }

                for (int currentPosition = 0;
                    currentPosition < matrix.Fields.Length;
                    currentPosition++)
                {
                    if (int.TryParse(numbersInString[currentPosition + 2], out int value))
                    {
                        int row = currentPosition / matrix.Fields.GetLength(1);
                        int column = currentPosition / matrix.Fields.GetLength(1);

                        matrix.Fields[row, column] = value;
                    }
                    else
                    {
                        return (false, null);
                    }
                }

                return (true, matrix);
            }
        }
    }
}
