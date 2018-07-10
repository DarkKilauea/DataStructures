using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class Heap<T> : ICollection<T> where T : IComparable<T>
    {
        private readonly List<T> _storage;

        public Heap()
        {
            _storage = new List<T> {default(T)};
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _storage
                .Skip(1)
                .GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public void Add(T item)
        {
            _storage.Add(item);

            SiftUpwards(_storage.Count - 1);
        }

        /// <summary>Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public void Clear()
        {
            _storage.Clear();
            _storage.Add(default(T));
        }

        /// <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.</summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>true if <paramref name="item">item</paramref> is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.</returns>
        public bool Contains(T item)
        {
            return FindIndexOfItem(item, 1) > 0;
        }

        /// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.</summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array">array</paramref> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex">arrayIndex</paramref> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"></see> is greater than the available space from <paramref name="arrayIndex">arrayIndex</paramref> to the end of the destination <paramref name="array">array</paramref>.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _storage.CopyTo(1, array, arrayIndex, _storage.Count - 1);
        }

        /// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>true if <paramref name="item">item</paramref> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if <paramref name="item">item</paramref> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.</returns>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public bool Remove(T item)
        {
            var index = FindIndexOfItem(item, 1);
            if (index == -1)
                return false;

            var lastIndex = _storage.Count - 1;

            var temp = _storage[lastIndex];
            _storage[index] = temp;
            _storage.RemoveAt(lastIndex);
            --lastIndex;

            if (index == 1 || _storage[index / 2].CompareTo(temp) < 0)
                SiftDownwards(index, lastIndex);
            else
                SiftUpwards(index);

            return true;
        }

        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</returns>
        public int Count => _storage.Count - 1;

        /// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.</returns>
        public bool IsReadOnly => false;

        private void SiftUpwards(int index)
        {
            if (index == 0)
                throw new ArgumentException("Index cannot be 0", nameof(index));

            while (true)
            {
                if (index == 1)
                    break;

                var parentIndex = index / 2;
                if (_storage[parentIndex].CompareTo(_storage[index]) <= 0)
                    break;

                Swap(_storage, parentIndex, index);
                index = parentIndex;
            }
        }

        private void SiftDownwards(int index, int endingIndex)
        {
            var currentIndex = index;
            while (true)
            {
                var childIndex = currentIndex * 2;
                if (childIndex > endingIndex)
                    break;

                if (childIndex + 1 <= endingIndex && _storage[childIndex + 1].CompareTo(_storage[childIndex]) < 0)
                    ++childIndex;

                if (_storage[currentIndex].CompareTo(_storage[childIndex]) <= 0)
                    break;

                Swap(_storage, childIndex, currentIndex);
                currentIndex = childIndex;
            }
        }

        private static void Swap(IList<T> source, int leftIndex, int rightIndex)
        {
            var temp = source[leftIndex];
            source[leftIndex] = source[rightIndex];
            source[rightIndex] = temp;
        }

        private int FindIndexOfItem(T item, int startingIndex)
        {
            if (_storage[startingIndex].Equals(item))
                return startingIndex;

            var foundIndex = -1;

            // Search the left child
            var childIndex = startingIndex * 2;
            if (childIndex < _storage.Count && _storage[childIndex].CompareTo(item) <= 0)
                foundIndex = FindIndexOfItem(item, childIndex);

            // If we haven't found a match, search the right child
            ++childIndex;
            if (foundIndex == -1 && childIndex < _storage.Count && _storage[childIndex].CompareTo(item) <= 0)
                foundIndex = FindIndexOfItem(item, childIndex);

            return foundIndex;
        }
    }
}
