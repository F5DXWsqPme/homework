﻿namespace HW_2_2_24_02_2020
{
    internal class List
    {
        private ListElement firstElement;
        private int size;

        public int GetSize()
        {
            return this.size;
        }

        public bool IsEmpty()
        {
            return this.GetSize() == 0;
        }

        public void AddElement(int value, int position)
        {
            if (position > this.size || position < 0)
            {
                return;
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
                return;
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

        public void SetElement(int value, int position)
        {
            if (position > this.size || position < 0)
            {
                return;
            }

            ListElement current = this.firstElement;

            for (int i = 0; i < position; i++)
            {
                current = current.GetNext();
            }

            current.SetValue(value);
        }

        public void Print()
        {
            ListElement current = this.firstElement;

            System.Console.Write($"List (size = {this.GetSize()}): ");

            if (this.IsEmpty())
            {
                System.Console.WriteLine("<Empty list>");
                return;
            }

            while (current != null)
            {
                System.Console.Write($"{current.GetValue()} ");
                current = current.GetNext();
            }

            System.Console.WriteLine();
        }

        public bool GetElementPosition(int value, ref int position)
        {
            position = 0;

            ListElement current = this.firstElement;

            while (current != null)
            {
                if (current.GetValue() == value)
                {
                    return true;
                }

                current = current.GetNext();
                position++;
            }

            return false;
        }

        public bool IsItemExists(int value)
        {
            ListElement current = this.firstElement;

            while (current != null)
            {
                if (current.GetValue() == value)
                {
                    return true;
                }

                current = current.GetNext();
            }

            return false;
        }

        private class ListElement
        {
            private int value;
            private ListElement next;

            public ListElement(int value, ListElement next)
            {
                this.value = value;
                this.next = next;
            }

            public int GetValue()
            {
                return this.value;
            }

            public ListElement GetNext()
            {
                return this.next;
            }

            public void SetValue(int value)
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
