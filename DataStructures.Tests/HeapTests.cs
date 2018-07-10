using Xunit;

namespace DataStructures.Tests
{
    public class HeapTests : BasicCollectionTests
    {
        public HeapTests()
        {
            Instance = new Heap<int>();
        }

        [Fact]
        public void AfterAddingItems_ShouldBeAValidHeap()
        {
            Instance.Add(12);
            Instance.Add(20);
            Instance.Add(15);
            Instance.Add(29);
            Instance.Add(23);
            Instance.Add(17);
            Instance.Add(22);
            Instance.Add(35);
            Instance.Add(40);
            Instance.Add(26);
            Instance.Add(51);
            Instance.Add(19);

            // Add test item
            Instance.Add(13);

            var expected = new[] {12, 20, 13, 29, 23, 15, 22, 35, 40, 26, 51, 19, 17};

            Assert.Equal(expected, Instance);
        }

        [Fact]
        public void AfterRemovingAnItem_ShouldBeAValidHeap_AfterSiftingUp()
        {
            Instance.Add(12);
            Instance.Add(20);
            Instance.Add(15);
            Instance.Add(29);
            Instance.Add(23);
            Instance.Add(17);
            Instance.Add(22);
            Instance.Add(35);
            Instance.Add(40);
            Instance.Add(26);
            Instance.Add(51);
            Instance.Add(19);

            Instance.Remove(23);

            var expected = new[] {12, 19, 15, 29, 20, 17, 22, 35, 40, 26, 51};

            Assert.Equal(expected, Instance);
        }

        [Fact]
        public void AfterRemovingAnItem_ShouldBeAValidHeap_AfterSiftingDown()
        {
            Instance.Add(12);
            Instance.Add(20);
            Instance.Add(15);
            Instance.Add(29);
            Instance.Add(23);
            Instance.Add(17);
            Instance.Add(22);
            Instance.Add(35);
            Instance.Add(40);
            Instance.Add(26);
            Instance.Add(51);
            Instance.Add(19);

            Instance.Remove(12);

            var expected = new[] {15, 20, 17, 29, 23, 19, 22, 35, 40, 26, 51};

            Assert.Equal(expected, Instance);
        }
    }
}
