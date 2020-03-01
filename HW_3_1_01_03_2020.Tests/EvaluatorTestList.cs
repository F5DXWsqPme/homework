using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class EvaluatorTestList
    {
        Evaluator evaluator;

        [SetUp]
        public void Setup()
        {
            evaluator = new Evaluator(new StackList());
        }

        [Test]
        public void EvaluatorShouldThrowExceptionWhenArgumentEmpty()
        {
            Assert.Throws<System.ArgumentException>(() => evaluator.Evaluate(new QueueArray()));
        }

        [Test]
        public void EvaluatorShouldEvaluateExpression()
        {
            var tokens = new QueueArray();

            tokens.Put(new Number(1));
            tokens.Put(new Number(2));
            tokens.Put(new Number(3));
            tokens.Put(new Operator('+'));
            tokens.Put(new Operator('-'));
            tokens.Put(new Number(4));
            tokens.Put(new Operator('*'));
            tokens.Put(new Number(8));
            tokens.Put(new Operator('/'));

            double result = evaluator.Evaluate(tokens);

            Assert.AreEqual(-2, result);
        }

        [Test]
        public void EvaluatorShouldThrowExceptionWhenArgumentsIncorrect()
        {
            var tokens = new QueueArray();

            tokens.Put(new Number(1));
            tokens.Put(new Number(2));
            tokens.Put(new Operator('+'));
            tokens.Put(new Operator('-'));

            Assert.Throws<System.ArgumentException>(() => evaluator.Evaluate(tokens));
        }
    }
}
