using System;
using System.Linq;
using Xunit;

namespace DataStructures.Tests
{
    public abstract class BaseSortTests
    {
        protected ISortAlgorithm<int> Algorithm;

        [Fact]
        public void ShouldReturnAlreadySortedList()
        {
            var expected = Enumerable.Range(0, 1000).ToArray();

            var actual = Algorithm.Sort(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldSortListOfNumbers()
        {
            var source = new[] {5, 2, 15, 99, 5, 16, 3, 10, 8, 7};
            var expected = new[] {2, 3, 5, 5, 7, 8, 10, 15, 16, 99};

            var actual = Algorithm.Sort(source);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldSortLargeListOfRandomNumbers()
        {
            var random = new Random();
            var source = Enumerable.Repeat(0, 1_000_000)
                .Select(_ => random.Next(100000))
                .ToList();
            var expected = source
                .OrderBy(x => x)
                .ToList();

            var actual = Algorithm.Sort(source);

            Assert.Equal(expected, actual);
        }
    }
}
