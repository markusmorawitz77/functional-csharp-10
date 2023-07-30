using Application.Persistence;
using Models.Types.Common;
using Models.Types.Components;

namespace TestPersistence;

public class Inventory : IReadOnlyRepository<(Part part, DiscreteMeasure quantity)>
{
    private PartsReadRepository Parts { get; } = new();
    private Random Random { get; } = new(42);

    public Option<(Part part, DiscreteMeasure quantity)> TryFind(Guid id) =>
        this.Parts.TryFind(id).Filter(Exists).Map(part => (part, QuantityFor(part)));

    private DiscreteMeasure QuantityFor(Part part) =>
        new("Piece", Exists(part) ? (uint)this.Random.Next(5, 15) : 0);

    public IEnumerable<(Part part, DiscreteMeasure quantity)> GetAll() =>
        this.Parts.GetAll().Where(Exists).Select(part => (part, QuantityFor(part)));

    private static bool Exists(Part part) => part.Sku.Value[part.Sku.Value.Length / 2] % 5 > 1;
}
