using System.Collections.Generic;

namespace KnowledgeBaseLib.Utils
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