using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    /// <summary>
    /// Class with implementation set interface.
    /// </summary>
    /// <typeparam name="T">Set element type.</typeparam>
    public class Set<T> : ISet<T>
    {
        private TreeNode root;
        private IComparer<T> comparer;
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="Set{T}"/> class.
        /// </summary>
        /// <param name="comparer">Comparator for tree elements distribution.</param>
        public Set(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        /// <summary>
        /// Gets set size.
        /// </summary>
        public int Count => this.count;

        /// <summary>
        /// Gets a value indicating whether that this set is read-only (false).
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Add element to set function.
        /// </summary>
        /// <param name="item">New element.</param>
        /// <returns>True-if succsess, false-if otherwise.</returns>
        public bool Add(T item)
        {
            if (this.root == null)
            {
                this.root = new TreeNode(item);
                this.count++;
                return true;
            }

            TreeNode current = this.root;
            TreeNode previus = null;

            while (true)
            {
                previus = current;

                int compareResult = this.comparer.Compare(item, current.Value);

                if (compareResult < 0)
                {
                    current = current.Left;
                    if (current == null)
                    {
                        previus.Left = new TreeNode(item);
                        this.count++;
                        return true;
                    }
                }
                else if (compareResult > 0)
                {
                    current = current.Right;
                    if (current == null)
                    {
                        previus.Right = new TreeNode(item);
                        this.count++;
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Clear set function.
        /// </summary>
        public void Clear()
        {
            this.root = null;
            this.count = 0;
        }

        /// <summary>
        /// Check element existence function.
        /// </summary>
        /// <param name="item">Element.</param>
        /// <returns>True-if exist, false-if otherwise.</returns>
        public bool Contains(T item)
        {
            return this.ContainsInTree(item, this.root);
        }

        /// <summary>
        /// Copy set to array from position.
        /// </summary>
        /// <param name="array">Array for copy.</param>
        /// <param name="arrayIndex">Index in array.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        /// <summary>
        /// Remove all elements from set which contains in other container.
        /// </summary>
        /// <param name="other">Other container.</param>
        public void ExceptWith(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                this.Remove(item);
            }
        }

        /// <summary>
        /// Get set enumerator.
        /// </summary>
        /// <returns>Items enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (this.root != null)
            {
                return this.root.GetEnumerator();
            }

            return Enumerable.Empty<T>().GetEnumerator();
        }

        /// <summary>
        /// Remove all elements which not contains other container.
        /// </summary>
        /// <param name="other">Other container.</param>
        public void IntersectWith(IEnumerable<T> other)
        {
            var oldRoot = this.root;

            foreach (var item in other)
            {
                if (this.ContainsInTree(item, oldRoot))
                {
                    this.Add(item);
                }
            }
        }

        /// <summary>
        /// Determine whether the current set is proper subset of other container.
        /// </summary>
        /// <param name="other">Other container.</param>
        /// <returns>True-if subset, false-if otherwise.</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return this.IsSubsetOf(other) && !this.SetEquals(other);
        }

        /// <summary>
        /// Determine whether the current set is proper superset of other container.
        /// </summary>
        /// <param name="other">Other container.</param>
        /// <returns>True-if superset, false-if otherwise.</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.IsSupersetOf(other) && !this.SetEquals(other);
        }

        /// <summary>
        /// Determine whether the current set is subset of other container.
        /// </summary>
        /// <param name="other">Other container.</param>
        /// <returns>True-if subset, false-if otherwise.</returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            var comparedSet = new Set<T>(this.comparer);

            foreach (var item in other)
            {
                if (this.Contains(item))
                {
                    comparedSet.Add(item);
                }
            }

            if (comparedSet.Count == this.count)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determine whether the current set is superset of other container.
        /// </summary>
        /// <param name="other">Other container.</param>
        /// <returns>True-if superset, false-if otherwise.</returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            var comparedSet = new Set<T>(this.comparer);

            foreach (var item in other)
            {
                if (!this.Contains(item))
                {
                    return false;
                }

                if (!comparedSet.Add(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection.
        /// </summary>
        /// <param name="other">Other container.</param>
        /// <returns>True-if overlaps, false-if otherwise.</returns>
        public bool Overlaps(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                if (this.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Remove item.
        /// </summary>
        /// <param name="item">Element for delete.</param>
        /// <returns>True-if success, false-if otherwise.</returns>
        public bool Remove(T item)
        {
            TreeNode current = this.root;
            TreeNode previus = null;
            int oldCompareResult = 0;
            int compareResult = 0;

            while (current != null)
            {
                oldCompareResult = compareResult;
                compareResult = this.comparer.Compare(item, current.Value);

                if (compareResult < 0)
                {
                    previus = current;
                    current = current.Left;
                }
                else if (compareResult > 0)
                {
                    previus = current;
                    current = current.Right;
                }
                else
                {
                    this.count--;

                    if (previus != null)
                    {
                        if (current.Left == null)
                        {
                            if (oldCompareResult < 0)
                            {
                                previus.Left = current.Right;
                            }
                            else
                            {
                                previus.Right = current.Right;
                            }

                            return true;
                        }

                        var right = current.Right;

                        if (oldCompareResult < 0)
                        {
                            previus.Left = current.Left;
                        }
                        else
                        {
                            previus.Right = current.Left;
                        }

                        current = current.Left;

                        if (right == null)
                        {
                            return true;
                        }

                        while (current.Right != null)
                        {
                            current = current.Right;
                        }

                        current.Right = right;
                    }
                    else
                    {
                        if (current.Left == null)
                        {
                            this.root = current.Right;

                            return true;
                        }

                        var right = current.Right;

                        this.root = current.Left;
                        current = current.Left;

                        if (right == null)
                        {
                            return true;
                        }

                        while (current.Right != null)
                        {
                            current = current.Right;
                        }

                        current.Right = right;
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the current set and the specified collection contain the same elements.
        /// </summary>
        /// <param name="other">Other container.</param>
        /// <returns>True-if equals, false-if otherwise.</returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            var comparedSet = new Set<T>(this.comparer);

            foreach (var item in other)
            {
                if (!this.Contains(item))
                {
                    return false;
                }

                if (!comparedSet.Add(item))
                {
                    return false;
                }
            }

            if (this.count != comparedSet.Count)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Symmetric substraction function.
        /// </summary>
        /// <param name="other">Other container.</param>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                if (!this.Add(item))
                {
                    this.Remove(item);
                }
            }
        }

        /// <summary>
        /// Union sets function.
        /// </summary>
        /// <param name="other">Other container.</param>
        public void UnionWith(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Add element to set function.
        /// </summary>
        /// <param name="item">New element.</param>
        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        /// <summary>
        /// Get set enumerator.
        /// </summary>
        /// <returns>Items enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Check element existance in tree function.
        /// </summary>
        /// <param name="item">Element.</param>
        /// <param name="tree">Tree.</param>
        /// <returns>True-if exist, false-if otherwise.</returns>
        private bool ContainsInTree(T item, TreeNode tree)
        {
            while (tree != null)
            {
                int compareResult = this.comparer.Compare(item, tree.Value);

                if (compareResult < 0)
                {
                    tree = tree.Left;
                }
                else if (compareResult > 0)
                {
                    tree = tree.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Tree node class.
        /// </summary>
        private class TreeNode : IEnumerable<T>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TreeNode"/> class.
            /// </summary>
            /// <param name="value">Node value.</param>
            public TreeNode(T value)
            {
                this.Value = value;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TreeNode"/> class.
            /// </summary>
            /// <param name="value">Node value.</param>
            /// <param name="left">Left subtree.</param>
            /// <param name="right">Right subtree.</param>
            public TreeNode(T value, TreeNode left, TreeNode right)
            {
                this.Value = value;
                this.Left = left;
                this.Right = right;
            }

            /// <summary>
            /// Gets node value.
            /// </summary>
            public T Value { get; }

            /// <summary>
            /// Gets or sets right subtree.
            /// </summary>
            public TreeNode Right { get; set; }

            /// <summary>
            /// Gets or sets left subtree.
            /// </summary>
            public TreeNode Left { get; set; }

            /// <summary>
            /// Get set enumerator function.
            /// </summary>
            /// <returns>Set elements enumerator.</returns>
            public IEnumerator<T> GetEnumerator()
            {
                if (this.Left != null)
                {
                    foreach (var item in this.Left)
                    {
                        yield return item;
                    }
                }

                yield return this.Value;

                if (this.Right != null)
                {
                    foreach (var item in this.Right)
                    {
                        yield return item;
                    }
                }
            }

            /// <summary>
            /// Get set enumerator function.
            /// </summary>
            /// <returns>Set elements enumerator.</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
