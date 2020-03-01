using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    class StackArrayTest
    {
        IStack stack;

        [SetUp]
        public void Setup()
        {
            stack = new StackArray();
        }

        [Test]
        public void EmptyStackShouldThrowExceptionInPop()
        {
            Assert.Throws<System.InvalidOperationException>(() => stack.Pop());
        }

        [Test]
        public void EmptyStackShouldEmpty()
        {
            Assert.IsTrue(stack.IsEmpty());
        }

        [Test]
        public void StackShouldNotEmptyAfterPush()
        {
            stack.Push(new Number(1));
            Assert.IsFalse(stack.IsEmpty());
        }

        [Test]
        public void StackShouldPushAndPopEqualValues()
        {
            var item = new Operator('/');

            stack.Push(item);

            Assert.AreEqual(item, stack.Pop());
        }

        [Test]
        public void StackShouldBeLIFO()
        {
            var item1 = new Operator('/');
            var item2 = new Operator('*');

            stack.Push(item1);
            stack.Push(item2);

            Assert.AreEqual(item2, stack.Pop());
            Assert.AreEqual(item1, stack.Pop());
        }

        [Test]
        public void StackShouldResize()
        {
            stack = new StackArray(1);

            var item1 = new Operator('/');
            var item2 = new Operator('*');

            stack.Push(item1);
            stack.Push(item2);

            Assert.AreEqual(item2, stack.Pop());
            Assert.AreEqual(item1, stack.Pop());
        }

        [Test]
        public void EmptyStackShouldThrowExceptionInPopAfterActions()
        {
            stack.Push(new Number(1));
            stack.Push(new Number(2));
            stack.Pop();
            stack.Pop();
            Assert.Throws<System.InvalidOperationException>(() => stack.Pop());
        }
    }
}
