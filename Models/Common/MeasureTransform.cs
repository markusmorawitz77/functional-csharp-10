using Models.Common;
using Models.Types.Common;

namespace Models.Common;

public static class MeasureTransforms
{
    public static (Measure a, Measure b) SplitInHalves(this Measure m) => (m, m);
}