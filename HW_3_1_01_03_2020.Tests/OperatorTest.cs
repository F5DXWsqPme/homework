using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    class OperatorTest
    {
        Operator oper;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OperatorShouldEvaluateSum()
        {
            oper = new Operator('+');

            Number result = oper.Evaluate(new Number(1), new Number(7));

            Assert.AreEqual(8, result.Value);
        }

        [Test]
        public void OperatorShouldEvaluateDifference()
        {
            oper = new Operator('-');

            Number result = oper.Evaluate(new Number(1), new Number(7));

            Assert.AreEqual(-6, result.Value);
        }

        [Test]
        public void OperatorShouldEvaluateQuotient()
        {
            oper = new Operator('/');

            Number result = oper.Evaluate(new Number(-35), new Number(7));

            Assert.AreEqual(-5, result.Value);
        }

        [Test]
        public void OperatorShouldEvaluateProduct()
        {
            oper = new Operator('*');

            Number result = oper.Evaluate(new Number(5), new Number(7));

            Assert.AreEqual(35, result.Value);
        }

        [Test]
        public void OperatorShouldDivideByZero()
        {
            oper = new Operator('/');

            Number result = oper.Evaluate(new Number(35), new Number(0));

            Assert.AreEqual(double.PositiveInfinity, result.Value);
        }

        [Test]
        public void OperatorShouldThrowExceptionInConstructorWithWrongArgument()
        {
            Assert.Throws<System.ArgumentException>(() => new Operator(','));
        }
    }
}
