using NUnit.Framework;

namespace HW_3_2_01_03_2020.Test
{
    public class HashSetTest
    {
        private HashSet hashSet;

        [SetUp]
        public void Setup()
        {
            this.hashSet = new HashSet(new ModHash(5));
        }

        [Test]
        public void EmptyHashSetShouldDeleteElement()
        {
            this.hashSet.DeleteElement(1);
            Assert.Pass();
        }

        [Test]
        public void HashSetShouldBeEmpty()
        {
            Assert.IsFalse(this.hashSet.IsElementExists(1));
        }

        [Test]
        public void HashSetShouldAddElement()
        {
            this.hashSet.AddElement(1);

            Assert.IsTrue(this.hashSet.IsElementExists(1));
        }

        [Test]
        public void HashSetShouldCheckExistance()
        {
            this.hashSet.AddElement(1);

            Assert.IsFalse(this.hashSet.IsElementExists(6));
        }

        [Test]
        public void HashSetShouldDeleteElement()
        {
            this.hashSet.AddElement(1);
            this.hashSet.AddElement(6);
            this.hashSet.DeleteElement(1);

            Assert.IsFalse(this.hashSet.IsElementExists(1));
        }

        [Test]
        public void HashSetShouldChangeHash()
        {
            this.hashSet.AddElement(1);
            this.hashSet.AddElement(11);

            this.hashSet.SetHash(new ModHash(10));

            Assert.IsTrue(this.hashSet.IsElementExists(1));
            Assert.IsTrue(this.hashSet.IsElementExists(11));
            Assert.IsFalse(this.hashSet.IsElementExists(6));
        }

        private class ModHash : IHash
        {
            private int arraySize;

            public ModHash(int size)
            {
                this.arraySize = size;
            }

            public int GetArraySize()
            {
                return this.arraySize;
            }

            public int GetHash(int value)
            {
                return value % this.arraySize;
            }
        }
    }
}
