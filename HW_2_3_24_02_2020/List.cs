namespace HW_2_3_24_02_2020
{
    internal class List
    {
        private ListElement firstElement;
        private int size;

        public List()
        {
            this.size = 0;
            this.firstElement = null;
        }

        ~List()
        {
            this.firstElement = null;
            this.size = 0;
        }

        public int GetSize()
        {
            return this.size;
        }

        public bool IsEmpty()
        {
            return this.GetSize() == 0;
        }

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
                current = current.GetNext();
            }

            current.SetNext(new ListElement(value, current.GetNext()));
        }

        public void DeleteElement(int position)
        {
            if (position >= this.size || position < 0)
            {
                throw new System.Exception("Wrong position");
            }

            this.size--;

            if (position == 0)
            {
                this.firstElement = this.firstElement.GetNext();
                return;
            }

            ListElement current = this.firstElement;

            for (int i = 0; i < position - 1 && i < this.GetSize() - 1; i++)
            {
                current = current.GetNext();
            }

            current.SetNext(current.GetNext().GetNext());
        }

        public void SetElement(IToken value, int position)
        {
            if (position >= this.size || position < 0)
            {
                throw new System.Exception("Wrong position");
            }

            ListElement current = this.firstElement;

            for (int i = 0; i < position; i++)
            {
                current = current.GetNext();
            }

            current.SetValue(value);
        }

        public IToken GetElement(int position)
        {
            if (position >= this.size || position < 0)
            {
                throw new System.Exception("Wrong position");
            }

            ListElement current = this.firstElement;

            for (int i = 0; i < position; i++)
            {
                current = current.GetNext();
            }

            return current.GetValue();
        }

        private class ListElement
        {
            private IToken value;
            private ListElement next;

            public ListElement(IToken value, ListElement next)
            {
                this.value = value;
                this.next = next;
            }

            public IToken GetValue()
            {
                return this.value;
            }

            public ListElement GetNext()
            {
                return this.next;
            }

            public void SetValue(IToken value)
            {
                this.value = value;
            }

            public void SetNext(ListElement next)
            {
                this.next = next;
            }
        }
    }
}
