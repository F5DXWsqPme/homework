namespace SomeNamespace
{
    class Program
    {
        public static void Main(string[] args)
        {
            int length = 10;
            int[] array = new int[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array.Length - i;
                System.Console.Write($"{array[i]} ");
            }

            System.Console.Write("\n");

            SelectionSort(array);

            for (int i = 0; i < array.Length; i++)
            {
                System.Console.Write($"{array[i]} ");
            }

            System.Console.Write("\n");
        }

        private static void Swap(ref int firstElement, ref int secondElement)
        {
            int temporary = firstElement;
            firstElement = secondElement;
            secondElement = temporary;
        }

        private static void SelectionSort(int[] array)
        {
            for (int j = 0; j < array.Length - 1; j++)
            {
                int minimum = j;
                for (int i = j + 1; i < array.Length; i++)
                {
                    if (array[i] < array[minimum])
                    {
                        minimum = i;
                    }
                }

                Swap(ref array[j], ref array[minimum]);
            }
        }
    }
}
