using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class StackArrayTest : StackTest
    {
        [SetUp]
        public void Setup()
        {
            this.stack = new StackArray();
        }

        [Test]
        public void EmptyStackShouldThrowExceptionInPop()
        {
            TestEmptyStackShouldThrowExceptionInPop();
        }

        [Test]
        public void EmptyStackShouldEmpty()
        {
            TestEmptyStackShouldEmpty();
        }

        [Test]
        public void StackShouldNotEmptyAfterPush()
        {
            TestStackShouldNotEmptyAfterPush();
        }

        [Test]
        public void StackShouldPushAndPopEqualValues()
        {
            TestStackShouldPushAndPopEqualValues();
        }

        [Test]
        public void StackShouldBeLIFO()
        {
            TestStackShouldBeLIFO();
        }

        [Test]
        public void EmptyStackShouldThrowExceptionInPopAfterActions()
        {
            TestEmptyStackShouldThrowExceptionInPopAfterActions();
        }
    }
}
