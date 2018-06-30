using System.Collections.Generic;
using Xunit;

namespace DataStructures.Tests
{
    public abstract class BasicCollectionTests
    {
        protected ICollection<int> Instance;

        [Fact]
        public void AfterAddingItem_CountShouldReturnOne()
        {
            Instance.Add(1);

            Assert.Equal(1, Instance.Count);
        }

        [Fact]
        public void AfterAddingMultipleItems_CountShouldReturnThatNumberOfItems()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(1);
            Instance.Add(3);
            Instance.Add(4);
            Instance.Add(9999);

            Assert.Equal(6, Instance.Count);
        }

        [Fact]
        public void AfterClear_CountShouldReturnZero()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            Instance.Clear();

            Assert.Equal(0, Instance.Count);
        }

        [Fact]
        public void IfItemDoesNotExistInCollection_ContainsShouldReturnFalse()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            var exists = Instance.Contains(4);

            Assert.False(exists);
        }

        [Fact]
        public void IfItemExistsInCollection_ContainsShouldReturnTrue()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            var exists = Instance.Contains(2);

            Assert.True(exists);
        }

        [Fact]
        public void Remove_ShouldDecreaseCountByOne()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            Instance.Remove(2);

            Assert.Equal(2, Instance.Count);
        }

        [Fact]
        public void Remove_ShouldReturnFalseIfTheItemIsNotFound()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            var result = Instance.Remove(4);

            Assert.False(result);
        }

        [Fact]
        public void Remove_ShouldReturnTrueIfTheItemIsFound()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            var result = Instance.Remove(2);

            Assert.True(result);
        }

        [Fact]
        public void ShouldBeInitiallyEmpty()
        {
            Assert.Equal(0, Instance.Count);
        }

        [Fact]
        public void ShouldEnumerateItemsInCollection()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            Assert.Equal(new[] {1, 2, 3}, Instance);
        }
    }
}
