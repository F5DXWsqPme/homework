/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_1_01_03_2020
{
    /// <summary>
    /// Class with stack (<see cref="IStack"/>) implementation as an array.
    /// </summary>
    internal class StackArray : IStack
    {
        private IToken[] array;
        private int size;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackArray"/> class.
        /// </summary>
        /// <param name="initialSize">Initial size of array.</param>
        public StackArray(int initialSize = 64)
        {
            this.array = new IToken[initialSize];
            this.size = 0;
        }

        /// <summary>
        /// This function removes all items from the stack.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < this.size; i++)
            {
                this.array[i] = null;
            }

            this.size = 0;
        }

        /// <summary>
        /// Adds element to a top of the stack.
        /// </summary>
        /// <param name="token">Element to add.</param>
        public void Push(IToken token)
        {
            if (this.size + 1 >= this.array.Length)
            {
                this.ResizeArray((this.size + 1) * 2);
            }

            this.array[this.size++] = token;
        }

        /// <summary>
        /// Gets element from a top of the stack and removes it.
        /// </summary>
        /// <returns>Element that was on the top.</returns>
        public IToken Pop()
        {
            if (this.size == 0)
            {
                throw new System.Exception("Stack empty");
            }

            IToken result = this.array[--this.size];

            this.array[this.size] = null;

            return result;
        }

        /// <summary>
        /// Check that the stack does not contain elements.
        /// </summary>
        /// <returns>true-if empty, false-if otherwise.</returns>
        public bool IsEmpty()
            => this.size == 0;

        /// <summary>
        /// Create new array and copy data to new array.
        /// </summary>
        /// <param name="newSize">Size of new array.</param>
        private void ResizeArray(int newSize)
        {
            IToken[] newArray = new IToken[newSize];

            this.array.CopyTo(newArray, 0);
            this.array = newArray;
        }
    }
}
