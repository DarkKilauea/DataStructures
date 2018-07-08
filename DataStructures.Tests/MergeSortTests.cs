using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Tests
{
    public class MergeSortTests : BaseSortTests
    {
        public MergeSortTests()
        {
            Algorithm = new MergeSort<int>();
        }
    }
}
