namespace HW_2_3_24_02_2020
{
    internal interface IQueue
    {
        public abstract IToken Get();

        public abstract void Put(IToken token);
    }
}
