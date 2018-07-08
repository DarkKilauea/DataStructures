using System;
using System.Collections.Generic;

namespace DataStructures
{
    public interface ISortAlgorithm<T> where T : IComparable<T>
    {
        IEnumerable<T> Sort(IEnumerable<T> source);
    }
}
