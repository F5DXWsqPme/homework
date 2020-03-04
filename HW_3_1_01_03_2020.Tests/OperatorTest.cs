using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class OperatorTest
    {
        private Operator oper;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OperatorShouldEvaluateSum()
        {
            this.oper = new Operator('+');

            Number result = this.oper.Evaluate(new Number(1), new Number(7));

            Assert.AreEqual(8, result.Value);
        }

        [Test]
        public void OperatorShouldEvaluateDifference()
        {
            this.oper = new Operator('-');

            Number result = this.oper.Evaluate(new Number(1), new Number(7));

            Assert.AreEqual(-6, result.Value);
        }

        [Test]
        public void OperatorShouldEvaluateQuotient()
        {
            this.oper = new Operator('/');

            Number result = this.oper.Evaluate(new Number(-35), new Number(7));

            Assert.AreEqual(-5, result.Value);
        }

        [Test]
        public void OperatorShouldEvaluateProduct()
        {
            this.oper = new Operator('*');

            Number result = this.oper.Evaluate(new Number(5), new Number(7));

            Assert.AreEqual(35, result.Value);
        }

        [Test]
        public void OperatorShouldDivideByZero()
        {
            this.oper = new Operator('/');

            Number result = this.oper.Evaluate(new Number(35), new Number(0));

            Assert.AreEqual(double.PositiveInfinity, result.Value);
        }

        [Test]
        public void OperatorShouldThrowExceptionInConstructorWithWrongArgument()
        {
            Assert.Throws<System.ArgumentException>(() => new Operator(','));
        }
    }
}
