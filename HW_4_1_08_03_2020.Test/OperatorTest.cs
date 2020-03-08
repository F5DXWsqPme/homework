using NUnit.Framework;
using System.Collections.Generic;

namespace HW_4_1_08_03_2020.Test
{
    public class OperatorTest
    {
        private static IEnumerable<TestCaseData> Operators
            => new TestCaseData[]
            {
                new TestCaseData(new Addition(new Value(1), new Value(2))).Returns(3),
                new TestCaseData(new Addition(new Value(0), new Value(2))).Returns(2),
                new TestCaseData(new Addition(new Value(1), new Value(0))).Returns(1),
                new TestCaseData(new Addition(new Value(0), new Value(0))).Returns(0),
                new TestCaseData(new Addition(new Value(4), new Value(-4))).Returns(0),

                new TestCaseData(new Substraction(new Value(1), new Value(2))).Returns(-1),
                new TestCaseData(new Substraction(new Value(0), new Value(2))).Returns(-2),
                new TestCaseData(new Substraction(new Value(1), new Value(0))).Returns(1),
                new TestCaseData(new Substraction(new Value(0), new Value(0))).Returns(0),
                new TestCaseData(new Substraction(new Value(4), new Value(-4))).Returns(8),

                new TestCaseData(new Multiplication(new Value(1), new Value(2))).Returns(2),
                new TestCaseData(new Multiplication(new Value(0), new Value(2))).Returns(0),
                new TestCaseData(new Multiplication(new Value(1), new Value(0))).Returns(0),
                new TestCaseData(new Multiplication(new Value(0), new Value(0))).Returns(0),
                new TestCaseData(new Multiplication(new Value(4), new Value(-4))).Returns(-16),

                new TestCaseData(new Division(new Value(1), new Value(2))).Returns(0),
                new TestCaseData(new Division(new Value(0), new Value(2))).Returns(0),
                new TestCaseData(new Division(new Value(4), new Value(-4))).Returns(-1),
            };

        [TestCaseSource(nameof(Operators))]
        public int OperatorShouldEvaluate(Operator oper)
        {
            return oper.Evaluate().GetNumber();
        }

        [Test]
        public void DivisionShouldThrowExceptionAfterDivideByZero()
        {
            Operator division = new Division(new Value(5), new Value(0));

            Assert.Throws<System.DivideByZeroException>(() => division.Evaluate());
        }

        [Test]
        public void DivisionShouldThrowExceptionAfterDivideZeroByZero()
        {
            Operator division = new Division(new Value(0), new Value(0));

            Assert.Throws<System.DivideByZeroException>(() => division.Evaluate());
        }
    }
}
