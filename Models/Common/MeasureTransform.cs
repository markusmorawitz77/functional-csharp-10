using Models.Common;
using Models.Types.Common;

namespace Models.Common;

public static class MeasureTransforms
{
    public static (Measure a, Measure b) SplitInHalves(this Measure m) => 
    m.AsDiscriminatedUnion() switch
    {
        DiscreteMeasure d => SplitInHalves(d),
        ContinuousMeasure c => SplitInHalves(c),
        _ => default!
    };

    private static (Measure a, Measure b) SplitInHalves(this DiscreteMeasure d) =>
        (d with { Value = (d.Value + 1) / 2 }, d with { Value = d.Value / 2 });

    private static (Measure a, Measure b) SplitInHalves(this ContinuousMeasure c)
    {
        Measure half = c with { Value = c.Value / 2 };
        return (half, half);
    }
}