namespace HW_1_5
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[,] matrix = new int[3, 4] { { 4, 3, 2, 1 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };
            PrintMatrix(matrix);
            SortMatrix(matrix);
            PrintMatrix(matrix);
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    System.Console.Write($"{matrix[i, j]} ");
                }

                System.Console.Write("\n");
            }

            System.Console.Write("\n");
        }

        private static void SwapCols(int[,] matrix, int index1, int index2)
        {
            int size = matrix.GetLength(0);
            int[] temporary = new int[size];

            for (int i = 0; i < size; i++)
            {
                temporary[i] = matrix[i, index1];
            }

            for (int i = 0; i < size; i++)
            {
                matrix[i, index1] = matrix[i, index2];
            }

            for (int i = 0; i < size; i++)
            {
                matrix[i, index2] = temporary[i];
            }
        }

        private static void SortMatrix(int[,] matrix)
        {
            int size = matrix.GetLength(1);
            for (int j = 0; j < size - 1; j++)
            {
                int minimum = j;
                for (int i = j + 1; i < size; i++)
                {
                    if (matrix[0, i] < matrix[0, minimum])
                    {
                        minimum = i;
                    }
                }

                SwapCols(matrix, j, minimum);
            }
        }
    }
}
