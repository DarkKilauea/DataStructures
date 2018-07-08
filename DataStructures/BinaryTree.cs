using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class BinaryTree<T> : ICollection<T> where T : IComparable<T>
    {
        public class Node
        {
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public T Value { get; set; }

            /// <summary>Returns a string that represents the current object.</summary>
            /// <returns>A string that represents the current object.</returns>
            public override string ToString()
            {
                return Value.ToString();
            }
        }

        public Node RootNode { get; private set; }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return GetListOfAllNodes(RootNode)
                .Select(node => node.Value)
                .GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public void Add(T item)
        {
            if (RootNode == null)
            {
                RootNode = new Node {Value = item};
                Count = 1;
                return;
            }

            var node = RootNode;
            while (node != null)
            {
                if (node.Value.CompareTo(item) > 0)
                {
                    if (node.Left == null)
                    {
                        node.Left = new Node {Parent = node, Value = item};
                        break;
                    }

                    node = node.Left;
                }
                else
                {
                    if (node.Right == null)
                    {
                        node.Right = new Node {Parent = node, Value = item};
                        break;
                    }

                    node = node.Right;
                }
            }

            ++Count;
        }

        /// <summary>Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public void Clear()
        {
            RootNode = null;
            Count = 0;
        }

        /// <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.</summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>true if <paramref name="item">item</paramref> is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.</returns>
        public bool Contains(T item)
        {
            var node = RootNode;
            while (node != null)
            {
                if (Equals(node.Value, item))
                    return true;

                node = node.Value.CompareTo(item) > 0 ? node.Left : node.Right;
            }

            return false;
        }

        /// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.</summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array">array</paramref> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex">arrayIndex</paramref> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"></see> is greater than the available space from <paramref name="arrayIndex">arrayIndex</paramref> to the end of the destination <paramref name="array">array</paramref>.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (Count > array.Length + arrayIndex)
                throw new ArgumentException(
                    "The number of elements in the source is greater than the available space from arrayIndex to the end of the destination array.",
                    nameof(array));

            var currentIndex = 0;
            foreach (var value in this)
            {
                array[arrayIndex + currentIndex] = value;
                ++currentIndex;
            }
        }

        /// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>true if <paramref name="item">item</paramref> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if <paramref name="item">item</paramref> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.</returns>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public bool Remove(T item)
        {
            var node = RootNode;
            while (node != null)
            {
                if (Equals(node.Value, item))
                    break;

                node = node.Value.CompareTo(item) > 0 ? node.Left : node.Right;
            }

            if (node == null)
                return false;

            RemoveNode(node);
            --Count;

            return true;
        }

        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</returns>
        public int Count { get; private set; }

        /// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.</returns>
        public bool IsReadOnly => false;

        private static IEnumerable<Node> GetListOfAllNodes(Node parentNode)
        {
            var list = Enumerable.Empty<Node>();

            if (parentNode?.Left != null)
                list = list.Concat(GetListOfAllNodes(parentNode.Left));

            if (parentNode != null)
                list = list.Append(parentNode);

            if (parentNode?.Right != null)
                list = list.Concat(GetListOfAllNodes(parentNode.Right));

            return list;
        }

        private void RemoveNode(Node node)
        {
            void SetParentNode(Node newNode)
            {
                if (RootNode == node)
                    RootNode = newNode;
                else
                {
                    if (node.Parent.Left == node)
                        node.Parent.Left = newNode;
                    else
                        node.Parent.Right = newNode;
                }
            }

            // Case 1: Node has two children
            if (node.Left != null && node.Right != null)
            {
                // Find the value that should replace this one
                var successor = node.Right;
                while (successor.Left != null)
                {
                    successor = successor.Left;
                }

                // Replace that value and remove the successor node
                node.Value = successor.Value;
                RemoveNode(successor);
            }
            // Case 2: Node has one child
            else if (node.Left != null)
                SetParentNode(node.Left);
            else if (node.Right != null)
                SetParentNode(node.Right);
            // Case 3: Node has no children
            else
                SetParentNode(null);
        }
    }
}
