using Application.Persistence;
using Models.Types.Common;
using Models.Types.Components;

namespace TestPersistence;

public class Inventory : IReadOnlyRepository<(Part part, DiscreteMeasure quantity)>
{
    private PartsReadRepository Parts { get; } = new();
    private Random Random { get; } = new(42);

    public Option<(Part part, DiscreteMeasure quantity)> TryFind(Guid id) =>
        this.Parts.TryFind(id).Map(part => (part, QuantityFor(part)));

    private DiscreteMeasure QuantityFor(Part part) =>
        new("Piece", (uint)this.Random.Next(9, 17));

    public IEnumerable<(Part part, DiscreteMeasure quantity)> GetAll() =>
        this.Parts.GetAll().Select(part => (part, QuantityFor(part)));
}
