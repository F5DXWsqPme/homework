namespace Solution
{
    /// <summary>
    /// Queue with priorities class.
    /// </summary>
    /// <typeparam name="T">Queue elemment type.</typeparam>
    public class Queue<T>
    {
        private QueueElement beginQueue;

        /// <summary>
        /// Add element to queue.
        /// </summary>
        /// <param name="item">New element.</param>
        /// <param name="priority">Element priority.</param>
        public void Enqueue(T item, int priority)
        {
            QueueElement current = this.beginQueue;
            QueueElement previus = null;

            while (current != null && current.Priority >= priority)
            {
                previus = current;
                current = current.Next;
            }

            if (previus == null)
            {
                this.beginQueue = new QueueElement(item, priority, current);
            }
            else
            {
                previus.Next = new QueueElement(item, priority, current);
            }
        }

        /// <summary>
        /// Get element with highest priority (if there are many, then the first enqueued).
        /// </summary>
        /// <returns>Element.</returns>
        /// <exception cref="QueueEmptyException">Throws when queue empty.</exception>
        public T Dequeue()
        {
            if (this.beginQueue == null)
            {
                throw new QueueEmptyException("Queue empty");
            }

            var result = this.beginQueue.Value;

            this.beginQueue = this.beginQueue.Next;

            return result;
        }

        /// <summary>
        /// Queue element class.
        /// </summary>
        private class QueueElement
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="QueueElement"/> class.
            /// </summary>
            /// <param name="value">Element value.</param>
            /// <param name="priority">Element priority.</param>
            /// <param name="next">Reference tor next element.</param>
            public QueueElement(T value, int priority, QueueElement next = null)
            {
                this.Value = value;
                this.Priority = priority;
                this.Next = next;
            }

            /// <summary>
            /// Gets or sets element value.
            /// </summary>
            public T Value { get; }

            /// <summary>
            /// Gets element priority.
            /// </summary>
            public int Priority { get; }

            /// <summary>
            /// Gets or sets reference to next element.
            /// </summary>
            public QueueElement Next { get; set; }
        }
    }
}
