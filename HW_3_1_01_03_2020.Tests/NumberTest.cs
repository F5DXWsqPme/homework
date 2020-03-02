using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class NumberTest
    {
        private Number number;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NumberShouldGetCorrectValue()
        {
            this.number = new Number(5);

            double result = this.number.Value;

            Assert.AreEqual(result, 5);
        }
    }
}
