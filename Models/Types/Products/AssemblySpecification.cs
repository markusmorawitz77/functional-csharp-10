using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Models.Types.Common;
using Models.Types.Components;

namespace Models.Types.Products;

public class AssemblySpecification
{
    public AssemblySpecification(Guid id) =>
        (Id, InstructionsList) = (id, ImmutableList<AssemblyInstruction>.Empty);

    [SetsRequiredMembers]    
    public AssemblySpecification(AssemblySpecification other) =>
        (Id, Name, Description, InstructionsList) =
        (other.Id, other.Name, other.Description, other.InstructionsList);

    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }

    private ImmutableList<AssemblyInstruction> InstructionsList { get; init; }
    public IEnumerable<AssemblyInstruction> Instructions => this.InstructionsList;

    public AssemblySpecification Add(params AssemblyInstruction[] instructions) =>
        new(this) { InstructionsList = this.InstructionsList.AddRange(instructions) };

    public IEnumerable<(Part part, DiscreteMeasure quantity)> Components =>
        this.InstructionsList
            .SelectMany(instruction => instruction.Components)
            .GroupBy(row => (part: row.part, unit: row.quantity.Unit), row => row.quantity)
            .Select(group => (group.Key.part, group.Sum()));

    public IEnumerable<(InventoryItem item, Measure quantity)> Consumables { get; init; }
        = Enumerable.Empty<(InventoryItem, Measure)>();
}