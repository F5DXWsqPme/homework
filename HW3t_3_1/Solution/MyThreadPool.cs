namespace Solution
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Thread pool class.
    /// </summary>
    public class MyThreadPool : IDisposable
    {
        private Thread[] threads;
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;
        private SynchronizedQueue<Action> tasksForRunning;
        private object shutdownLockObject = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="MyThreadPool"/> class.
        /// </summary>
        /// <param name="numberOfThreads">Number of threads in thread pool.</param>
        public MyThreadPool(int numberOfThreads)
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            this.cancellationToken = this.cancellationTokenSource.Token;
            this.tasksForRunning = new SynchronizedQueue<Action>();

            this.threads = new Thread[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                this.threads[i] = new Thread(() =>
                {
                    while (true)
                    {
                        (Action task, bool exitFlag) = this.tasksForRunning.Dequeue();

                        if (exitFlag)
                        {
                            break;
                        }

                        task();
                    }
                });

                this.threads[i].Start();
            }
        }

        /// <summary>
        /// Add new task method.
        /// </summary>
        /// <typeparam name="TResult">Task result type.</typeparam>
        /// <param name="function">Task function.</param>
        /// <returns>Created task.</returns>
        public IMyTask<TResult> Submit<TResult>(Func<TResult> function)
        {
            if (this.cancellationToken.IsCancellationRequested)
            {
                throw new InvalidOperationException("Thread pool closed.");
            }

            lock (this.shutdownLockObject)
            {
                if (this.cancellationToken.IsCancellationRequested)
                {
                    throw new InvalidOperationException("Thread pool closed.");
                }

                var task = new MyTask<TResult>(this, function);
                Action action = () =>
                {
                    task.Run();
                };

                this.tasksForRunning.Enqueue(action);

                return task;
            }
        }

        /// <summary>
        /// Add new continue task method.
        /// </summary>
        /// <param name="newTask">New continue task.</param>
        public void SubmitContinueTask(IContinueTask newTask)
        {
            if (this.cancellationToken.IsCancellationRequested)
            {
                newTask.InvalidateTask();
                return;
            }

            lock (this.shutdownLockObject)
            {
                if (this.cancellationToken.IsCancellationRequested)
                {
                    newTask.InvalidateTask();
                    return;
                }

                Action action = () =>
                {
                    newTask.Run();
                };

                this.tasksForRunning.Enqueue(action);
            }
        }

        /// <summary>
        /// Wait all threads method.
        /// </summary>
        public void Shutdown()
        {
            lock (this.shutdownLockObject)
            {
                this.cancellationTokenSource.Cancel();
            }

            this.tasksForRunning.Shutdown();

            foreach (var thread in this.threads)
            {
                thread.Join();
            }
        }

        /// <summary>
        /// Dispose function.
        /// </summary>
        public void Dispose()
        {
            this.Shutdown();
        }

        /// <summary>
        /// Syncronized queue class.
        /// </summary>
        private class SynchronizedQueue<T>
        {
            private Queue<T> buffer = new Queue<T>();
            private volatile bool doExit = false;

            /// <summary>
            /// Enqueue item in queue.
            /// </summary>
            /// <param name="item">New item.</param>
            public void Enqueue(T item)
            {
                lock (this.buffer)
                {
                    this.buffer.Enqueue(item);
                    Monitor.Pulse(this.buffer);
                }
            }

            /// <summary>
            /// Dequeue element.
            /// </summary>
            /// <returns>First element in queue and exit flag.</returns>
            public (T, bool) Dequeue()
            {
                lock (this.buffer)
                {
                    while (this.buffer.Count == 0)
                    {
                        if (this.doExit)
                        {
                            return (default(T), true);
                        }

                        Monitor.Wait(this.buffer);
                    }

                    return (this.buffer.Dequeue(), false);
                }
            }

            /// <summary>
            /// Shutdown function.
            /// </summary>
            public void Shutdown()
            {
                this.doExit = true;
                lock (this.buffer)
                {
                    Monitor.PulseAll(this.buffer);
                }
            }
        }
    }
}
