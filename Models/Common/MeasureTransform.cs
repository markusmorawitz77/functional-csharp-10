using Models.Common;
using Models.Types.Common;

namespace Models.Common;

public static class MeasureTransforms
{
    public static (Measure a, Measure b) SplitInHalves(this Measure m) => 
    m switch
    {
        DiscreteMeasure d => SplitInHalves(d),
        ContinuousMeasure c => SplitInHalves(c),
        _ => throw new ArgumentException("Measure must be either DiscreteMeasure or ContinuousMeasure")
    };

    private static (Measure a, Measure b) SplitInHalves(this DiscreteMeasure d) =>
        (d with { Value = (d.Value + 1) / 2 }, d with { Value = d.Value / 2 });

    private static (Measure a, Measure b) SplitInHalves(this ContinuousMeasure c)
    {
        Measure half = c with { Value = c.Value / 2 };
        return (half, half);
    }
}