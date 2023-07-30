namespace Models.Types.Common;

public abstract record Measure(string Unit);
public record DiscreteMeasure(string Unit, uint Value) : Measure(Unit);
public record ContinuousMeasure(string Unit, decimal Value) : Measure(Unit);
