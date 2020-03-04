/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_1_01_03_2020
{
    /// <summary>
    /// Class implementation of Reverse-Polish-Notation-evaluator.
    /// </summary>
    public class Evaluator
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
        /// <exception cref="System.ArgumentException">Throws when expreession dont correct.</exception>
        public double Evaluate(IQueue tokens)
        {
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

                double result = ((Number)this.stack.Pop()).Value;

                if (!this.stack.IsEmpty())
                {
                    throw new System.ArgumentException("Wrong expression (Evaluation step)");
                }

                return result;
            }
            catch (System.InvalidCastException)
            {
                throw new System.ArgumentException("Wrong expression (Evaluation step)");
            }
            catch (System.ArgumentException)
            {
                throw new System.ArgumentException("Wrong expression (Evaluation step)");
            }
            catch (System.InvalidOperationException)
            {
                throw new System.ArgumentException("Wrong expression (Evaluation step)");
            }
        }
    }
}
