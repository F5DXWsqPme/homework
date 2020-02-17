namespace SomeNamespace
{
    class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine($"Fibonacci(10) = {Fibonacci(10)}");
        }

        private static int Fibonacci(int num)
        {
            int fib0 = 0, fib1 = 1, newFib;

            for (int i = 0; i < num; i++)
            {
                newFib = fib0 + fib1;
                fib0 = fib1;
                fib1 = newFib;
            }

            return fib0;
        }
    }
}
