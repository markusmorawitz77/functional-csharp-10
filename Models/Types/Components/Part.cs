using Models.Types.Common;

namespace Models.Types.Components;

public abstract record InventoryItem(Guid Id, string Name, StockKeepingUnit Sku);
public record Part(Guid Id, string Name, StockKeepingUnit Sku)
    : InventoryItem(Id, Name, Sku);

public record Matirial(Guid Id, string Name, StockKeepingUnit Sku,
    ContinuousMeasure Quantity)
    : InventoryItem(Id, Name, Sku);