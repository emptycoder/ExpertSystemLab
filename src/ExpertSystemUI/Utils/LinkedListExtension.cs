using System.Collections.Generic;

namespace ExpertSystemUI.Utils
{
    public static class LinkedListExtension
    {
        public static void AddRangeFirst<T>(this LinkedList<T> linkedList, IEnumerable<T> values)
        {
            foreach (var value in values)
                linkedList.AddFirst(value);
        }
    }
}