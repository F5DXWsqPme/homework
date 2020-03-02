/// <summary>
/// Global namespace.
/// </summary>
namespace HW_2_3_24_02_2020
{
    /// <summary>
    /// Queue interface, a first-in-first-out container for tokens.
    /// </summary>
    public interface IQueue
    {
        /// <summary>
        /// Gets element from a head of the queue and removes it.
        /// </summary>
        /// <returns>Element that was on the head.</returns>
        /// <exception cref="System.InvalidOperationException">Throws when queue empty.</exception>
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
