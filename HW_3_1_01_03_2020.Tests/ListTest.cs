using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class ListTest
    {
        List list;

        [SetUp]
        public void Setup()
        {
            list = new List();
        }

        [Test]
        public void ListShouldEmptyBeforeActions()
        {
            Assert.AreEqual(0, list.GetSize());
            Assert.IsTrue(list.IsEmpty());
        }

        [Test]
        public void EmptyListShouldThrowExceptionInGetElement()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => list.GetElement(0));
        }

        [Test]
        public void ListShouldAddElement()
        {
            list.AddElement(new Number(1), 0);

            Assert.AreEqual(1, list.GetSize());
            Assert.IsFalse(list.IsEmpty());
        }

        [Test]
        public void ListShouldGetElement()
        {
            var item0 = new Number(0);
            var item1 = new Number(1);

            list.AddElement(item0, 0);
            list.AddElement(item1, 1);

            Assert.AreEqual(item0, list.GetElement(0));
            Assert.AreEqual(item1, list.GetElement(1));
        }

        [Test]
        public void ListShouldSetElement()
        {
            var item0 = new Number(0);
            var item1 = new Number(1);
            var item2 = new Operator('/');

            list.AddElement(item0, 0);
            list.AddElement(item1, 1);
            list.SetElement(item2, 1);

            Assert.AreEqual(item0, list.GetElement(0));
            Assert.AreEqual(item2, list.GetElement(1));
        }

        [Test]
        public void ListShouldDeleteElement()
        {
            var item0 = new Number(0);
            var item1 = new Number(1);

            list.AddElement(item0, 0);
            list.AddElement(item1, 1);
            list.DeleteElement(1);

            Assert.AreEqual(1, list.GetSize());
            Assert.AreEqual(item0, list.GetElement(0));
        }

        [Test]
        public void EmptyListShouldThrowExceptionInGetElementAfterActions()
        {
            list.AddElement(new Number(1), 0);
            list.DeleteElement(0);
            Assert.Throws<System.ArgumentOutOfRangeException>(() => list.GetElement(0));
        }
    }
}