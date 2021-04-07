using System;
using System.Collections.Generic;
using ExpertSystemUI.Entities.Enums;

namespace ExpertSystemUI.Entities.Models.LogicalEquations
{
    public class LogicalEquation : ILogicalEquation
    {
        public ILogicalEquation FirstInstance { get; set; }
        public ILogicalEquation SecondInstance { get; set; }
        public LogicalConnection LogicalConnection { get; set; }

        public virtual bool Result
        {
            get
            {
                return FirstInstance switch
                {
                    not null when SecondInstance is null => FirstInstance.Result,
                    null when SecondInstance is not null => SecondInstance.Result,
                    null when SecondInstance is null => false,
                    _ => LogicalConnection switch
                    {
                        LogicalConnection.AND => FirstInstance.Result & SecondInstance.Result,
                        LogicalConnection.OR => FirstInstance.Result | SecondInstance.Result,
                        _ => throw new ArgumentOutOfRangeException()
                    }
                };
            }
        }

        public bool GetResult(HashSet<Instance> parts)
        {
            return FirstInstance switch
            {
                not null when SecondInstance is null => FirstInstance.GetResult(parts),
                null when SecondInstance is not null => SecondInstance.GetResult(parts),
                null when SecondInstance is null => false,
                _ => LogicalConnection switch
                {
                    LogicalConnection.AND => FirstInstance.GetResult(parts) & SecondInstance.GetResult(parts),
                    LogicalConnection.OR => FirstInstance.GetResult(parts) | SecondInstance.GetResult(parts),
                    _ => throw new ArgumentOutOfRangeException()
                }
            };
        }

        public override string ToString()
        {
            if (FirstInstance is null && SecondInstance is null)
                return string.Empty;
            else if (FirstInstance is null)
                return SecondInstance.ToString();
            else if (SecondInstance is null)
                return FirstInstance.ToString();
            else
                return $"({FirstInstance} {LogicalConnectionUtil.ToString(LogicalConnection)} {SecondInstance})";
        }
    }
}