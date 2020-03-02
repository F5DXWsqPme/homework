using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class StackListTest
    {
        private IStack stack;

        [SetUp]
        public void Setup()
        {
            this.stack = new StackList();
        }

        [Test]
        public void EmptyStackShouldThrowExceptionInPop()
        {
            Assert.Throws<System.InvalidOperationException>(() => this.stack.Pop());
        }

        [Test]
        public void EmptyStackShouldEmpty()
        {
            Assert.IsTrue(this.stack.IsEmpty());
        }

        [Test]
        public void StackShouldNotEmptyAfterPush()
        {
            this.stack.Push(new Number(1));
            Assert.IsFalse(this.stack.IsEmpty());
        }

        [Test]
        public void StackShouldPushAndPopEqualValues()
        {
            var item = new Operator('/');

            this.stack.Push(item);

            Assert.AreEqual(item, this.stack.Pop());
        }

        [Test]
        public void StackShouldBeLIFO()
        {
            var item1 = new Operator('/');
            var item2 = new Operator('*');

            this.stack.Push(item1);
            this.stack.Push(item2);

            Assert.AreEqual(item2, this.stack.Pop());
            Assert.AreEqual(item1, this.stack.Pop());
        }

        [Test]
        public void EmptyStackShouldThrowExceptionInPopAfterActions()
        {
            this.stack.Push(new Number(1));
            this.stack.Push(new Number(2));
            this.stack.Pop();
            this.stack.Pop();
            Assert.Throws<System.InvalidOperationException>(() => this.stack.Pop());
        }
    }
}
