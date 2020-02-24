namespace SomeNamespace
{
    class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine($"Factorial(5) = {Factorial(5)}");
        }

        private static int Factorial(int num)
        {
            int res = 1;

            while (num > 1)
            {
                res *= num;
                num--;
            }

            return res;
        }
    }
}