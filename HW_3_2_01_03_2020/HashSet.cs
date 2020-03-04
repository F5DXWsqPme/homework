/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_2_01_03_2020
{
    public class HashSet
    {
        private List[] array;
        private IHash hashFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashSet"/> class.
        /// </summary>
        /// <param name="hash">Hash function.</param>
        public HashSet(IHash hash)
        {
            this.array = new List[hash.GetArraySize()];

            for (int i = 0; i < this.array.Length; i++)
            {
                this.array[i] = new List();
            }

            this.hashFunction = hash;
        }

        /// <summary>
        /// Adds element to hashset.
        /// </summary>
        /// <param name="value">New element.</param>
        public void AddElement(int value)
        {
            int hash = this.hashFunction.GetHash(value);
            List list = this.array[hash];

            if (!list.IsItemExists(value))
            {
                list.AddElement(value, 0);
            }
        }

        /// <summary>
        /// Check element existance.
        /// </summary>
        /// <param name="value">Element.</param>
        /// <returns>True-if exists, false-if otherwise.</returns>
        public bool IsElementExists(int value)
        {
            int hash = this.hashFunction.GetHash(value);
            List list = this.array[hash];

            return list.IsItemExists(value);
        }

        /// <summary>
        /// Delete element from hashset.
        /// </summary>
        /// <param name="value">Element.</param>
        public void DeleteElement(int value)
        {
            int hash = this.hashFunction.GetHash(value);
            List list = this.array[hash];

            if (list.GetElementPosition(value, out int position))
            {
                list.DeleteElement(position);
            }
        }

        /// <summary>
        /// Change hash function.
        /// </summary>
        /// <param name="newHash">Hash function.</param>
        public void SetHash(IHash newHash)
        {
            List[] newArray = new List[newHash.GetArraySize()];

            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = new List();
            }

            foreach (List list in this.array)
            {
                while (!list.IsEmpty())
                {
                    int element = list.GetElement(0);

                    list.DeleteElement(0);

                    int hash = newHash.GetHash(element);

                    newArray[hash].AddElement(element, 0);
                }
            }

            this.array = newArray;
            this.hashFunction = newHash;
        }
    }
}