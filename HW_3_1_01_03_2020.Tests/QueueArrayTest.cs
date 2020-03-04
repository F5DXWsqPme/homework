using NUnit.Framework;

namespace HW_3_1_01_03_2020.Test
{
    public class QueueArrayTest
    {
        private IQueue queue;

        [SetUp]
        public void Setup()
        {
            this.queue = new QueueArray();
        }

        [Test]
        public void EmptyQueueShouldThrowExceptionInGet()
        {
            Assert.Throws<System.InvalidOperationException>(() => this.queue.Get());
        }

        [Test]
        public void EmptyQueueShouldEmpty()
        {
            Assert.IsTrue(this.queue.IsEmpty());
        }

        [Test]
        public void QueueShouldNotEmptyAfterPut()
        {
            this.queue.Put(new Number(1));
            Assert.IsFalse(this.queue.IsEmpty());
        }

        [Test]
        public void QueueShouldPutAndGetEqualValues()
        {
            var item = new Operator('/');

            this.queue.Put(item);

            Assert.AreEqual(item, this.queue.Get());
        }

        [Test]
        public void QueueShouldBeFIFO()
        {
            var item1 = new Operator('/');
            var item2 = new Operator('*');

            this.queue.Put(item1);
            this.queue.Put(item2);

            Assert.AreEqual(item1, this.queue.Get());
            Assert.AreEqual(item2, this.queue.Get());
        }

        [Test]
        public void QueueShouldResize()
        {
            this.queue = new QueueArray(1);

            var item1 = new Operator('/');
            var item2 = new Operator('*');

            this.queue.Put(item1);
            this.queue.Put(item2);

            Assert.AreEqual(item1, this.queue.Get());
            Assert.AreEqual(item2, this.queue.Get());
        }

        [Test]
        public void EmptyQueueShouldThrowExceptionInGetAfterActions()
        {
            this.queue.Put(new Number(1));
            this.queue.Put(new Number(2));
            this.queue.Get();
            this.queue.Get();
            Assert.Throws<System.InvalidOperationException>(() => this.queue.Get());
        }
    }
}
