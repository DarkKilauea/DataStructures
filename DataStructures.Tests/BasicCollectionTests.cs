using System.Collections.Generic;
using FluentAssertions;
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

            Instance.Count.Should().Be(1);
        }

        [Fact]
        public void AfterAddingMultipleItems_CountShouldReturnThatNumberOfItems()
        {
            Instance.Add(8);
            Instance.Add(3);
            Instance.Add(10);
            Instance.Add(1);
            Instance.Add(6);
            Instance.Add(14);
            Instance.Add(4);
            Instance.Add(7);
            Instance.Add(13);

            Instance.Count.Should().Be(9);
        }

        [Fact]
        public void AfterClear_CountShouldReturnZero()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            Instance.Clear();

            Instance.Count.Should().Be(0);
        }

        [Fact]
        public void AfterRemove_FirstItem_CollectionShouldStillEnumerate()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);
            Instance.Remove(1);

            Instance.Should().BeEquivalentTo(new[] {2, 3});
        }

        [Fact]
        public void AfterRemove_LastItem_CollectionShouldStillEnumerate()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);
            Instance.Remove(3);

            Instance.Should().BeEquivalentTo(new[] {1, 2});
        }

        [Fact]
        public void AfterRemove_MiddleItem_CollectionShouldStillEnumerate()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);
            Instance.Remove(2);

            Instance.Should().BeEquivalentTo(new[] {1, 3});
        }

        [Fact]
        public void AfterRemove_ShouldDecreaseCountByOne()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            Instance.Remove(2);

            Instance.Count.Should().Be(2);
        }

        [Fact]
        public void AfterRemove_ShouldReturnFalseIfTheItemIsNotFound()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            var result = Instance.Remove(4);

            result.Should().BeFalse();
        }

        [Fact]
        public void AfterRemove_ShouldReturnTrueIfTheItemIsFound()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            var result = Instance.Remove(2);

            result.Should().BeTrue();
        }

        [Fact]
        public void AfterRemove_ThenAdd_CollectionShouldStillEnumerate()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);
            Instance.Remove(2);
            Instance.Add(4);

            Instance.Should().BeEquivalentTo(new[] {1, 3, 4});
        }

        [Fact]
        public void IfItemDoesNotExistInCollection_ContainsShouldReturnFalse()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            var exists = Instance.Contains(4);

            exists.Should().BeFalse();
        }

        [Fact]
        public void IfItemExistsInCollection_ContainsShouldReturnTrue()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            var exists = Instance.Contains(2);

            exists.Should().BeTrue();
        }

        [Fact]
        public void ShouldBeInitiallyEmpty()
        {
            Instance.Count.Should().Be(0);
        }

        [Fact]
        public void ShouldEnumerateItemsInCollection()
        {
            Instance.Add(1);
            Instance.Add(2);
            Instance.Add(3);

            Instance.Should().BeEquivalentTo(new[] {1, 2, 3});
        }
    }
}
