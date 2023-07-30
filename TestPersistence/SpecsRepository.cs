using Application.Persistence;
using Models.Types.Common;
using Models.Types.Components;
using Models.Types.Products;

namespace TestPersistence;

public class SpecsRepository : IReadOnlyRepository<AssemblySpecification>
{
    private IEnumerable<AssemblySpecification> Specs { get; }
    private Random Random { get; } = new(42);
    private PartsReadRepository Parts { get; } = new();
    private const int SpecsCount = 15;

    public SpecsRepository()
    {
        this.Specs = Enumerable.Range(1, SpecsCount).Select(this.NextSpecification).ToArray();
    }

    private AssemblySpecification NextSpecification(int ordinal) =>
        new()
        {
            Id = Ids[ordinal - 1],
            Name = $"Traffic light DIY project #{ordinal}",
            Description = $"Specification of parts for the home project #{ordinal}",
            Components = this.NextComponents(),
            Consumables = Enumerable.Empty<(InventoryItem, Measure)>()
        };

    private IEnumerable<(Part Part, DiscreteMeasure Quantity)> NextComponents() =>
        this.Parts.GetAll()
            .Where(_ => this.Random.Next(0, 2) == 0)
            .Select(part => (part, new DiscreteMeasure("Piece", (uint)this.Random.Next(2, 10))));

    public IEnumerable<AssemblySpecification> GetAll() => this.Specs;

    public Option<AssemblySpecification> TryFind(Guid id) =>
        this.Specs.Where(s => s.Id == id)
            .Optional();

    private static Guid[] Ids { get; } =
        Enumerable.Range(0, 1000).Select(_ => Guid.NewGuid()).ToArray();
}