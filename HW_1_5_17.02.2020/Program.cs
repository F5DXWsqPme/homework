namespace HW_1_5
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[,] matr = new int[3, 4] { { 4, 3, 2, 1 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };
            PrintMatrix(matr);
            SortMatrix(matr);
            PrintMatrix(matr);
        }

        private static void PrintMatrix(int[,] matr)
        {
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    System.Console.Write($"{matr[i, j]} ");
                }

                System.Console.Write("\n");
            }

            System.Console.Write("\n");
        }

        private static void SwapCols(int[,] matr, int ind1, int ind2)
        {
            int size = matr.GetLength(0);
            int[] tmp = new int[size];

            for (int i = 0; i < size; i++)
            {
                tmp[i] = matr[i, ind1];
            }

            for (int i = 0; i < size; i++)
            {
                matr[i, ind1] = matr[i, ind2];
            }

            for (int i = 0; i < size; i++)
            {
                matr[i, ind2] = tmp[i];
            }
        }

        private static void SortMatrix(int[,] matr)
        {
            int size = matr.GetLength(1);
            for (int j = 0; j < size - 1; j++)
            {
                int min = j;
                for (int i = j + 1; i < size; i++)
                {
                    if (matr[0, i] < matr[0, min])
                    {
                        min = i;
                    }
                }

                SwapCols(matr, j, min);
            }
        }
    }
}
