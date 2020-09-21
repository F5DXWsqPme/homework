namespace Solution.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using NUnit.Framework;

    public class MyThreadPoolTests
    {
        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldEvaluateFunction(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);

            var task = threadPool.Submit(() => 5);

            Assert.AreEqual(5, task.Result);
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldEvaluateFunctionEndWait(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);

            var task = threadPool.Submit(() =>
            {
                Thread.Sleep(5);
                return 5;
            });

            Assert.AreEqual(5, task.Result);
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldEvaluateBeforeDispose(int numberOfThreads)
        {
            IMyTask<int> task;
            {
                using var threadPool = new MyThreadPool(numberOfThreads);

                task = threadPool.Submit(() =>
                {
                    Thread.Sleep(5);
                    return 5;
                });
            }

            Assert.AreEqual(5, task.Result);
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldEvaluateFunctionMultiTask(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);
            var tasks = new List<IMyTask<int>>();
            int numberOfTasks = 30;

            for (int i = 0; i < numberOfTasks; i++)
            {
                int index = i;

                tasks.Add(threadPool.Submit(() => index));
            }

            for (int i = 0; i < numberOfTasks; i++)
            {
                Assert.AreEqual(i, tasks[i].Result);
            }
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldEvaluateFunctionEndWaitMultiTask(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);
            var tasks = new List<IMyTask<int>>();
            int numberOfTasks = 30;

            for (int i = 0; i < numberOfTasks; i++)
            {
                int index = i;

                tasks.Add(threadPool.Submit(() =>
                {
                    Thread.Sleep(5);
                    return index;
                }));
            }

            for (int i = 0; i < numberOfTasks; i++)
            {
                Assert.AreEqual(i, tasks[i].Result);
            }
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldEvaluateBeforeDisposeMultiTask(int numberOfThreads)
        {
            var tasks = new List<IMyTask<int>>();
            int numberOfTasks = 30;
            {
                using var threadPool = new MyThreadPool(numberOfThreads);

                for (int i = 0; i < numberOfTasks; i++)
                {
                    int index = i;

                    tasks.Add(threadPool.Submit(() =>
                    {
                        Thread.Sleep(5);
                        return index;
                    }));
                }
            }

            for (int i = 0; i < numberOfTasks; i++)
            {
                Assert.AreEqual(i, tasks[i].Result);
            }
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldCreateNumberOfThreadsThreads(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);
            var tasks = new List<IMyTask<int>>();
            int numberOfTasks = 5 * numberOfThreads;
            int sleepTime = 30;

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < numberOfTasks; i++)
            {
                tasks.Add(threadPool.Submit(() =>
                {
                    Thread.Sleep(sleepTime);
                    return 0;
                }));
            }

            for (int i = 0; i < numberOfTasks; i++)
            {
                var result = tasks[i].Result;
            }

            stopwatch.Stop();

            int estimatedMilliseconds = numberOfTasks * sleepTime / numberOfThreads;
            int threshold = estimatedMilliseconds / 3;

            Assert.Greater(estimatedMilliseconds + threshold, stopwatch.ElapsedMilliseconds);
            Assert.Less(estimatedMilliseconds - threshold, stopwatch.ElapsedMilliseconds);
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldContinueFunction(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);

            var task = threadPool.Submit(() => 5).ContinueWith(x => x.ToString());

            Assert.AreEqual("5", task.Result);
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldContinueFunctionAndEvaluateFunction(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);

            var task = threadPool.Submit(() => 5);
            var stringTask = task.ContinueWith(x => x.ToString());

            Assert.AreEqual(5, task.Result);
            Assert.AreEqual("5", stringTask.Result);
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldContinueAndContinueFunction(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);

            var task = threadPool.Submit(() => 5);
            var secondTask = task.ContinueWith(x => x + 1);
            var thirdTask = secondTask.ContinueWith(x => x + 1);

            Assert.AreEqual(5, task.Result);
            Assert.AreEqual(6, secondTask.Result);
            Assert.AreEqual(7, thirdTask.Result);
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldContinueFunctionMultiTask(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);
            var tasks = new List<IMyTask<int>>();
            int numberOfTasks = 30;

            for (int i = 0; i < numberOfTasks; i++)
            {
                int index = i;

                tasks.Add(threadPool.Submit(() => index).ContinueWith(x => x + 1).ContinueWith(x => x + 1));
            }

            for (int i = 0; i < numberOfTasks; i++)
            {
                Assert.AreEqual(i + 2, tasks[i].Result);
            }
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldContinueBeforeDispose(int numberOfThreads)
        {
            var tasks = new List<IMyTask<int>>();
            int numberOfTasks = 30;
            {
                using var threadPool = new MyThreadPool(numberOfThreads);

                for (int i = 0; i < numberOfTasks; i++)
                {
                    int index = i;

                    var task = threadPool.Submit(() => index);
                    tasks.Add(task.ContinueWith(x =>
                    {
                        Thread.Sleep(30);
                        return x + 1;
                    }));
                }
            }

            for (int i = 0; i < numberOfTasks; i++)
            {
                Assert.AreEqual(i + 1, tasks[i].Result);
            }
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldReturnStatus(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);

            var task = threadPool.Submit(() =>
            {
                Thread.Sleep(50);
                return 1;
            });
            var secondTask = task.ContinueWith(x =>
            {
                Thread.Sleep(50);
                return x + 1;
            });

            Assert.IsFalse(task.IsCompleted());
            Assert.IsFalse(secondTask.IsCompleted());

            Thread.Sleep(30);

            Assert.IsFalse(task.IsCompleted());
            Assert.IsFalse(secondTask.IsCompleted());

            Thread.Sleep(40);

            Assert.IsTrue(task.IsCompleted());
            Assert.IsFalse(secondTask.IsCompleted());

            Thread.Sleep(50);

            Assert.IsTrue(task.IsCompleted());
            Assert.IsTrue(secondTask.IsCompleted());
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldThrowInResult(int numberOfThreads)
        {
            using var threadPool = new MyThreadPool(numberOfThreads);

            var task = threadPool.Submit<int>(() =>
            {
                throw new ArgumentException();
            });

            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = task.Result;
            });

            Assert.That(exception.InnerException, Is.TypeOf<ArgumentException>());
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldThrowInSubmit(int numberOfThreads)
        {
            var threadPool = new MyThreadPool(numberOfThreads);

            threadPool.Shutdown();

            Thread.Sleep(5);

            Assert.Throws<InvalidOperationException>(() =>
            {
                threadPool.Submit(() => 5);
            });
        }

        [TestCaseSource(nameof(NumberOfThreadsData))]
        [Test]
        public void ThreadPoolShouldThrowInContinueWith(int numberOfThreads)
        {
            var threadPool = new MyThreadPool(numberOfThreads);

            var task = threadPool.Submit(() => 5);

            threadPool.Shutdown();

            Thread.Sleep(5);

            Assert.Throws<InvalidOperationException>(() =>
            {
                task.ContinueWith((x) => 5);
            });
        }

        private static IEnumerable<TestCaseData> NumberOfThreadsData()
        {
            for (int i = 1; i < 4; i++)
            {
                yield return new TestCaseData(i).SetCategory(i.ToString() + " thread(s)");
            }

            yield return new TestCaseData(8).SetCategory("8 thread(s)");
            yield return new TestCaseData(30).SetCategory("30 thread(s)");
        }
    }
}
