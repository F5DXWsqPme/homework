using System;
using System.Drawing;
using System.Threading;

namespace Solution
{
    /// <summary>
    /// Matrix class.
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="width">Matrix width.</param>
        /// <param name="height">Matrix height.</param>
        public Matrix(int width, int height)
        {
            this.Fields = new int[height, width];
        }

        /// <summary>
        /// Gets or sets a value indicating whether use multithreaded multiplication.
        /// </summary>
        public static bool UseMultithreading
        {
            get;
            set;
        }

        /// <summary>
        /// Gets matrix fields.
        /// </summary>
        public int[,] Fields
        {
            get;
            private set;
        }

        /// <summary>
        /// Multiply matrix funcion.
        /// </summary>
        /// <param name="first">First matrix.</param>
        /// <param name="second">Second matrix.</param>
        /// <exception cref="ArgumentException">Throws when argument matrix incompatible for this matrix.</exception>
        /// <returns>Result matrix.</returns>
        public static Matrix operator *(Matrix first, Matrix second)
        {
            if (first.Fields.GetLength(1) != second.Fields.GetLength(0))
            {
                throw new ArgumentException("Matrix multiplication impossible");
            }

            var answer = new Matrix(second.Fields.GetLength(1), first.Fields.GetLength(0));

            if (UseMultithreading && Environment.ProcessorCount > 1)
            {
                MultiplyMatrixWithMultithreading(first, second, answer);
            }
            else
            {
                MultiplyMatrixWithoutMultithreading(first, second, answer);
            }

            return answer;
        }

        /// <summary>
        /// Fill matrix with random numbers.
        /// </summary>
        public void FillRandom()
        {
            var random = new Random();

            for (var row = 0; row < this.Fields.GetLength(0); row++)
            {
                for (var column = 0; column < this.Fields.GetLength(1); column++)
                {
                    this.Fields[row, column] = random.Next();
                }
            }
        }

        private static void MultiplyMatrixWithMultithreading(Matrix first, Matrix second, Matrix answer)
        {
            var numberOfUsedThreads = Environment.ProcessorCount - 1;
            Thread[] threads = new Thread[numberOfUsedThreads];

            for (var i = 0; i < threads.Length; i++)
            {
                var localI = i;
                threads[i] = new Thread(() =>
                {
                    for (var index = localI; index < answer.Fields.GetLength(1) * answer.Fields.GetLength(0); index += numberOfUsedThreads)
                    {
                        var row = index / answer.Fields.GetLength(1);
                        var column = index % answer.Fields.GetLength(1);

                        answer.Fields[row, column] = MultiplyRowByColumn(row, first, column, second);
                    }
                });
            }

            for (var i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            for (var i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
        }

        private static void MultiplyMatrixWithoutMultithreading(Matrix first, Matrix second, Matrix answer)
        {
            for (var row = 0; row < answer.Fields.GetLength(0); row++)
            {
                for (var column = 0; column < answer.Fields.GetLength(1); column++)
                {
                    answer.Fields[row, column] = MultiplyRowByColumn(row, first, column, second);
                }
            }
        }

        private static int MultiplyRowByColumn(int row, Matrix first, int column, Matrix second)
        {
            var result = 0;

            for (var i = 0; i < first.Fields.GetLength(1); i++)
            {
                result += first.Fields[row, i] * second.Fields[i, column];
            }

            return result;
        }
    }
}
