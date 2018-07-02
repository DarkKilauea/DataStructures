using System;
using FluentAssertions;
using Xunit;

namespace DataStructures.Tests
{
    public class BinaryTreeTests : BasicCollectionTests
    {
        public BinaryTreeTests()
        {
            Instance = new BinaryTree<int>();
        }

        [Fact]
        public void ShouldBeAValidBinarySearchTree()
        {
            Instance.Add(20);
            Instance.Add(10);
            Instance.Add(30);
            Instance.Add(5);
            Instance.Add(40);

            var actual = BinarySearchTreeIsValid(((BinaryTree<int>) Instance).RootNode);

            actual.Should().BeTrue();
        }

        private bool BinarySearchTreeIsValid(BinaryTree<int>.Node node, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            if (node == null)
                return true;

            if (node.Value < minValue || node.Value > maxValue)
                return false;

            return BinarySearchTreeIsValid(node.Left, minValue, node.Value) &&
                BinarySearchTreeIsValid(node.Right, node.Value, maxValue);
        }
    }
}
