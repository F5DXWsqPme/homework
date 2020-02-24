namespace HW_1_4
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[,] array = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            WriteSpiral(array);
        }

        private static void WriteSpiral(int[,] array)
        {
            int size = array.GetLength(0);
            int[,] direction = new int[4, 2] { { 1, 0 }, { 0, -1 }, { -1, 0 }, { 0, 1 } };
            int step = 1;
            int x = size / 2;
            int y = size / 2;
            int state = 0;

            while (step < size)
            {
                for (int i = 0; i < step; i++)
                {
                    System.Console.Write($"{array[x, y]} ");
                    x += direction[state, 0];
                    y += direction[state, 1];
                }

                state = (state + 1) % 4;
                if (state % 2 == 0)
                {
                    step++;
                }
            }

            for (int i = 0; i < size; i++)
            {
                System.Console.Write($"{array[x, y]} ");
                x += direction[state, 0];
                y += direction[state, 1];
            }

            System.Console.Write("\n");
        }
    }
}
