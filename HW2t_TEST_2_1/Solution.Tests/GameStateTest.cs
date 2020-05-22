using NUnit.Framework;

namespace Solution.Tests
{
    public class CalculatorStateTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GameShouldThrowWhenFieldSizeLessThan()
        {
            Assert.Throws<System.ArgumentException>(() => new Solution.GameState(0));
            Assert.Throws<System.ArgumentException>(() => new Solution.GameState(-4));
            Assert.Throws<System.ArgumentException>(() => new Solution.GameState(-5));
        }

        [Test]
        public void GameShouldThrowWhenFieldSizeWrong()
        {
            Assert.Throws<System.ArgumentException>(() => new Solution.GameState(1));
            Assert.Throws<System.ArgumentException>(() => new Solution.GameState(3));
            Assert.Throws<System.ArgumentException>(() => new Solution.GameState(5));
        }
    }
}
