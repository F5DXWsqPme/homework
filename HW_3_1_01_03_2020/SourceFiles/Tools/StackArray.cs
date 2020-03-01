namespace HW_2_3_24_02_2020
{
    internal class StackArray : IStack
    {
        private IToken[] array;
        private int size;

        public StackArray(int maximalSize = 64)
        {
            this.array = new IToken[maximalSize];
            this.size = 0;
        }

        public void Clear()
        {
            for (int i = 0; i < this.size; i++)
            {
                this.array[i] = null;
            }

            this.size = 0;
        }

        public void Push(IToken token)
        {
            if (this.size + 1 >= this.array.Length)
            {
                this.ResizeArray((this.size + 1) * 2);
            }

            this.array[this.size++] = token;
        }

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

        public bool IsEmpty()
            => this.size == 0;

        private void ResizeArray(int newSize)
        {
            IToken[] newArray = new IToken[newSize];

            this.array.CopyTo(newArray, 0);
            this.array = newArray;
        }
    }
}
