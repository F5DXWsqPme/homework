/// <summary>
/// Global namespace.
/// </summary>
namespace HW_2_3_24_02_2020
{
    /// <summary>
    /// Class with implementation of operator (<see cref="IToken"/> subtype).
    /// </summary>
    public class Operator : IToken
    {
        private readonly char sign;

        /// <summary>
        /// Initializes a new instance of the <see cref="Operator"/> class.
        /// </summary>
        /// <param name="sign">Operator sign ('+', '-', '*', '/').</param>
        /// <exception cref="System.ArgumentException">Throws when sign don't correct.</exception>
        public Operator(char sign)
        {
            this.sign = sign;

            switch (this.sign)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    break;
                default:
                    throw new System.ArgumentException($"Wrong operator '{this.sign}'");
            }
        }

        /// <summary>
        /// Apply operator to two numbers.
        /// </summary>
        /// <param name="left">First number.</param>
        /// <param name="right">Second number.</param>
        /// <returns>Operation result.</returns>
        public Number Evaluate(Number left, Number right)
        {
            switch (this.sign)
            {
                case '+':
                    return new Number(left.Value + right.Value);
                case '-':
                    return new Number(left.Value - right.Value);
                case '*':
                    return new Number(left.Value * right.Value);
                case '/':
                    return new Number(left.Value / right.Value);
            }

            return new Number(int.MinValue);
        }
    }
}
