/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_1_01_03_2020
{
    /// <summary>
    /// Class with implementation of list.
    /// </summary>
    internal class List
    {
        private ListElement firstElement;
        private int size;

        /// <summary>
        /// Initializes a new instance of the <see cref="List"/> class.
        /// </summary>
        public List()
        {
            this.size = 0;
            this.firstElement = null;
        }

        /// <summary>
        /// Returns size of list.
        /// </summary>
        /// <returns>Size of list.</returns>
        public int GetSize()
            => this.size;

        /// <summary>
        /// Check that the list does not contain elements.
        /// </summary>
        /// <returns>true-if empty, false-if otherwise.</returns>
        public bool IsEmpty()
            => this.GetSize() == 0;

        /// <summary>
        /// Adds element to position in list.
        /// </summary>
        /// <param name="value">Element to add.</param>
        /// <param name="position">Position of new element in list (position >= 0 and position <= GetSize()).</param>
        public void AddElement(IToken value, int position)
        {
            if (position > this.size || position < 0)
            {
                throw new System.Exception("Wrong position");
            }

            this.size++;

            if (position == 0)
            {
                this.firstElement = new ListElement(value, this.firstElement);
                return;
            }

            ListElement current = this.firstElement;

            for (int i = 0; i < position - 1 && i < this.GetSize(); i++)
            {
                current = current.Next;
            }

            current.Next = new ListElement(value, current.Next);
        }

        /// <summary>
        /// Gets element from position.
        /// </summary>
        /// <param name="position">Element pposition.</param>
        public void DeleteElement(int position)
        {
            if (position >= this.size || position < 0)
            {
                throw new System.Exception("Wrong position");
            }

            this.size--;

            if (position == 0)
            {
                this.firstElement = this.firstElement.Next;
                return;
            }

            ListElement current = this.firstElement;

            for (int i = 0; i < position - 1 && i < this.GetSize() - 1; i++)
            {
                current = current.Next;
            }

            current.Next = current.Next.Next;
        }

        /// <summary>
        /// Setup element by position.
        /// </summary>
        /// <param name="value">New element value.</param>
        /// <param name="position">Element position.</param>
        public void SetElement(IToken value, int position)
        {
            if (position >= this.size || position < 0)
            {
                throw new System.Exception("Wrong position");
            }

            ListElement current = this.firstElement;

            for (int i = 0; i < position; i++)
            {
                current = current.Next;
            }

            current.Value = value;
        }

        /// <summary>
        /// Gets element by position.
        /// </summary>
        /// <param name="position">Element position.</param>
        /// <returns>Element by position.</returns>
        public IToken GetElement(int position)
        {
            if (position >= this.size || position < 0)
            {
                throw new System.Exception("Wrong position");
            }

            ListElement current = this.firstElement;

            for (int i = 0; i < position; i++)
            {
                current = current.Next;
            }

            return current.Value;
        }

        /// <summary>
        /// Class with implementation of list element.
        /// </summary>
        private class ListElement
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ListElement"/> class.
            /// </summary>
            /// <param name="value">Element value.</param>
            /// <param name="next">Reference to next element.</param>
            public ListElement(IToken value, ListElement next)
            {
                this.Value = value;
                this.Next = next;
            }

            public IToken Value
            {
                get;
                set;
            }

            public ListElement Next
            {
                get;
                set;
            }
        }
    }
}
