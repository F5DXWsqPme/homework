using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class CalculatorTestList
    {
        Calculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new Calculator(new StackList());
        }

        [Test]
        public void CalculatorShouldThrowExceptionWhenArgumentEmpty()
        {
            Assert.Throws<System.ArgumentException>(() => calculator.Evaluate(string.Empty));
            Assert.Throws<System.ArgumentException>(() => calculator.Evaluate("   "));
        }

        [Test]
        public void CalculatorShouldThrowExceptionWhenArgumentIncorrectForEvaluator()
        {
            Assert.Throws<System.ArgumentException>(() => calculator.Evaluate("1 2 -   +"));
        }

        [Test]
        public void CalculatorShouldThrowExceptionWhenArgumentIncorrectForScanner()
        {
            Assert.Throws<System.ArgumentException>(() => calculator.Evaluate("1,2,3"));
            Assert.Throws<System.ArgumentException>(() => calculator.Evaluate("  &"));
        }

        [Test]
        public void CalculatorShouldEvaluateExpression()
        {
            double result = calculator.Evaluate("1 2  3+- 4*8/");

            Assert.AreEqual(-2, result);
        }

        [Test]
        public void CalculatorShouldReturnsNumber()
        {
            double result = calculator.Evaluate("5");

            Assert.AreEqual(5, result);
        }
    }
}
