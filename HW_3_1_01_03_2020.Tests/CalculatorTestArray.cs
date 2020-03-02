using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class CalculatorTestArray
    {
        private Calculator calculator;

        [SetUp]
        public void Setup()
        {
            this.calculator = new Calculator(new StackArray());
        }

        [Test]
        public void CalculatorShouldThrowExceptionWhenArgumentEmpty()
        {
            Assert.Throws<System.ArgumentException>(() => this.calculator.Evaluate(string.Empty));
            Assert.Throws<System.ArgumentException>(() => this.calculator.Evaluate("   "));
        }

        [Test]
        public void CalculatorShouldThrowExceptionWhenArgumentIncorrectForEvaluator()
        {
            Assert.Throws<System.ArgumentException>(() => this.calculator.Evaluate("1 2 -   +"));
        }

        [Test]
        public void CalculatorShouldThrowExceptionWhenArgumentIncorrectForScanner()
        {
            Assert.Throws<System.ArgumentException>(() => this.calculator.Evaluate("1,2,3"));
            Assert.Throws<System.ArgumentException>(() => this.calculator.Evaluate("  &"));
        }

        [Test]
        public void CalculatorShouldEvaluateExpression()
        {
            double result = this.calculator.Evaluate("1 2  3+- 4*8/");

            Assert.AreEqual(-2, result);
        }

        [Test]
        public void CalculatorShouldReturnsNumber()
        {
            double result = this.calculator.Evaluate("5");

            Assert.AreEqual(5, result);
        }
    }
}
