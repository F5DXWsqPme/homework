namespace Solution.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class LazyOneThreadTest
    {
        [TestCaseSource(nameof(CreateLazyData))]
        [Test]
        public void LazyShouldEvaluateFunction(Func<Func<int>, ILazy<int>> createLazy)
        {
            Func<int> function = () => 5;
            var lazy = createLazy(function);

            Assert.AreEqual(5, lazy.Get());
        }

        [TestCaseSource(nameof(CreateLazyData))]
        [Test]
        public void LazyShouldEvaluateOneTime(Func<Func<int>, ILazy<int>> createLazy)
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

            var lazy = createLazy(function);

            Assert.AreEqual(5, lazy.Get());
            Assert.AreEqual(5, lazy.Get());
        }

        private static IEnumerable<TestCaseData> CreateLazyData()
        {
            Func<Func<int>, ILazy<int>> createOneThreadLazy = LazyFactory.CreateOneThreadLazy;
            yield return new TestCaseData(createOneThreadLazy).SetCategory("Without multithreading");

            Func<Func<int>, ILazy<int>> createMultithreadingLazy = LazyFactory.CreateMultithreadingLazy;
            yield return new TestCaseData(createMultithreadingLazy).SetCategory("With multithreading");
        }
    }
}