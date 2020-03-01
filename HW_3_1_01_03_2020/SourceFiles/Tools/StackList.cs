/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_1_01_03_2020
{
    /// <summary>
    /// Class with stack (<see cref="IStack"/>) implementation as an list.
    /// </summary>
    public class StackList : IStack
    {
        private List list;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackList"/> class.
        /// </summary>
        public StackList()
        {
            this.list = new List();
        }

        /// <summary>
        /// This function removes all items from the stack.
        /// </summary>
        public void Clear()
        {
            this.list = new List();
        }

        /// <summary>
        /// Adds element to a top of the stack.
        /// </summary>
        /// <param name="token">Element to add.</param>
        public void Push(IToken token)
        {
            this.list.AddElement(token, 0);
        }

        /// <summary>
        /// Gets element from a top of the stack and removes it.
        /// </summary>
        /// <returns>Element that was on the top.</returns>
        /// <exception cref="System.InvalidOperationException">Throws when stack empty.</exception>
        public IToken Pop()
        {
            IToken result;

            if (list.IsEmpty())
            {
                throw new System.InvalidOperationException("Stack empty");
            }

            result = this.list.GetElement(0);
            this.list.DeleteElement(0);

            return result;
        }

        /// <summary>
        /// Check that the stack does not contain elements.
        /// </summary>
        /// <returns>true-if empty, false-if otherwise.</returns>
        public bool IsEmpty()
            => this.list.IsEmpty();
    }
}
