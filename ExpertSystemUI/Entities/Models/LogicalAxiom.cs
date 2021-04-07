using System.Collections.Generic;
using System.Linq;
using ExpertSystemUI.Entities.Models.LogicalEquations;

namespace ExpertSystemUI.Entities.Models
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