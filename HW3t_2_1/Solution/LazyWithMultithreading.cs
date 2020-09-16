namespace Solution
{
    using System;
    using System.Threading;

    /// <summary>
    /// Lazy evaluation class (multithreading).
    /// </summary>
    /// <typeparam name="T">Lazy function result type.</typeparam>
    internal class LazyWithMultithreading<T> : ILazy<T>
    {
        private Func<T> function;
        private ManualResetEventSlim dataReadyEvent;
        private volatile bool evaluationsStarted;
        private T data;
        private object syncronizationObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyWithMultithreading{T}"/> class.
        /// </summary>
        /// <param name="function">Function for evaluations.</param>
        public LazyWithMultithreading(Func<T> function)
        {
            this.evaluationsStarted = false;
            this.syncronizationObject = new object();

            this.dataReadyEvent = new ManualResetEventSlim();

            this.function = function;
        }

        /// <summary>
        /// Eval or get evaluations result.
        /// </summary>
        /// <returns>Lazy function result.</returns>
        T ILazy<T>.Get()
        {
            if (!this.evaluationsStarted)
            {
                lock (this.syncronizationObject)
                {
                    if (!this.evaluationsStarted)
                    {
                        this.evaluationsStarted = true;
                        this.data = this.function();
                        this.function = null;
                        this.dataReadyEvent.Set();
                    }
                }
            }

            this.dataReadyEvent.Wait();

            return this.data;
        }
    }
}
