using NUnit.Framework;

namespace HW_4_1_08_03_2020.Test
{
    public class TreeTest
    {
        [Test]
        public void TreeShouldEvaluateExpression()
        {
            string path = "TreeTest.txt";
            using (System.IO.StreamWriter writer = System.IO.File.CreateText(path))
            {
                writer.WriteLine("( * ( + 1 (/ 2   2) ) (- 3 1) )");
            }

            var tree = new Tree(path);
            Assert.AreEqual(4, tree.Evaluate());
        }

        [Test]
        public void TreeShouldEvaluateExpressionWithOneValue()
        {
            string path = "TreeTest.txt";
            using (System.IO.StreamWriter writer = System.IO.File.CreateText(path))
            {
                writer.WriteLine("2");
            }

            var tree = new Tree(path);
            Assert.AreEqual(2, tree.Evaluate());
        }

        [Test]
        public void TreeShouldThrowExceptionWhenWrongFileName()
        {
            string path = "TreeTest_DontExists.txt";

            Assert.Throws<System.ArgumentException>(() => new Tree(path));
        }
    }
}
