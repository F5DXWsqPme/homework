/// <summary>
/// Global namespace.
/// </summary>
namespace HW_4_1_08_03_2020
{
    /// <summary>
    /// Value class.
    /// </summary>
    public sealed class Value : TreeNode
    {
        private int number;

        /// <summary>
        /// Initializes a new instance of the <see cref="Value"/> class.
        /// </summary>
        /// <param name="number">Number value.</param>
        public Value(int number)
        {
            this.number = number;
        }

        /// <summary>
        /// Gets number function.
        /// </summary>
        /// <returns>Number.</returns>
        public int GetNumber()
        {
            return this.number;
        }

        /// <summary>
        /// Evaluate result of tree.
        /// </summary>
        /// <returns>Result value (this).</returns>
        public override Value Evaluate()
        {
            return this;
        }

        /// <summary>
        /// Print value function.
        /// </summary>
        public override void Print()
        {
            System.Console.Write(this.number);
        }
    }
}
