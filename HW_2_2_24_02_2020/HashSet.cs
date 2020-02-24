namespace HW_2_2_24_02_2020
{
    internal class HashSet
    {
        private const int ArraySize = 1 << 12;
        private List[] array;

        public HashSet()
        {
            this.array = new List[ArraySize];

            for (int i = 0; i < ArraySize; i++)
            {
                this.array[i] = new List();
            }
        }

        public void AddElement(int value)
        {
            int hash = this.EvalHash(value);
            List list = this.array[hash];

            if (!list.IsItemExists(value))
            {
                list.AddElement(value, 0);
            }
        }

        public bool IsElementExists(int value)
        {
            int hash = this.EvalHash(value);
            List list = this.array[hash];

            return list.IsItemExists(value);
        }

        public void DeleteElement(int value)
        {
            int hash = this.EvalHash(value);
            List list = this.array[hash];

            int position = 0;
            if (list.GetElementPosition(value, ref position))
            {
                list.DeleteElement(position);
            }
        }

        private int EvalHash(int number)
        {
            return number % ArraySize;
        }
    }
}
