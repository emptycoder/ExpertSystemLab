using System;

namespace ExpertSystemUI.Entities.Enums
{
    public enum LogicalConnection
    {
        // ReSharper disable once InconsistentNaming
        OR = 0,
        // ReSharper disable once InconsistentNaming
        AND = 1
    }

    public static class LogicalConnectionUtil
    {
        public static LogicalConnection ToEnum(string str) => str.Trim() switch
        {
            "АБО" => LogicalConnection.OR,
            "І" => LogicalConnection.AND,
            _ => throw new NotSupportedException()
        };

        public static string ToString(this LogicalConnection logicalConnection) => logicalConnection switch
        {
            LogicalConnection.OR => "АБО",
            LogicalConnection.AND => "І",
            _ => throw new NotSupportedException()
        };
    }
}