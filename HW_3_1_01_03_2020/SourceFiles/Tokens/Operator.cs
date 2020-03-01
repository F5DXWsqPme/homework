/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_1_01_03_2020
{
    /// <summary>
    /// Class with implementation of operator (<see cref="IToken"/> subtype).
    /// </summary>
    internal class Operator : IToken
    {
        private readonly char sign;

        /// <summary>
        /// Initializes a new instance of the <see cref="Operator"/> class.
        /// </summary>
        /// <param name="sign">Operator sign ('+', '-', '*', '/').</param>
        public Operator(char sign)
        {
            this.sign = sign;
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
