namespace Models.Types.Common;

public static class MeasuresLinq
{
    public static DiscreteMeasure Add(this DiscreteMeasure a, DiscreteMeasure b) =>
        a.Unit == b.Unit ? a with { Value = a.Value + b.Value }
        : throw new ArgumentException();

    public static DiscreteMeasure Sum(this IEnumerable<DiscreteMeasure> sequence) =>
        sequence.Aggregate(Add);
}