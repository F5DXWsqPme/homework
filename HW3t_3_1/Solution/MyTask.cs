namespace Solution
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Thread pool task interface implementation.
    /// </summary>
    /// <typeparam name="TResult">Task result type.</typeparam>
    internal class MyTask<TResult> : IMyTask<TResult>, IRunnableTask
    {
        private object continueTasksLockObject = new object();
        private object finishTaskLockObject = new object();
        private Queue<IRunnableTask> continueTasks = new Queue<IRunnableTask>();
        private volatile bool dataReady;
        private ManualResetEventSlim dataReadyEvent = new ManualResetEventSlim();
        private TResult result;
        private AggregateException runException = null;
        private MyThreadPool hostThreadPool;
        private Func<TResult> function;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyTask{TResult}"/> class.
        /// </summary>
        /// <param name="threadPool">Host thread pool.</param>
        /// <param name="function">Function for evaluations.</param>
        public MyTask(MyThreadPool threadPool, Func<TResult> function)
        {
            this.hostThreadPool = threadPool;
            this.function = function;
        }

        /// <summary>
        /// Gets task result.
        /// </summary>
        /// <exception cref="AggregateException">Throws when task function throw exception.</exception>
        public TResult Result
        {
            get
            {
                this.dataReadyEvent.Wait();

                if (this.runException != null)
                {
                    throw this.runException;
                }

                return this.result;
            }
        }

        /// <summary>
        /// Continue task with task result.
        /// </summary>
        /// <typeparam name="TNewResult">New function result type.</typeparam>
        /// <param name="function">Function for evaluations.</param>
        /// <returns>New task.</returns>
        /// <exception cref="InvalidOperationException">Throws when thread pool closed and data ready.</exception>
        public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> function)
        {
            lock (this.finishTaskLockObject)
            {
                if (this.dataReady)
                {
                    return this.hostThreadPool.Submit(() => function(this.Result));
                }

                var newTask = new MyTask<TNewResult>(this.hostThreadPool, () => function(this.Result));

                lock (this.continueTasksLockObject)
                {
                    this.continueTasks.Enqueue(newTask);
                }

                return newTask;
            }
        }

        /// <summary>
        /// Get task status function.
        /// </summary>
        /// <returns>Task status.</returns>
        public bool IsCompleted() =>
            this.dataReady;

        /// <summary>
        /// Run function.
        /// </summary>
        public void Run()
        {
            try
            {
                this.result = this.function();
            }
            catch (Exception exception)
            {
                this.runException = new AggregateException(exception);
            }

            this.dataReadyEvent.Set();
            this.function = null;

            lock (this.finishTaskLockObject)
            {
                this.dataReady = true;
            }

            foreach (var task in this.GetContinueTasks())
            {
                task.Run();
            }
        }

        private IEnumerable<IRunnableTask> GetContinueTasks()
        {
            while (true)
            {
                IRunnableTask runnableTask;

                lock (this.continueTasksLockObject)
                {
                    if (this.continueTasks.Count > 0)
                    {
                        runnableTask = this.continueTasks.Dequeue();
                    }
                    else
                    {
                        break;
                    }
                }

                yield return runnableTask;
            }
        }
    }
}
