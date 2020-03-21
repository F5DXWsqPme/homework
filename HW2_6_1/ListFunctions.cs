using System;
using System.Collections.Generic;

/// <summary>
/// Global namespace.
/// </summary>
namespace HW2_6_1
{
    /// <summary>
    /// Class with implementation list functions.
    /// </summary>
    public class ListFunctions
    {
        /// <summary>
        /// Transform list function.
        /// </summary>
        /// <param name="list">List for transform.</param>
        /// <param name="transform">Transform function.</param>
        /// <returns>List after transforms.</returns>
        public static List<int> Map(List<int> list, Func<int, int> transform)
        {
            var result = new List<int>(list.Count);

            foreach (int item in list)
            {
                result.Add(transform(item));
            }

            return result;
        }

        /// <summary>
        /// Filter list function.
        /// </summary>
        /// <param name="list">List for filter.</param>
        /// <param name="filter">Filter function.</param>
        /// <returns>List after deleting wrong elements.</returns>
        public static List<int> Filter(List<int> list, Func<int, bool> filter)
        {
            var result = new List<int>();

            foreach (int item in list)
            {
                if (filter(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Transform list function with accumalate variable.
        /// </summary>
        /// <param name="list">List for transform.</param>
        /// <param name="accumulator">Accumulate variable initial value.</param>
        /// <param name="transform">Transform function.</param>
        /// <returns>List after transforms.</returns>
        public static List<int> Fold(List<int> list, int accumulator, Func<int, int, int> transform)
        {
            var result = new List<int>(list.Count);

            foreach (int item in list)
            {
                accumulator = transform(accumulator, item);
                result.Add(accumulator);
            }

            return result;
        }
    }
}
