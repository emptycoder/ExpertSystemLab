using System.Collections.Generic;

namespace ExpertSystemUI.Entities.Models
{
    public class LogicalAxiom
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public Concept Concept { get; init; }
        public List<Attribute> Attributes { get; } = new();
        public string LogicalEquation { get; init; }
    }
}