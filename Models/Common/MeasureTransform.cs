using Models.Common;
using Models.Types.Common;

namespace Models.Common;

public static class MeasureTransforms
{
    public static (Measure a, Measure b) SplitInHalves(this Measure m) => 
    m switch
    {
        DiscreteMeasure d =>
            (d with { Value = (d.Value + 1) / 2 }, d with { Value = d.Value / 2 }),
        ContinuousMeasure c => 
            (c with { Value = c.Value / 2 }, c with { Value = c.Value / 2 })
    };
}