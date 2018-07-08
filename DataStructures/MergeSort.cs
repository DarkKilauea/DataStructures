using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class MergeSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        public IEnumerable<T> Sort(IEnumerable<T> source)
        {
            return RecurseSort(source.ToList());
        }

        private static IList<T> RecurseSort(IList<T> source)
        {
            if (source.Count == 1)
                return source;

            // Step 1: Split the source into 2 lists
            IList<T> left = source
                .Take(source.Count / 2)
                .ToList();
            IList<T> right = source
                .Skip(source.Count / 2)
                .ToList();

            // Step 2: Sort each list
            left = RecurseSort(left);
            right = RecurseSort(right);

            // Step 3: Merge the lists back together
            var result = new List<T>();
            var currentLeftIndex = 0;
            var currentRightIndex = 0;
            while (left.Count > currentLeftIndex && right.Count > currentRightIndex)
            {
                var leftItem = left[currentLeftIndex];
                var rightItem = right[currentRightIndex];

                if (leftItem.CompareTo(rightItem) <= 0)
                {
                    result.Add(leftItem);
                    ++currentLeftIndex;
                }
                else
                {
                    result.Add(rightItem);
                    ++currentRightIndex;
                }
            }

            // Add remaining items (only one of these will have items)
            result.AddRange(left.Skip(currentLeftIndex));
            result.AddRange(right.Skip(currentRightIndex));

            return result;
        }
    }
}
