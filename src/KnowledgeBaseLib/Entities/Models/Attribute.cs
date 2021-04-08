namespace KnowledgeBaseLib.Entities.Models
{
    public class Attribute : ItemType
    {
        public string Unit { get; init; }
        public ushort Accuracy { get; init; }
        public (int, int) ValueRange { get; init; }

        public override string ToString() =>
            $"{nameof(Unit)}: {Unit}, {nameof(Accuracy)}: {Accuracy.ToString()}, {nameof(ValueRange)}: {ValueRange.ToString()}, {base.ToString()}";
    }
}