using NUnit.Framework;

namespace Solution.Tests
{
    public class CalculatorStateTest
    {
        private GameState state;

        [SetUp]
        public void Setup()
        {
            state = new GameState(4);
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

        [Test]
        public void GameShouldPressButton()
        {
            var changes = state.Update(0, out bool isWin);

            Assert.AreEqual(changes.Length, 1);
            Assert.AreEqual(changes[0].cellIndex, 0);
            Assert.AreEqual(changes[0].changedCell.State, GameState.CellState.Pressed);
            Assert.AreEqual(isWin, false);
        }

        [Test]
        public void GameShouldPressButton2()
        {
            state.Update(0, out bool isWin0);
            var changes = state.Update(1, out bool isWin1);

            Assert.AreEqual(changes.Length, 1);
            Assert.AreEqual(changes[0].cellIndex, 1);
            Assert.AreEqual(changes[0].changedCell.State, GameState.CellState.Pressed);
            Assert.AreEqual(isWin1, false);
        }

        [Test]
        public void GameShouldPressButton3()
        {
            state.Update(0, out bool isWin0);
            state.Update(1, out bool isWin1);
            var changes = state.Update(2, out bool isWin2);

            Assert.AreEqual(changes.Length, 3);
            Assert.AreEqual(isWin2, false);
        }
    }
}
