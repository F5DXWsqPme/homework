namespace HW_2_3_24_02_2020
{
    internal class StackList : IStack
    {
        private List list;

        public StackList()
        {
            this.list = new List();
        }

        public void Clear()
        {
            this.list = new List();
        }

        public void Push(IToken token)
        {
            this.list.AddElement(token, 0);
        }

        public IToken Pop()
        {
            IToken result;

            try
            {
                result = this.list.GetElement(0);
                this.list.DeleteElement(0);
            }
            catch (System.Exception)
            {
                throw new System.Exception("Stack empty");
            }

            return result;
        }
    }
}
