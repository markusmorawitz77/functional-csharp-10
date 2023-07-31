using Application.Persistence;
using Models.Types.Common;
using Models.Types.Components;
using Models.Types.Products;

namespace TestPersistence;

public class SpecsRepository : IReadOnlyRepository<AssemblySpecification>
{
    private IEnumerable<AssemblySpecification> Specs { get; }
    private Random Random { get; } = new(42);
    private PartsReadRepository PartsRepository { get; }
    private Part[] Parts { get; }
    private const int SpecsCount = 15;

    public SpecsRepository()
    {
        this.PartsRepository = new();
        this.Parts = this.PartsRepository.GetAll().ToArray();
        this.Specs = Enumerable.Range(1, SpecsCount).Select(this.NextSpecification).ToArray();
    }

    private AssemblySpecification NextSpecification(int ordinal) =>
        new AssemblySpecification(Ids[ordinal - 1])
        {
            Name = $"Traffic light DIY project #{ordinal}",
            Description = $"Specification of parts for the home project #{ordinal}",
            Consumables = Enumerable.Empty<(InventoryItem, Measure)>()
        }.Add(Instructions);

    private DiscreteMeasure OnePiece { get; } = new("Piece", 1);
    private DiscreteMeasure TwoPieces { get; } = new("Piece", 2);
    private DiscreteMeasure ThreePieces { get; } = new("Piece", 3);

    private AssemblyInstruction[] Instructions => new[]
    {
        new AssemblyInstruction().Append(
            new TextSegment("Take all components as shown in the list")),
        new AssemblyInstruction().Append(
            new TextSegment("Connect"), new NewPartSegment(Parts[4], TwoPieces),
            new TextSegment("to"), new NewPartSegment(Parts[0], OnePiece)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect"), new NewPartSegment(Parts[5], TwoPieces),
            new TextSegment("to"), new NewPartSegment(Parts[0], OnePiece)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect"), new NewPartSegment(Parts[6], TwoPieces),
            new TextSegment("to"), new NewPartSegment(Parts[0], OnePiece)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect emitters of"),
            new PartReferenceSegment(Parts[0], ThreePieces)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect"), new NewPartSegment(Parts[1], ThreePieces)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect"), new NewPartSegment(Parts[2], TwoPieces)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect"), new NewPartSegment(Parts[3], OnePiece)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect out wires of"),
            new PartReferenceSegment(Parts[1], ThreePieces),
            new PartReferenceSegment(Parts[2], TwoPieces),
            new PartReferenceSegment(Parts[3], OnePiece)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect"), new NewPartSegment(Parts[8], OnePiece)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect"), new NewPartSegment(Parts[7], TwoPieces)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect +ve pin of 2nd"),
            new PartReferenceSegment(Parts[7], OnePiece)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect"), new NewPartSegment(Parts[10], OnePiece)),
        new AssemblyInstruction().Append(
            new TextSegment("Connect"), new NewPartSegment(Parts[9], OnePiece)),
    };

    public IEnumerable<AssemblySpecification> GetAll() => this.Specs;

    public Option<AssemblySpecification> TryFind(Guid id) =>
        this.Specs.Where(s => s.Id == id)
            .Select(spec => spec.Optional()).SingleOrDefault(None.Value);

    private static Guid[] Ids { get; } =
        Enumerable.Range(0, 1000).Select(_ => Guid.NewGuid()).ToArray();
}

