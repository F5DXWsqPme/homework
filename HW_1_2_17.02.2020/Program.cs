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
            int currentFibonacciNumber = 0, nextFibonacciNumber = 1, newFibonacciNumber;

            for (int i = 0; i < number; i++)
            {
                newFibonacciNumber = currentFibonacciNumber + nextFibonacciNumber;
                currentFibonacciNumber = nextFibonacciNumber;
                nextFibonacciNumber = newFibonacciNumber;
            }

            return currentFibonacciNumber;
        }
    }
}
