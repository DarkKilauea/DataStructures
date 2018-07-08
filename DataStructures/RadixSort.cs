using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class RadixSort : ISortAlgorithm<int>
    {
        private readonly List<int>[] _buckets = new List<int>[10];

        public IEnumerable<int> Sort(IEnumerable<int> source)
        {
            for (var i = 0; i < _buckets.Length; ++i)
                _buckets[i] = new List<int>();

            var count = 0;
            while (true)
            {
                PopulateBuckets(source, count);
                if (!_buckets.Any(bucket => !ReferenceEquals(bucket, _buckets[0]) && bucket.Count > 0))
                    break;

                source = BucketsToSortedNumbers();
                ++count;
            }

            return source;
        }

        private void PopulateBuckets(IEnumerable<int> source, int iteration)
        {
            foreach (var bucket in _buckets)
                bucket.Clear();

            foreach (var number in source)
            {
                var digit = number / (int)Math.Pow(10, iteration) % 10;
                _buckets[digit].Add(number);
            }
        }

        private IEnumerable<int> BucketsToSortedNumbers()
        {
            return _buckets.SelectMany(bucket => bucket).ToList();
        }
    }
}
