/// <summary>
/// Global namespace.
/// </summary>
namespace HW_4_1_08_03_2020
{
    /// <summary>
    /// Operator abstract class.
    /// </summary>
    public abstract class Operator : TreeNode
    {
        private IOperand left;
        private IOperand right;

        /// <summary>
        /// Initializes a new instance of the <see cref="Operator"/> class.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        public Operator(IOperand left, IOperand right)
        {
            this.left = left;
            this.right = right;
        }

        /// <summary>
        /// Evaluate result of operation.
        /// </summary>
        /// <returns>Operation result.</returns>
        public override Value Evaluate()
        {
            return this.Evaluate(this.left.GetValue(), this.right.GetValue());
        }

        /// <summary>
        /// Print operator function.
        /// </summary>
        public override void Print()
        {
            System.Console.Write("( ");
            this.PrintSign();
            System.Console.Write(" ");
            this.left.Print();
            System.Console.Write(" ");
            this.right.Print();
            System.Console.Write(" )");
        }

        /// <summary>
        /// Print current operator function.
        /// </summary>
        protected abstract void PrintSign();

        /// <summary>
        /// Evaluate result of operation.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Operation result.</returns>
        protected abstract Value Evaluate(Value left, Value right);
    }
}
