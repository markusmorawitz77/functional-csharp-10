namespace Models.Types.Common;

public abstract record Measure(string Unit);
public record DiscreteMeasure(string Unit, uint Value) : Measure(Unit);
public record ContinuousMeasure(string Unit, decimal Value) : Measure(Unit);

public static class MeasureExtensions
{
    public static Measure AsDiscriminatedUnion(this Measure m) =>
        m switch
        {
            DiscreteMeasure or ContinuousMeasure => m,
            _ => throw new ArgumentException("Measure must be either DiscreteMeasure or ContinuousMeasure")
        }
}