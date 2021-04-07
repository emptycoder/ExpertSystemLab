using System.Collections.Generic;

namespace ExpertSystemUI.Entities.Models.LogicalEquations
{
    public interface ILogicalEquation
    {
        public bool Result { get; }
        public bool GetResult(HashSet<Instance> parts);
    }
}