/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_2_01_03_2020
{
    /// <summary>
    /// Class with implementation hash table structure.
    /// </summary>
    public class HashSet
    {
        private List[] array;
        private IHash hashFunction;
        private int numberOfElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashSet"/> class.
        /// </summary>
        /// <param name="hash">Hash function.</param>
        public HashSet(IHash hash)
        {
            this.array = new List[/*hash.GetArraySize()*/1];

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
            int index = this.EvaluateHash(value, this.array.Length);
            List list = this.array[index];

            if (!list.IsItemExists(value))
            {
                list.AddElement(value, 0);
                this.numberOfElements++;
                if (this.numberOfElements / (double)this.array.Length > 2)
                {
                    this.ResetArray(this.numberOfElements);
                }
            }
        }

        /// <summary>
        /// Check element existance.
        /// </summary>
        /// <param name="value">Element.</param>
        /// <returns>True-if exists, false-if otherwise.</returns>
        public bool IsElementExists(int value)
        {
            int index = this.EvaluateHash(value, this.array.Length);
            List list = this.array[index];

            return list.IsItemExists(value);
        }

        /// <summary>
        /// Delete element from hashset.
        /// </summary>
        /// <param name="value">Element.</param>
        public void DeleteElement(int value)
        {
            int index = this.EvaluateHash(value, this.array.Length);
            List list = this.array[index];

            if (list.GetElementPosition(value, out int position))
            {
                list.DeleteElement(position);
                this.numberOfElements--;
                if (this.numberOfElements / (double)this.array.Length < 0.5)
                {
                    this.ResetArray(this.numberOfElements + 1);
                }
            }
        }

        /// <summary>
        /// Change hash function.
        /// </summary>
        /// <param name="newHash">Hash function.</param>
        public void SetHash(IHash newHash)
        {
            this.hashFunction = newHash;
            ResetArray(this.array.Length);
        }

        /// <summary>
        /// Resize array function.
        /// </summary>
        /// <param name="newSize">New array size.</param>
        private void ResetArray(int newSize)
        {
            List[] newArray = new List[newSize];

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

                    int index = this.EvaluateHash(element, newArray.Length);

                    newArray[index].AddElement(element, 0);
                }
            }

            this.array = newArray;
        }

        private int EvaluateHash(int value, int size)
            => System.Math.Abs(this.hashFunction.GetHash(value) % this.array.Length);
    }
}