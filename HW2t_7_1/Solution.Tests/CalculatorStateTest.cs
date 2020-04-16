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
        public void CalculatorShouldScan()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update("2");
            state.Update("3");

            Assert.AreEqual("+123", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldScanZero()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("0");
            state.Update("0");
            state.Update("1");
            state.Update("2");
            state.Update("0");
            state.Update("3");

            Assert.AreEqual("+1203", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldGetRightSubstraction()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update("2");
            state.Update("-");
            state.Update("5");
            state.Update("2");
            state.Update("=");

            Assert.AreEqual("-40", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldGetRightAddition()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update("2");
            state.Update("+");
            state.Update("5");
            state.Update("2");
            state.Update("=");

            Assert.AreEqual("+64", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldGetRightMultiplication()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update("2");
            state.Update("*");
            state.Update("5");
            state.Update("=");

            Assert.AreEqual("+60", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldGetRightDivision()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update("2");
            state.Update("/");
            state.Update("4");
            state.Update("=");

            Assert.AreEqual("+3", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldGetRightDivisionFloat()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update("2");
            state.Update("/");
            state.Update("7");
            state.Update(".");
            state.Update("1");
            state.Update("=");

            Assert.AreEqual("+1,69", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldChangeSign()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update("2");
            state.Update("3");
            state.Update("+/-");

            Assert.AreEqual("-123", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldChangeSignInNewNumber()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update("2");
            state.Update("3");
            state.Update("+");
            state.Update("7");
            state.Update("+/-");
            state.Update("7");
            state.Update("=");

            Assert.AreEqual("+46", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldRightRound()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("2");
            state.Update("/");
            state.Update("3");
            state.Update("=");

            Assert.AreEqual("+0,67", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldWorkAfterClearLikeNew()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("3");
            state.Update("+");
            state.Update("2");
            state.Update("C");
            state.Update("6");
            state.Update("+");
            state.Update("7");
            state.Update("=");

            Assert.AreEqual("+13", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldWorkAfterEqualWithResult()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("3");
            state.Update("+");
            state.Update("2");
            state.Update("=");
            state.Update("-");
            state.Update("7");
            state.Update(".");
            state.Update("5");
            state.Update("=");

            Assert.AreEqual("-2,5", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldWorkAfterEqualWithoutResult()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("3");
            state.Update("+");
            state.Update("2");
            state.Update("=");
            state.Update("9");
            state.Update("-");
            state.Update("7");
            state.Update(".");
            state.Update("5");
            state.Update("=");

            Assert.AreEqual("+1,5", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldRightWorkWithBigDisplaySize1()
        {
            var state = new Solution.CalculatorState(12);

            state.Update("9");
            state.Update("-");
            state.Update("7");
            state.Update(".");
            state.Update("5");
            state.Update("=");

            Assert.AreEqual("+1,5", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldRightWorkWithBigDisplaySize2()
        {
            var state = new Solution.CalculatorState(12);

            state.Update("2");
            state.Update("/");
            state.Update("3");
            state.Update("=");

            Assert.AreEqual("+0,666666667", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldRightWorkWithLowDisplaySize()
        {
            var state = new Solution.CalculatorState(2);

            state.Update("2");
            state.Update("-");
            state.Update("3");
            state.Update("=");

            Assert.AreEqual("-1", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldThrowWhenWrongDigit()
        {
            var state = new Solution.CalculatorState(5);

            Assert.Throws<System.ArgumentException>(() => state.Update("10.0"));
            Assert.Throws<System.ArgumentException>(() => state.Update("-1"));
            Assert.Throws<System.ArgumentException>(() => state.Update("5."));
        }

        [Test]
        public void CalculatorShouldThrowWhenUnknownSymbol()
        {
            var state = new Solution.CalculatorState(5);

            Assert.Throws<System.ArgumentException>(() => state.Update("?"));
            Assert.Throws<System.ArgumentException>(() => state.Update("^"));
            Assert.Throws<System.ArgumentException>(() => state.Update(":"));
        }

        [Test]
        public void CalculatorShouldThrowWhenDisplaySizeLessThan2()
        {
            Assert.Throws<System.ArgumentException>(() => new Solution.CalculatorState(1));
        }

        [Test]
        public void CalculatorShouldGetErrorWhenNumberVeryBig1()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update("2");
            state.Update("3");
            state.Update("4");
            state.Update("5");

            Assert.AreEqual("ERROR", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldGetErrorWhenNumberVeryBig2()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update("2");
            state.Update("3");
            state.Update("4");
            state.Update("+");
            state.Update("9");
            state.Update("9");
            state.Update("9");
            state.Update("9");
            state.Update("=");

            Assert.AreEqual("ERROR", state.GetOutputString());
        }

        [Test]
        public void CalculatorShouldGetErrorWhenInNumber2Dot()
        {
            var state = new Solution.CalculatorState(5);

            state.Update("1");
            state.Update(".");
            state.Update("1");
            state.Update(".");

            Assert.AreEqual("ERROR", state.GetOutputString());
        }
    }
}