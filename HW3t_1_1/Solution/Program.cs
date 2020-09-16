using System;
using System.Diagnostics;
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
            if (arguments.Length != 2)
            {
                Console.WriteLine("Wrong arguments");
                return;
            }

            Matrix[] matrices = new Matrix[2];

            for (var i = 0; i < 2; i++)
            {
                (bool success, Matrix matrix) = LoadMatrix(arguments[i]);

                if (success)
                {
                    matrices[i] = matrix;
                }
                else
                {
                    Console.WriteLine("Wrong arguments, generating matrix");

                    var matrixHeightAndWidth = 500;

                    matrices[i] = new Matrix(matrixHeightAndWidth, matrixHeightAndWidth);
                    matrices[i].FillRandom();
                }
            }

            if (matrices[0].Fields.GetLength(1) != matrices[1].Fields.GetLength(0))
            {
                Console.WriteLine("Wrong arguments");
                return;
            }

            var numberOfIterations = 20;

            Console.Write("Without multithreading: ");
            PrintPerformance(
                () =>
                {
                    Matrix result = matrices[0] * matrices[1];
                },
                numberOfIterations);

            Matrix.UseMultithreading = true;
            Console.Write("With multithreading:    ");
            PrintPerformance(
                () =>
                {
                    Matrix result = matrices[0] * matrices[1];
                },
                numberOfIterations);
        }

        private static void PrintPerformance(Action function, int numberOfIterations)
        {
            double meanTimeValue = 0;
            double meanSquareTimeValue = 0;

            for (var j = 0; j < numberOfIterations; j++)
            {
                Stopwatch stopWatch = new Stopwatch();

                stopWatch.Start();

                function();

                stopWatch.Stop();

                var deltaTime = stopWatch.Elapsed;

                meanTimeValue += deltaTime.TotalSeconds;
                meanSquareTimeValue += Math.Pow(deltaTime.TotalSeconds, 2);
            }

            meanTimeValue /= numberOfIterations;
            meanSquareTimeValue /= numberOfIterations;

            var dispersion = meanSquareTimeValue - Math.Pow(meanTimeValue, 2);

            Console.Write("M(T)=");
            Console.Write(meanTimeValue.ToString("F4"));
            Console.Write("\tD(T)=");
            Console.Write(dispersion.ToString("F6"));
            Console.WriteLine($"\tN={numberOfIterations}");
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

                for (var currentPosition = 0;
                    currentPosition < matrix.Fields.Length;
                    currentPosition++)
                {
                    if (int.TryParse(numbersInString[currentPosition + 2], out int value))
                    {
                        var row = currentPosition / matrix.Fields.GetLength(1);
                        var column = currentPosition / matrix.Fields.GetLength(1);

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
