namespace HW_2_3_24_02_2020
{
    internal class Evaluator
    {
        private IStack stack;

        public Evaluator(IStack stack)
        {
            this.stack = stack;
        }

        public double Evaluate(IQueue tokens)
        {
            while (!tokens.IsEmpty())
            {
                IToken current = tokens.Get();

                if (current is Number number)
                {
                    this.stack.Push(number);
                }
                else
                {
                    Number right = (Number)this.stack.Pop();
                    Number left = (Number)this.stack.Pop();
                    this.stack.Push(((Operator)current).Evaluate(left, right));
                }
            }

            return ((Number)this.stack.Pop()).Get();
        }
    }
}
