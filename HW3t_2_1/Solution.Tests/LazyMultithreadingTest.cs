namespace Solution.Tests
{
    using NUnit.Framework;
    using System;
    using System.Threading;

    public class LazyMultithreadingTest
    {
        [Test]
        public void LazyShouldEvaluateFunction()
        {
            Func<int> function = () => 5;
            var lazy = LazyFactory.CreateMultithreadingLazy(function);

            var numberOfThreads = 2;
            var threads = new Thread[numberOfThreads];
            var results = new bool[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                var index = i;
                threads[i] = new Thread(() =>
                {
                    results[index] = (lazy.Get() == 5);
                });
            }

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i].Join();
                Assert.IsTrue(results[i]);
            }
        }

        [Test]
        public void LazyShouldEvaluateOneTime()
        {
            bool firstCall = true;

            Func<int> function = () =>
            {
                if (firstCall)
                {
                    firstCall = false;
                    return 5;
                }

                return 0;
            };

            var lazy = LazyFactory.CreateMultithreadingLazy(function);
            var runEvent = new ManualResetEventSlim(false);

            var numberOfThreads = 5;
            var threads = new Thread[numberOfThreads];
            var results = new bool[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                var index = i;
                threads[i] = new Thread(() =>
                {
                    runEvent.Wait();
                    results[index] = (lazy.Get() == 5) && (lazy.Get() == 5);
                });
            }

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i].Start();
            }

            runEvent.Set();

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i].Join();
                Assert.IsTrue(results[i]);
            }
        }
    }
}