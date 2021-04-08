using System.Collections.Generic;
using System.Linq;
using KnowledgeBaseLib.Entities.Models.LogicalEquations;

namespace KnowledgeBaseLib.Entities.Models
{
    public class LogicalAxiom
    {
        public string Name { get; init; }
        public List<Attribute> Attributes { get; } = new();
        public LogicalEquation LogicalEquationPart { get; init; }
        public IEnumerable<Instance> Result { get; init; }

        public override string ToString() =>
            $"{LogicalEquationPart} то {string.Join(" АБО ", Result.Select(item => item.Name))}";
    }
}