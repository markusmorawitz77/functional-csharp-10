namespace Models.Common;

using Models.Types.Common;

public static class MeasureTransforms
{
    public static (Measure a, Measure b) SplitInHalves(this Measure m) => m.MapAny(
        d => (d with { Value = (d.Value + 1) / 2}, d with { Value = d.Value / 2 }),
        c =>
        {
            Measure half = c with { Value = c.Value / 2 };
            return (half, half);
        });
}