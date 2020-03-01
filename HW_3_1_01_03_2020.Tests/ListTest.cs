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
        public void EmptyListShouldThrowExceptionInGetElement()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => list.GetElement(0));
        }
    }
}