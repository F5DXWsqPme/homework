/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_1_01_03_2020
{
    /// <summary>
    /// Class implementation of Reverse-Polish-Notation-evaluator.
    /// </summary>
    internal class Evaluator
    {
        private IStack stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="Evaluator"/> class.
        /// </summary>
        /// <param name="stack">Stack for evaluation.</param>
        public Evaluator(IStack stack)
        {
            this.stack = stack;
        }

        /// <summary>
        /// Evaluate result of expression in Reverse-Polish-Notation.
        /// </summary>
        /// <param name="tokens">Tokens queue.</param>
        /// <returns>Result of expression.</returns>
        public double Evaluate(IQueue tokens)
        {
            double result;

            this.stack.Clear();

            try
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

                result = ((Number)this.stack.Pop()).Value;
            }
            catch (System.Exception)
            {
                throw new System.Exception("Wrong expression (Evaluation step)");
            }

            if (!this.stack.IsEmpty())
            {
                throw new System.Exception("Wrong expression (Evaluation step)");
            }

            return result;
        }
    }
}
