﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class LinkedList<T> : ICollection<T>
    {
        private Node _headNode;
        private Node _tailNode;

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var node = _headNode;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>
        ///     An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the
        ///     collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1"></see> is
        ///     read-only.
        /// </exception>
        public void Add(T item)
        {
            if (_headNode == null)
            {
                _headNode = new Node {Value = item};
                _tailNode = _headNode;
                Count = 1;
                return;
            }

            _tailNode.Next = new Node {Value = item};
            _tailNode = _tailNode.Next;
            ++Count;
        }

        /// <summary>Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1"></see> is
        ///     read-only.
        /// </exception>
        public void Clear()
        {
            _headNode = null;
            _tailNode = null;
            Count = 0;
        }

        /// <summary>
        ///     Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific
        ///     value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        ///     true if <paramref name="item">item</paramref> is found in the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            foreach (var value in this)
            {
                if (Equals(value, item))
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an
        ///     <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements
        ///     copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see>
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array">array</paramref> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="arrayIndex">arrayIndex</paramref> is less than
        ///     0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The number of elements in the source
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see> is greater than the available space from
        ///     <paramref name="arrayIndex">arrayIndex</paramref> to the end of the destination
        ///     <paramref name="array">array</paramref>.
        /// </exception>
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

        /// <summary>
        ///     Removes the first occurrence of a specific object from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        ///     true if <paramref name="item">item</paramref> was successfully removed from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if
        ///     <paramref name="item">item</paramref> is not found in the original
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1"></see> is
        ///     read-only.
        /// </exception>
        public bool Remove(T item)
        {
            var node = _headNode;
            Node previousNode = null;
            while (node != null)
            {
                if (Equals(node.Value, item))
                {
                    if (previousNode != null)
                        previousNode.Next = node.Next;

                    if (node == _headNode)
                        _headNode = node.Next;

                    if (node == _tailNode)
                        _tailNode = previousNode;

                    node.Next = null;
                    --Count;
                    return true;
                }

                previousNode = node;
                node = node.Next;
            }

            return false;
        }

        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</returns>
        public int Count { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is
        ///     read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.</returns>
        public bool IsReadOnly => false;

        private class Node
        {
            public Node Next { get; set; }
            public T Value { get; set; }
        }
    }
}
