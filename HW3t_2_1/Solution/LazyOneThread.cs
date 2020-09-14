namespace Solution
{
    using System;

    /// <summary>
    /// Lazy evaluation class (one thread).
    /// </summary>
    /// <typeparam name="T">Lazy function result type.</typeparam>
    internal class LazyOneThread<T> : ILazy<T>
    {
        private Func<T> function;
        private volatile bool dataReady;
        private T data;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyOneThread{T}"/> class.
        /// </summary>
        /// <param name="function">Function for evaluations.</param>
        public LazyOneThread(Func<T> function)
        {
            this.dataReady = false;

            this.function = function;
        }

        /// <summary>
        /// Eval or get evaluations result.
        /// </summary>
        /// <returns>Lazy function result.</returns>
        T ILazy<T>.Get()
        {
            if (!this.dataReady)
            {
                this.data = this.function();
                this.dataReady = true;
            }

            return this.data;
        }
    }
}
