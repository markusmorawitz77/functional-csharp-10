using Models.Types.Common;
using Models.Types.Components;

namespace Models.Types.Products;

public class AssemblySpecification
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IEnumerable<(Part part, DiscreteMeasure quantity)> Components { get; init; } 
        = Enumerable.Empty<(Part part, DiscreteMeasure quantity)>();
    public IEnumerable<(InventoryItem item, Measure quantity)> Consumables { get; init; } 
        = Enumerable.Empty<(InventoryItem item, Measure quantity)>();
}