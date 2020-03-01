namespace HW_2_3_24_02_2020
{
    internal class Operator : IToken
    {
        private readonly char sign;

        public Operator(char sign)
        {
            this.sign = sign;
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
    }
}
