namespace HW_2_3_24_02_2020
{
    internal class Calculator
    {
        private Scanner scanner;
        private Evaluator evaluator;

        public Calculator(IStack stack)
        {
            this.scanner = new Scanner();
            this.evaluator = new Evaluator(stack);
        }

        public double Evaluate(string input)
        {
            IQueue scannedTokens = this.scanner.CreateTokensQueue(input);

            return this.evaluator.Evaluate(scannedTokens);
        }
    }
}
