/// <summary>
/// Global namespace.
/// </summary>
namespace HW_2_3_24_02_2020
{
    /// <summary>
    /// Class with queue (<see cref="IQueue"/>) implementation as an array.
    /// </summary>
    public class QueueArray : IQueue
    {
        private IToken[] array;
        private int begin;
        private int end;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueArray"/> class.
        /// </summary>
        /// <param name="initialSize">Initial size of array.</param>
        public QueueArray(int initialSize = 64)
        {
            this.array = new IToken[initialSize];
            this.begin = 0;
            this.end = 0;
        }

        /// <summary>
        /// Adds element to a tail of the queue.
        /// </summary>
        /// <param name="token">Element to add.</param>
        public void Put(IToken token)
        {
            if ((this.end + 1) % this.array.Length == this.begin)
            {
                this.ResizeArray(this.array.Length * 2);
            }

            this.array[this.end] = token;
            this.end = (this.end + 1) % this.array.Length;
        }

        /// <summary>
        /// Gets element from a head of the queue and removes it.
        /// </summary>
        /// <returns>lement that was on the head.</returns>
        /// <exception cref="System.InvalidOperationException">Throws when queue empty.</exception>
        public IToken Get()
        {
            if (this.begin == this.end)
            {
                throw new System.InvalidOperationException("Queue empty");
            }

            IToken result = this.array[this.begin];

            this.array[this.begin] = null;
            this.begin = (this.begin + 1) % this.array.Length;

            return result;
        }

        /// <summary>
        /// Check that the queue does not contain elements.
        /// </summary>
        /// <returns>true-if empty, false-if otherwise.</returns>
        public bool IsEmpty()
            => this.begin == this.end;

        /// <summary>
        /// Create new array and copy data to new array.
        /// </summary>
        /// <param name="newSize">Size of new array.</param>
        private void ResizeArray(int newSize)
        {
            IToken[] newArray = new IToken[newSize];

            int size;
            for (size = 0; this.begin != this.end; size++)
            {
                newArray[size] = this.array[this.begin];
                this.begin = (this.begin + 1) % this.array.Length;
            }

            this.array = newArray;
            this.begin = 0;
            this.end = size;
        }
    }
}
