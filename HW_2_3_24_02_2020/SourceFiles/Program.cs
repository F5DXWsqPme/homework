namespace HW_2_3_24_02_2020
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            System.Console.Write("Input stack implementation type (array or list): ");
            string stackType = System.Console.ReadLine();
            Calculator calculator;

            if (stackType == "array")
            {
                calculator = new Calculator(new StackArray());
            }
            else if (stackType == "list")
            {
                calculator = new Calculator(new StackList());
            }
            else
            {
                System.Console.WriteLine("Error: Wrong stack implementation type");
                return;
            }

            System.Console.Write("Input expression: ");
            string expression = System.Console.ReadLine();

            try
            {
                double result = calculator.Evaluate(expression);
                System.Console.WriteLine($"Success: {expression} = {result}");
            }
            catch (System.Exception exception)
            {
                System.Console.WriteLine($"Error: {exception.Message}");
            }
        }
    }
}
