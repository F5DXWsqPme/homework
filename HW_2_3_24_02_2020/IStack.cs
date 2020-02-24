namespace HW_2_3_24_02_2020
{
    internal interface IStack
    {
        public abstract IToken Pop();

        public abstract void Push(IToken token);
    }
}
