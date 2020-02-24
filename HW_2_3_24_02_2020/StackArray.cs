namespace HW_2_3_24_02_2020
{
    internal class StackArray : IStack
    {
        private IToken[] array;
        private int size;

        public StackArray(int maximalSize = 1 << 12)
        {
            this.array = new IToken[maximalSize];
            this.size = 0;
        }

        public void Push(IToken token)
        {
            this.array[this.size++] = token;
        }

        public IToken Pop()
        {
            IToken result = this.array[--this.size];

            this.array[this.size] = null;

            return result;
        }
    }
}
