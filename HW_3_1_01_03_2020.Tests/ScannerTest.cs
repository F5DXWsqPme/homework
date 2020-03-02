using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class ScannerTest
    {
        private Scanner scanner;

        [SetUp]
        public void Setup()
        {
            this.scanner = new Scanner();
        }

        [Test]
        public void ScannerShouldThrowExceptionWhenArgumentEmpty()
        {
            Assert.Throws<System.ArgumentException>(() => this.scanner.CreateTokensQueue(string.Empty));
            Assert.Throws<System.ArgumentException>(() => this.scanner.CreateTokensQueue("    "));
        }

        [Test]
        public void ScannerShouldParseNumbers()
        {
            IQueue numbers = this.scanner.CreateTokensQueue("1,5 ,9   990");

            Assert.AreEqual(1.5, ((Number)numbers.Get()).Value);
            Assert.AreEqual(0.9, ((Number)numbers.Get()).Value);
            Assert.AreEqual(990, ((Number)numbers.Get()).Value);
            Assert.IsTrue(numbers.IsEmpty());
        }

        [Test]
        public void ScannerShouldThrowErrorWhenNumberArgumentWrong()
        {
            Assert.Throws<System.ArgumentException>(() => this.scanner.CreateTokensQueue("1,2,3"));
        }

        [Test]
        public void ScannerShouldThrowErrorWhenOperatorArgumentWrong()
        {
            Assert.Throws<System.ArgumentException>(() => this.scanner.CreateTokensQueue("."));
        }

        [Test]
        public void ScannerShouldParseOperators()
        {
            IQueue operators = this.scanner.CreateTokensQueue("+ -  *   /");
            var left = new Number(6);
            var right = new Number(2);

            Assert.AreEqual(8, ((Operator)operators.Get()).Evaluate(left, right).Value);
            Assert.AreEqual(4, ((Operator)operators.Get()).Evaluate(left, right).Value);
            Assert.AreEqual(12, ((Operator)operators.Get()).Evaluate(left, right).Value);
            Assert.AreEqual(3, ((Operator)operators.Get()).Evaluate(left, right).Value);
            Assert.IsTrue(operators.IsEmpty());
        }
    }
}
