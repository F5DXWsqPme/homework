namespace Solution
{
    using System;

    /// <summary>
    /// Lazy evaluations factory.
    /// </summary>
    public class LazyFactory
    {
        /// <summary>
        /// Create lazy evaluator (one thread).
        /// </summary>
        /// <typeparam name="T">Type of evaluations result.</typeparam>
        /// <param name="supplier">Evaluations function.</param>
        /// <returns>Lazy evaluator.</returns>
        public static ILazy<T> CreateOneThreadLazy<T>(Func<T> supplier)
        {
            return new LazyOneThread<T>(supplier);
        }

        /// <summary>
        /// Create lazy evaluator (multithreading).
        /// </summary>
        /// <typeparam name="T">Type of evaluations result.</typeparam>
        /// <param name="supplier">Evaluations function.</param>
        /// <returns>Lazy evaluator.</returns>
        public static ILazy<T> CreateMultithreadingLazy<T>(Func<T> supplier)
        {
            return new LazyWithMultithreading<T>(supplier);
        }
    }
}
