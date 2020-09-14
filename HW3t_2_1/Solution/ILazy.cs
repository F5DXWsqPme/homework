namespace Solution
{
    /// <summary>
    /// Lazy evaluator interface.
    /// </summary>
    /// <typeparam name="T">Lazy function result type.</typeparam>
    public interface ILazy<T>
    {
        /// <summary>
        /// Eval or get evaluations result.
        /// </summary>
        /// <returns>Lazy function result.</returns>
        public T Get();
    }
}
