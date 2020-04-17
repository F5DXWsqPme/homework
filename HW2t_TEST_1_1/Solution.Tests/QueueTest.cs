using NUnit.Framework;

namespace Solution.Tests
{
    public class QueueTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void QueueShouldAddElement()
        {
            var queue = new Queue<int>();

            queue.Enqueue(30, 0);
        }

        [Test]
        public void QueueShouldAddAndGetElement()
        {
            var queue = new Queue<int>();

            queue.Enqueue(30, 0);

            Assert.AreEqual(30, queue.Dequeue());
        }

        [Test]
        public void QueueShouldWorkLikeQueueWithEqualPriority()
        {
            var queue = new Queue<int>();

            queue.Enqueue(1, 0);
            queue.Enqueue(2, 0);

            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(2, queue.Dequeue());
        }

        [Test]
        public void QueueShouldWorkWithDifferentPriority()
        {
            var queue = new Queue<int>();

            queue.Enqueue(1, 2);
            queue.Enqueue(2, 0);
            queue.Enqueue(0, 1);

            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(0, queue.Dequeue());
            Assert.AreEqual(2, queue.Dequeue());
        }

        [Test]
        public void QueueShouldWorkWithDifferentPriorityLikeQueue()
        {
            var queue = new Queue<int>();

            queue.Enqueue(5, 1);
            queue.Enqueue(1, 1);
            queue.Enqueue(3, 2);
            queue.Enqueue(4, 2);
            queue.Enqueue(6, 0);
            queue.Enqueue(2, 0);

            Assert.AreEqual(3, queue.Dequeue());
            Assert.AreEqual(4, queue.Dequeue());
            Assert.AreEqual(5, queue.Dequeue());
            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(6, queue.Dequeue());
            Assert.AreEqual(2, queue.Dequeue());
        }

        [Test]
        public void QueueShouldThrowWhenQueueEmpty()
        {
            var queue = new Queue<int>();

            Assert.Throws<QueueEmptyException>(() => queue.Dequeue());
        }

        [Test]
        public void QueueShouldThrowAfterThrow()
        {
            var queue = new Queue<int>();

            Assert.Throws<QueueEmptyException>(() => queue.Dequeue());
            Assert.Throws<QueueEmptyException>(() => queue.Dequeue());
        }

        [Test]
        public void QueueShouldThrowAfterWork()
        {
            var queue = new Queue<int>();

            queue.Enqueue(1, 1);
            queue.Dequeue();

            Assert.Throws<QueueEmptyException>(() => queue.Dequeue());
        }

        [Test]
        public void QueueShouldThrowAfterWork2()
        {
            var queue = new Queue<int>();

            queue.Enqueue(1, 1);
            queue.Dequeue();
            queue.Enqueue(2, 0);
            queue.Dequeue();

            Assert.Throws<QueueEmptyException>(() => queue.Dequeue());
        }

        [Test]
        public void QueueShouldAddEqualElements()
        {
            var queue = new Queue<int>();

            queue.Enqueue(1, -1);
            queue.Enqueue(1, -1);

            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(1, queue.Dequeue());
        }

        [Test]
        public void QueueShouldWorkWithDouble()
        {
            var queue = new Queue<double>();

            queue.Enqueue(5, 1);
            queue.Enqueue(1, 1);
            queue.Enqueue(4, 2);
            queue.Enqueue(2, 0);

            Assert.AreEqual(4, queue.Dequeue());
            Assert.AreEqual(5, queue.Dequeue());
            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(2, queue.Dequeue());
        }

        [Test]
        public void QueueShouldWorkLikeQueueWithReferenceType()
        {
            var queue = new Queue<string>();

            queue.Enqueue("11", 0);
            queue.Enqueue("22", 0);

            Assert.AreEqual("11", queue.Dequeue());
            Assert.AreEqual("22", queue.Dequeue());
        }

        [Test]
        public void QueueShouldWorkWithReferenceTypeWithDifferentPriority()
        {
            var queue = new Queue<string>();

            queue.Enqueue("11", 1);
            queue.Enqueue("21", 1);
            queue.Enqueue("12", 2);
            queue.Enqueue("22", 2);
            queue.Enqueue("10", 0);
            queue.Enqueue("20", 0);

            Assert.AreEqual("12", queue.Dequeue());
            Assert.AreEqual("22", queue.Dequeue());
            Assert.AreEqual("11", queue.Dequeue());
            Assert.AreEqual("21", queue.Dequeue());
            Assert.AreEqual("10", queue.Dequeue());
            Assert.AreEqual("20", queue.Dequeue());
        }

        [Test]
        public void QueueShouldThrowWithReferenceType()
        {
            var queue = new Queue<string>();

            queue.Enqueue("1", 0);
            queue.Dequeue();

            Assert.Throws<QueueEmptyException>(() => queue.Dequeue());
        }
    }
}