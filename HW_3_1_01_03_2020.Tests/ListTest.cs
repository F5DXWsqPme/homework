using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
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
            this.list.AddElement(new Number(1), 0);

            Assert.AreEqual(1, this.list.GetSize());
            Assert.IsFalse(this.list.IsEmpty());
        }

        [Test]
        public void ListShouldGetElement()
        {
            var item0 = new Number(0);
            var item1 = new Number(1);

            this.list.AddElement(item0, 0);
            this.list.AddElement(item1, 1);

            Assert.AreEqual(item0, this.list.GetElement(0));
            Assert.AreEqual(item1, this.list.GetElement(1));
        }

        [Test]
        public void ListShouldSetElement()
        {
            var item0 = new Number(0);
            var item1 = new Number(1);
            var item2 = new Operator('/');

            this.list.AddElement(item0, 0);
            this.list.AddElement(item1, 1);
            this.list.SetElement(item2, 1);

            Assert.AreEqual(item0, this.list.GetElement(0));
            Assert.AreEqual(item2, this.list.GetElement(1));
        }

        [Test]
        public void ListShouldDeleteElement()
        {
            var item0 = new Number(0);
            var item1 = new Number(1);

            this.list.AddElement(item0, 0);
            this.list.AddElement(item1, 1);
            this.list.DeleteElement(1);

            Assert.AreEqual(1, this.list.GetSize());
            Assert.AreEqual(item0, this.list.GetElement(0));
        }

        [Test]
        public void EmptyListShouldThrowExceptionInGetElementAfterActions()
        {
            this.list.AddElement(new Number(1), 0);
            this.list.DeleteElement(0);
            Assert.Throws<System.ArgumentOutOfRangeException>(() => this.list.GetElement(0));
        }
    }
}