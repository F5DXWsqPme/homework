using NUnit.Framework;

namespace HW_4_1_08_03_2020.Test
{
    public class ValueTest
    {
        [Test]
        public void ValueShouldReturnNumber()
        {
            var value = new Value(3);

            Assert.AreEqual(3, value.GetNumber());
        }

        [Test]
        public void ValueShouldReturnThisInEvaluate()
        {
            var value = new Value(1);

            Assert.AreEqual(1, value.Evaluate().GetNumber());
        }
    }
}
