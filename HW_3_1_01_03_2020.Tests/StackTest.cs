using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class StackTest
    {
        protected IStack stack;

        public void TestEmptyStackShouldThrowExceptionInPop()
        {
            Assert.Throws<System.InvalidOperationException>(() => this.stack.Pop());
        }

        public void TestEmptyStackShouldEmpty()
        {
            Assert.IsTrue(this.stack.IsEmpty());
        }

        public void TestStackShouldNotEmptyAfterPush()
        {
            this.stack.Push(new Number(1));
            Assert.IsFalse(this.stack.IsEmpty());
        }

        public void TestStackShouldPushAndPopEqualValues()
        {
            var item = new Operator('/');

            this.stack.Push(item);

            Assert.AreEqual(item, this.stack.Pop());
        }

        public void TestStackShouldBeLIFO()
        {
            var item1 = new Operator('/');
            var item2 = new Operator('*');

            this.stack.Push(item1);
            this.stack.Push(item2);

            Assert.AreEqual(item2, this.stack.Pop());
            Assert.AreEqual(item1, this.stack.Pop());
        }

        public void TestEmptyStackShouldThrowExceptionInPopAfterActions()
        {
            this.stack.Push(new Number(1));
            this.stack.Push(new Number(2));
            this.stack.Pop();
            this.stack.Pop();
            Assert.Throws<System.InvalidOperationException>(() => this.stack.Pop());
        }
    }
}
