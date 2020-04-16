using System;
using System.Collections;
using System.Collections.Generic;

namespace Solution
{
    /// <summary>
    /// Class with implementation set interface.
    /// </summary>
    public class Set<T> : ISet<T>
    {
        private TreeNode root;
        private IComparer<T> comparer;
        private int count;

        public Set(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public int Count => count;

        public bool IsReadOnly => false;

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

                int compareResult = comparer.Compare(item, current.Value);

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

        public void Clear()
        {
            this.root = null;
            this.count = 0;
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
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

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class TreeNode
        {
            public T Value { get; set; }
            public TreeNode Right { get; set; }
            public TreeNode Left { get; set; }

            public TreeNode(T value)
            {
                this.Value = value;
            }

            public TreeNode(T value, TreeNode left, TreeNode right)
            {
                this.Value = value;
                this.Left = left;
                this.Right = right;
            }
        }
    }
}
