namespace HW_2_2_24_02_2020
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HashSet set = new HashSet();

            System.Console.WriteLine(set.IsElementExists(1 << 12));
            set.AddElement(1 << 12);
            System.Console.WriteLine(set.IsElementExists(1 << 12));
            System.Console.WriteLine(set.IsElementExists(0));
            set.AddElement((1 << 12) + 1);
            System.Console.WriteLine(set.IsElementExists((1 << 12) + 1));
            set.AddElement(0);
            System.Console.WriteLine(set.IsElementExists(0));
            set.AddElement(1);
            System.Console.WriteLine(set.IsElementExists(1));
            set.AddElement(1 << 12);
            System.Console.WriteLine(set.IsElementExists(1 << 12));
            set.AddElement((1 << 12) + 1);
            System.Console.WriteLine(set.IsElementExists((1 << 12) + 1));
            System.Console.WriteLine(set.IsElementExists((2 * (1 << 12)) + 1));
            set.DeleteElement((1 << 12) + 1);
            System.Console.WriteLine(set.IsElementExists((1 << 12) + 1));
        }
    }
}
