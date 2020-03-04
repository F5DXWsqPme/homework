/// <summary>
/// Global namespace.
/// </summary>
namespace HW_2_3_24_02_2020
{
    /// <summary>
    /// Class with implementation of number (<see cref="IToken"/> subtype).
    /// </summary>
    public class Number : IToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> class.
        /// </summary>
        /// <param name="number">Number.</param>
        public Number(double number)
        {
            this.Value = number;
        }

        /// <summary>
        /// Gets number value.
        /// </summary>
        public double Value
        {
            get;
            private set;
        }
    }
}
