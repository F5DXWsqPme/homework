namespace HW_2_3_24_02_2020
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var calculator = new Calculator();

            System.Console.WriteLine(calculator.Evaluate("1 2 + 4 - 6*3/"));
        }
    }
}
