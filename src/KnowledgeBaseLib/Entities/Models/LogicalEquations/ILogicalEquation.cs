using System.Collections.Generic;

namespace KnowledgeBaseLib.Entities.Models.LogicalEquations
{
    public interface ILogicalEquation
    {
        public bool Result { get; }
        public bool GetResult(HashSet<Instance> parts);
    }
}