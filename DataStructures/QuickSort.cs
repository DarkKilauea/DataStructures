using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class QuickSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public IEnumerable<T> Sort(IEnumerable<T> source)
        {
            var result = source.ToList();
            RecurseSort(result, 0, result.Count - 1);

            return result;
        }

        private static void RecurseSort(IList<T> source, int start, int end)
        {
            if (start >= end)
                return;
            
            var pivot = Partition(source, start, end);
            RecurseSort(source, start, pivot - 1);
            RecurseSort(source, pivot + 1, end);
        }

        private static int Partition(IList<T> source, int start, int end)
        {
            var pivot = start;
            for (var i = start + 1; i <= end; ++i)
            {
                if (source[i].CompareTo(source[start]) < 0)
                    Swap(source, ++pivot, i);
            }

            Swap(source, start, pivot);

            return pivot;
        }

        private static void Swap(IList<T> source, int leftIndex, int rightIndex)
        {
            var temp = source[leftIndex];
            source[leftIndex] = source[rightIndex];
            source[rightIndex] = temp;
        }
    }
}
