using NUnit.Framework;

namespace HW_3_2_01_03_2020.Test
{
    public class ListTest
    {
        private List list;

        [SetUp]
        public void Setup()
        {
            this.list = new List();
        }

        [Test]
        public void ListShouldEmptyBeforeActions()
        {
            Assert.AreEqual(0, this.list.GetSize());
            Assert.IsTrue(this.list.IsEmpty());
        }

        [Test]
        public void EmptyListShouldThrowExceptionInGetElement()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => this.list.GetElement(0));
        }

        [Test]
        public void ListShouldAddElement()
        {
            this.list.AddElement(1, 0);

            Assert.AreEqual(1, this.list.GetSize());
            Assert.IsFalse(this.list.IsEmpty());
        }

        [Test]
        public void ListShouldGetElement()
        {
            var item0 = 0;
            var item1 = 1;

            this.list.AddElement(item0, 0);
            this.list.AddElement(item1, 1);

            Assert.AreEqual(item0, this.list.GetElement(0));
            Assert.AreEqual(item1, this.list.GetElement(1));
        }

        [Test]
        public void ListShouldSetElement()
        {
            var item0 = 1;
            var item1 = 2;
            var item2 = 3;

            this.list.AddElement(item0, 0);
            this.list.AddElement(item1, 1);
            this.list.SetElement(item2, 1);

            Assert.AreEqual(item0, this.list.GetElement(0));
            Assert.AreEqual(item2, this.list.GetElement(1));
        }

        [Test]
        public void ListShouldDeleteElement()
        {
            var item0 = 0;
            var item1 = 1;

            this.list.AddElement(item0, 0);
            this.list.AddElement(item1, 1);
            this.list.DeleteElement(1);

            Assert.AreEqual(1, this.list.GetSize());
            Assert.AreEqual(item0, this.list.GetElement(0));
        }

        [Test]
        public void EmptyListShouldThrowExceptionInGetElementAfterActions()
        {
            this.list.AddElement(1, 0);
            this.list.DeleteElement(0);
            Assert.Throws<System.ArgumentOutOfRangeException>(() => this.list.GetElement(0));
        }

        [Test]
        public void ListShouldFindElement()
        {
            this.list.AddElement(1, 0);
            this.list.AddElement(2, 1);

            Assert.IsTrue(this.list.IsItemExists(1));
        }

        [Test]
        public void ListShouldDontFindElement()
        {
            this.list.AddElement(1, 0);
            this.list.AddElement(2, 1);

            Assert.IsFalse(this.list.IsItemExists(3));
        }

        [Test]
        public void ListShouldGetPosition()
        {
            this.list.AddElement(1, 0);
            this.list.AddElement(2, 1);

            Assert.IsTrue(this.list.GetElementPosition(2, out int position));
            Assert.AreEqual(1, position);
        }

        [Test]
        public void ListShouldDontGetPosition()
        {
            this.list.AddElement(1, 0);
            this.list.AddElement(2, 1);

            Assert.IsFalse(this.list.GetElementPosition(3, out int position));
        }
    }
}
