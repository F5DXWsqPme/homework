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
        /// Gets or sets matrix fields.
        /// </summary>
        public int[,] Fields
        {
            get;
            set;
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
                throw new ArgumentException("Matrix multiplication imposible");
            }

            var answer = new Matrix(second.Fields.GetLength(1), first.Fields.GetLength(0));
            var transposedSecond = second.GetTranspose();

            if (UseMultithreading && Environment.ProcessorCount > 1)
            {
                MultiplyMatrixWithMultithreading(first, transposedSecond, answer);
            }
            else
            {
                MultiplyMatrixWithoutMultithreading(first, transposedSecond, answer);
            }

            return answer;
        }

        /// <summary>
        /// Gets transpose matrix.
        /// </summary>
        /// <returns>Transpose matrix.</returns>
        public Matrix GetTranspose()
        {
            var result = new Matrix(this.Fields.GetLength(0), this.Fields.GetLength(1));

            for (int row = 0; row < this.Fields.GetLength(0); row++)
            {
                for (int column = 0; column < this.Fields.GetLength(1); column++)
                {
                    result.Fields[column, row] = this.Fields[row, column];
                }
            }

            return result;
        }

        private static void MultiplyMatrixWithMultithreading(Matrix first, Matrix transposedSecond, Matrix answer)
        {
            int numberOfUsedThreads = Environment.ProcessorCount - 1;
            Thread[] threads = new Thread[numberOfUsedThreads];

            for (int i = 0; i < threads.Length; i++)
            {
                int localI = i;
                threads[i] = new Thread(() =>
                {
                    for (int index = localI; index < answer.Fields.GetLength(1) * answer.Fields.GetLength(0); index += numberOfUsedThreads)
                    {
                        int row = index / answer.Fields.GetLength(1);
                        int column = index % answer.Fields.GetLength(1);

                        answer.Fields[row, column] = MultiplyRowByRow(row, first, column, transposedSecond);
                    }
                });
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
        }

        private static void MultiplyMatrixWithoutMultithreading(Matrix first, Matrix transposedSecond, Matrix answer)
        {
            for (int row = 0; row < answer.Fields.GetLength(0); row++)
            {
                for (int column = 0; column < answer.Fields.GetLength(1); column++)
                {
                    answer.Fields[row, column] = MultiplyRowByRow(row, first, column, transposedSecond);
                }
            }
        }

        private static int MultiplyRowByRow(int row, Matrix first, int column, Matrix second)
        {
            int result = 0;

            for (int i = 0; i < first.Fields.GetLength(1); i++)
            {
                result += first.Fields[row, i] * second.Fields[column, i];
            }

            return result;
        }
    }
}
