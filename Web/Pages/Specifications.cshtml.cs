using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Components;
using Models.Types.Common;
using Models.Types.Products;

namespace Web.Pages;

public class SpecificationsModel : PageModel
{
    IReadOnlyRepository<AssemblySpecification> Specifications { get; }
    IReadOnlyRepository<(Part part, DiscreteMeasure quantity)> Inventory { get; }

    public SpecificationsModel(
        IReadOnlyRepository<AssemblySpecification> parts,
        IReadOnlyRepository<(Part part, DiscreteMeasure quantity)> inventory)
    {
        this.Specifications = parts;
        this.Inventory = inventory;
    }

    public IEnumerable<AssemblySpecification> AllProducts { get; set; }
        = Enumerable.Empty<AssemblySpecification>();
    public bool IsShowingAllSpecs { get; set; }

    public void OnGet(string show)
    {
        this.IsShowingAllSpecs = show == "all";
        Func<AssemblySpecification, bool> isSupported = this.IsSupportedStrategy(show);
        this.AllProducts = this.Specifications.GetAll().Where(isSupported);
    }

    private Func<AssemblySpecification, bool> IsSupportedStrategy(string show) =>
        show == "all" ? this.AllSupported : this.IsSupportedSupply;

    private bool AllSupported(AssemblySpecification spec) => true;

    private bool IsSupportedSupply(AssemblySpecification spec) =>
        spec.Components.All(component => this.InSupply(component.part, component.quantity));

    private bool InSupply(Part part, DiscreteMeasure required) =>
        this.Inventory.GetAll()
            .Any(item =>
                item.part.Id == part.Id &&
                item.quantity.Unit == required.Unit &&
                item.quantity.Value >= required.Value);
}
