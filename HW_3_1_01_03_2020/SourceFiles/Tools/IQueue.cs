/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_1_01_03_2020
{
    /// <summary>
    /// Queue interface, a first-in-first-out container for tokens.
    /// </summary>
    internal interface IQueue
    {
        /// <summary>
        /// Gets element from a head of the queue and removes it.
        /// </summary>
        /// <returns>Element that was on the head.</returns>
        public abstract IToken Get();

        /// <summary>
        /// Adds element to a tail of the queue.
        /// </summary>
        /// <param name="token">Element to add.</param>
        public abstract void Put(IToken token);

        /// <summary>
        /// Check that the queue does not contain elements.
        /// </summary>
        /// <returns>true-if empty, false-if otherwise.</returns>
        public abstract bool IsEmpty();
    }
}
