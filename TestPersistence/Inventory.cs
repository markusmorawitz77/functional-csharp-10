using Application.Persistence;
using Models.Types.Common;
using Models.Types.Components;

namespace TestPersistence;

public class Inventory : IReadOnlyRepository<(Part part, DiscreteMeasure quantity)>
{
    private PartsReadRepository Parts { get; } = new();

    public IEnumerable<(Part part, DiscreteMeasure quantity)> TryFind(Guid id) =>
        this.Parts.TryFind(id).Where(Exists).Select(part => (part, QuantityFor(part)));

    private (Part part, DiscreteMeasure quantity) Find(Part part) =>
        (part, QuantityFor(part));

    private static DiscreteMeasure QuantityFor(Part part) =>
        new("Piece", Exists(part) ? 1U : 0);

    public IEnumerable<(Part part, DiscreteMeasure quantity)> GetAll() =>
        this.Parts.GetAll().Where(Exists).Select(part => (part, QuantityFor(part)));

    private static bool Exists(Part part) => part.Sku.Value[part.Sku.Value.Length / 2] % 5 == 2;
}
