using System.Collections.Generic;

namespace ExpertSystemUI.Entities.Models
{
    public class Concept
    {
        public string Name { get; init; }
        public List<Attribute> Attributes { get; } = new();
        public List<Instance> Instances { get; } = new();

        public override string ToString() =>
            $"{nameof(Name)}: {Name}, {nameof(Attributes)}: {string.Join(',', Attributes)}, {nameof(Instances)}: {string.Join(',', Instances)}";
    }
}