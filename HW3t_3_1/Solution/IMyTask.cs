namespace Solution
{
    using System;

    /// <summary>
    /// Thread pool task interface.
    /// </summary>
    /// <typeparam name="TResult">Task result.</typeparam>
    public interface IMyTask<TResult>
    {
        /// <summary>
        /// Gets task result.
        /// </summary>
        /// <exception cref="AggregateException">Throws when task function throw exception.</exception>
        public TResult Result
        {
            get;
        }

        /// <summary>
        /// Get task status function.
        /// </summary>
        /// <returns>Task status.</returns>
        public bool IsCompleted();

        /// <summary>
        /// Continue task with task result.
        /// </summary>
        /// <typeparam name="TNewResult">New function result type.</typeparam>
        /// <param name="function">Function for evaluations.</param>
        /// <returns>New task.</returns>
        public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> function);
    }
}
