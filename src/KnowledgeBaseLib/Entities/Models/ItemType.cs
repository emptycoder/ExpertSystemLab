using System;

namespace KnowledgeBaseLib.Entities.Models
{
    public class ItemType
    {
        public string Name { get; init; }
        public Type Type { get; init; }
        public object Value { get; init; }

        public override string ToString() =>
            $"{nameof(Name)}: {Name}, {nameof(Type)}: {Type}, {nameof(Value)}: {Value}";
    }
}