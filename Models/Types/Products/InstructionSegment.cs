using Models.Types.Common;
using Models.Types.Components;

namespace Models.Types.Products;

public abstract record InstructionSegment();

public record TextSegment(string Value) : InstructionSegment;

public abstract record PartSegment(Part Part, DiscreteMeasure Quantity) : InstructionSegment
{
    public Option<DiscreteMeasure> NonUnitQuantity =>
        this.Quantity.Optional().Filter(count => count.Value > 1);
}

public record NewPartSegment(Part Part, DiscreteMeasure Quantity)
    : PartSegment(Part, Quantity);

public record PartReferenceSegment(Part Part, DiscreteMeasure Quantity)
    : PartSegment(Part, Quantity);