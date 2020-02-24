namespace HW_2_1_24_02_2020
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List list = new List();

            if (list.IsEmpty())
            {
                list.Print();
                list.AddElement(9, 0);
                System.Console.WriteLine($"List[0] = {list.GetElement(0)}");
                list.Print();
                list.AddElement(8, 0);
                list.Print();
                list.AddElement(7, 0);
                list.Print();
                list.AddElement(3, 0);
                list.Print();
                list.AddElement(2, 0);
                list.Print();
                list.AddElement(1, 0);
                list.Print();
                list.AddElement(0, 0);
                list.Print();
                list.AddElement(4, 4);
                list.Print();
                list.AddElement(6, 5);
                list.Print();
                list.AddElement(5, 5);
                list.Print();
                list.SetElement(105, 5);
                list.Print();
                list.AddElement(10, 10);
                list.Print();
                list.DeleteElement(10);
                list.Print();
                list.DeleteElement(1);
                list.Print();
                list.DeleteElement(0);
                list.Print();

                for (int i = 0; i < 15; i++)
                {
                    list.DeleteElement(0);
                    list.Print();
                }

                System.Console.WriteLine($"size = {list.GetSize()}");
            }
        }
    }
}
