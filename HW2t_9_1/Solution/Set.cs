using System;
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
            TreeNode current = this.root;

            while (current != null)
            {
                int compareResult = this.comparer.Compare(item, current.Value);

                if (compareResult < 0)
                {
                    current = current.Left;
                }
                else if (compareResult > 0)
                {
                    current = current.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
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

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
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

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
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
            if (this.root != null)
            {
                return this.root.GetEnumerator();
            }

            return Enumerable.Empty<T>().GetEnumerator();
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
            /// Gets or sets node value.
            /// </summary>
            public T Value { get; set; }

            /// <summary>
            /// Gets or sets right subtree.
            /// </summary>
            public TreeNode Right { get; set; }

            /// <summary>
            /// Gets or sets left subtree.
            /// </summary>
            public TreeNode Left { get; set; }
        }
    }
}
