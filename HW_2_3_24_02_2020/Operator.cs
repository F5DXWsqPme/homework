namespace HW_2_3_24_02_2020
{
    internal class Operator : IToken
    {
        private readonly char sign;
        private readonly int priority;

        public Operator(char sign)
        {
            this.sign = sign;
            this.priority = this.EvaluatePriority();
        }

        public int GetPriority()
        {
            return this.priority;
        }

        public Number Evaluate(Number left, Number right)
        {
            switch (this.sign)
            {
                case '+':
                    return new Number(left.Get() + right.Get());
                case '-':
                    return new Number(left.Get() - right.Get());
                case '*':
                    return new Number(left.Get() * right.Get());
                case '/':
                    return new Number(left.Get() / right.Get());
            }

            return new Number(int.MinValue);
        }

        private int EvaluatePriority()
        {
            switch (this.sign)
            {
                case '+':
                    return 400;
                case '-':
                    return 400;
                case '*':
                    return 600;
                case '/':
                    return 600;
                default:
                    throw new System.Exception($"Wrong operator '{this.sign}'");
            }
        }
    }
}
