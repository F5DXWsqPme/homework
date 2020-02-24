namespace HW_2_3_24_02_2020
{
    internal class QueueArray
    {
        private IToken[] array;
        private int begin;
        private int end;
        private int maximalSize;

        public QueueArray(int maximalSize = 1 << 12)
        {
            this.array = new IToken[maximalSize];
            this.begin = 0;
            this.end = 0;
            this.maximalSize = maximalSize;
        }

        public void Put(IToken token)
        {
            this.array[this.end] = token;
            this.end = (this.end + 1) % this.maximalSize;
        }

        public IToken Get()
        {
            IToken result = this.array[this.begin];

            this.array[this.begin] = null;
            this.begin = (this.begin + 1) % this.maximalSize;

            return result;
        }
    }
}
