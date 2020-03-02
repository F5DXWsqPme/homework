/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_2_01_03_2020
{
    /// <summary>
    /// Hash interface.
    /// </summary>
    public interface IHash
    {
        /// <summary>
        /// Gets array size in hash table.
        /// </summary>
        /// <returns>Array size (maximum hash + 1).</returns>
        public abstract int GetArraySize();

        /// <summary>
        /// Gets element hash.
        /// </summary>
        /// <returns>Element hash.</returns>
        public abstract int GetHash(int element);
    }
}
