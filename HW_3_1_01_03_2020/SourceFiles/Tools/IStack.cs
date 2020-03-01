/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_1_01_03_2020
{
    /// <summary>
    /// Stack interface, a last-in-first-out container for tokens.
    /// </summary>
    internal interface IStack
    {
        /// <summary>
        /// Gets element from a top of the stack and removes it.
        /// </summary>
        /// <returns>Element that was on the top.</returns>
        public abstract IToken Pop();

        /// <summary>
        /// Adds element to a top of the stack.
        /// </summary>
        /// <param name="token">Element to add.</param>
        public abstract void Push(IToken token);

        /// <summary>
        /// This function removes all items from the stack.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Check that the stack does not contain elements.
        /// </summary>
        /// <returns>true-if empty, false-if otherwise.</returns>
        public abstract bool IsEmpty();
    }
}
