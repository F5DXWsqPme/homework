namespace HW_2_3_24_02_2020
{
    internal class Evaluator
    {
        public double Evaluate(IQueue tokens)
        {
            IStack stack = (IStack)new StackArray();

            while (!tokens.IsEmpty())
            {
                IToken current = tokens.Get();

                if (current is Number number)
                {
                    stack.Push(number);
                }
                else
                {
                    Number right = (Number)stack.Pop();
                    Number left = (Number)stack.Pop();
                    stack.Push(((Operator)current).Evaluate(left, right));
                }
            }

            return ((Number)stack.Pop()).Get();
        }
    }
}
