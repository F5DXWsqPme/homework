namespace HW_2_3_24_02_2020
{
    internal class QueueArray : IQueue
    {
        private IToken[] array;
        private int begin;
        private int end;

        public QueueArray(int maximalSize = 1 /* 64 */)
        {
            this.array = new IToken[maximalSize];
            this.begin = 0;
            this.end = 0;
        }

        public void Put(IToken token)
        {
            if ((this.end + 1) % this.array.Length == this.begin)
            {
                this.ResizeArray(this.array.Length * 2);
            }

            this.array[this.end] = token;
            this.end = (this.end + 1) % this.array.Length;
        }

        public IToken Get()
        {
            if (this.begin == this.end)
            {
                throw new System.Exception("Queue empty");
            }

            IToken result = this.array[this.begin];

            this.array[this.begin] = null;
            this.begin = (this.begin + 1) % this.array.Length;

            return result;
        }

        public bool IsEmpty()
        {
            return this.begin == this.end;
        }

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
