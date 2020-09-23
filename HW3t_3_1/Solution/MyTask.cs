namespace Solution
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Thread pool task interface implementation.
    /// </summary>
    /// <typeparam name="TResult">Task result type.</typeparam>
    internal class MyTask<TResult> : IMyTask<TResult>, IContinueTask
    {
        private object continueTasksLockObject = new object();
        private object finishTaskLockObject = new object();
        private Queue<IContinueTask> continueTasks = new Queue<IContinueTask>();
        private volatile bool dataReady;
        private ManualResetEventSlim dataReadyEvent = new ManualResetEventSlim();
        private TResult result;
        private AggregateException runException = null;
        private InvalidOperationException invalidOperationException = null;
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
        /// <exception cref="InvalidOperationException">Throws when task was invalidated.</exception>
        public TResult Result
        {
            get
            {
                this.dataReadyEvent.Wait();

                if (this.invalidOperationException != null)
                {
                    throw this.invalidOperationException;
                }

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
        public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> function)
        {
            if (this.dataReady)
            {
                return this.hostThreadPool.Submit(() => function(this.Result));
            }

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

            this.function = null;

            lock (this.finishTaskLockObject)
            {
                this.dataReady = true;
            }

            this.dataReadyEvent.Set();

            foreach (var task in this.GetContinueTasks())
            {
                this.hostThreadPool.SubmitContinueTask(task);
            }
        }

        /// <summary>
        /// Set task invalid.
        /// </summary>
        public void InvalidateTask()
        {
            this.invalidOperationException = new InvalidOperationException("Task invalidated.");

            this.function = null;
            this.hostThreadPool = null;
            this.runException = null;

            lock (this.finishTaskLockObject)
            {
                this.dataReady = true;
            }

            this.dataReadyEvent.Set();
        }

        private IEnumerable<IContinueTask> GetContinueTasks()
        {
            while (true)
            {
                IContinueTask task;

                lock (this.continueTasksLockObject)
                {
                    if (this.continueTasks.Count > 0)
                    {
                        task = this.continueTasks.Dequeue();
                    }
                    else
                    {
                        break;
                    }
                }

                yield return task;
            }
        }
    }
}
