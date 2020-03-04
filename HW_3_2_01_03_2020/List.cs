/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_2_01_03_2020
{
    /// <summary>
    /// Class with implementation of list.
    /// </summary>
    public class List
    {
        private ListElement firstElement;
        private int size;
                     
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
        /// <param name="position">Position of new element in list.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Throws at the wrong position.</exception>
        public void AddElement(int value, int position)
        {
            if (position > this.size || position < 0)
            {
                throw new System.ArgumentOutOfRangeException("Wrong position");
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
        /// <exception cref="System.ArgumentOutOfRangeException">Throws at the wrong position.</exception>
        public void DeleteElement(int position)
        {
            if (position >= this.size || position < 0)
            {
                throw new System.ArgumentOutOfRangeException("Wrong position");
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
        /// <exception cref="System.ArgumentOutOfRangeException">Throws at the wrong position.</exception>
        public void SetElement(int value, int position)
        {
            if (position >= this.size || position < 0)
            {
                throw new System.ArgumentOutOfRangeException("Wrong position");
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
        /// <exception cref="System.ArgumentOutOfRangeException">Throws at the wrong position.</exception>
        public int GetElement(int position)
        {
            if (position >= this.size || position < 0)
            {
                throw new System.ArgumentOutOfRangeException("Wrong position");
            }

            ListElement current = this.firstElement;

            for (int i = 0; i < position; i++)
            {
                current = current.Next;
            }

            return current.Value;
        }

        /// <summary>
        /// Checks element existance and gets its position.
        /// </summary>
        /// <param name="value">Element.</param>
        /// <param name="position">Element position.</param>
        /// <returns>True-if exists, false-if otherwise.</returns>
        public bool GetElementPosition(int value, out int position)
        {
            position = 0;

            ListElement current = this.firstElement;

            while (current != null)
            {
                if (current.Value == value)
                {
                    return true;
                }

                current = current.Next;
                position++;
            }

            return false;
        }

        /// <summary>
        /// Checks element existance.
        /// </summary>
        /// <param name="value">Element.</param>
        /// <returns>True-if exists, false-if otherwise</returns>
        public bool IsItemExists(int value)
        {
            ListElement current = this.firstElement;

            while (current != null)
            {
                if (current.Value == value)
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
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
            public ListElement(int value, ListElement next)
            {
                this.Value = value;
                this.Next = next;
            }

            /// <summary>
            /// Gets or sets list element value.
            /// </summary>
            public int Value
            {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets next element reference.
            /// </summary>
            public ListElement Next
            {
                get;
                set;
            }
        }
    }
}
