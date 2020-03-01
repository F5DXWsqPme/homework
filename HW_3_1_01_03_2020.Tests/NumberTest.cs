using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    class NumberTest
    {
        Number number;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NumberShouldGetCorrectValue()
        {
            number = new Number(5);

            double result = number.Value;

            Assert.AreEqual(result, 5);
        }
    }
}
