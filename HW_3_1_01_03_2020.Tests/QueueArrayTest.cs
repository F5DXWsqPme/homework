using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    class QueueArrayTest
    {
        IQueue queue;

        [SetUp]
        public void Setup()
        {
            queue = new QueueArray();
        }

        [Test]
        public void EmptyQueueShouldThrowExceptionInGet()
        {
            Assert.Throws<System.InvalidOperationException>(() => queue.Get());
        }

        [Test]
        public void EmptyQueueShouldEmpty()
        {
            Assert.IsTrue(queue.IsEmpty());
        }

        [Test]
        public void QueueShouldNotEmptyAfterPut()
        {
            queue.Put(new Number(1));
            Assert.IsFalse(queue.IsEmpty());
        }

        [Test]
        public void QueueShouldPutAndGetEqualValues()
        {
            var item = new Operator('/');

            queue.Put(item);

            Assert.AreEqual(item, queue.Get());
        }

        [Test]
        public void QueueShouldBeFIFO()
        {
            var item1 = new Operator('/');
            var item2 = new Operator('*');

            queue.Put(item1);
            queue.Put(item2);

            Assert.AreEqual(item1, queue.Get());
            Assert.AreEqual(item2, queue.Get());
        }

        [Test]
        public void QueueShouldResize()
        {
            queue = new QueueArray(1);

            var item1 = new Operator('/');
            var item2 = new Operator('*');

            queue.Put(item1);
            queue.Put(item2);

            Assert.AreEqual(item1, queue.Get());
            Assert.AreEqual(item2, queue.Get());
        }

        [Test]
        public void EmptyQueueShouldThrowExceptionInGetAfterActions()
        {
            queue.Put(new Number(1));
            queue.Put(new Number(2));
            queue.Get();
            queue.Get();
            Assert.Throws<System.InvalidOperationException>(() => queue.Get());
        }
    }
}
