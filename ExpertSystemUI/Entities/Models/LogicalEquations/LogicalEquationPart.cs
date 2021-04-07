using System;
using System.Collections.Generic;

namespace ExpertSystemUI.Entities.Models.LogicalEquations
{
    public class LogicalEquationPart : ILogicalEquation
    {
        public Instance Instance { get; init; }
        public string VariableName { get; init; }
        
        public bool Result
        {
            get
            {
                if (VariableName.StartsWith("не "))
                {
                    if (Instance[VariableName.Substring(3)].Value is not bool realValue)
                        throw new NotSupportedException();
                    return !realValue;
                }
                else
                {
                    if (Instance[VariableName].Value is not bool realValue)
                        throw new NotSupportedException();
                    return realValue;
                }
            }
        }

        public bool GetResult(List<Instance> parts)
        {
            if (!Result) return false;
            
            parts.Add(Instance);
            return true;
        }

        public override string ToString() => $"{Instance.Name}-{VariableName}";
    }
}