using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Components;
using Models.Types.Common;
using Models.Types.Products;
using Microsoft.AspNetCore.Http.Features;
using System.Data;
using System.ComponentModel;

namespace Web.Pages;

public class IndexModel : PageModel
{
    IReadOnlyRepository<AssemblySpecification> Specifications { get; }
    IReadOnlyRepository<(Part part, DiscreteMeasure quantity)> Inventory { get; }

    public IndexModel(
        IReadOnlyRepository<AssemblySpecification> parts,
        IReadOnlyRepository<(Part part, DiscreteMeasure quantity)> inventory)
    {
        this.Specifications = parts;
        this.Inventory = inventory;
    }

    public IEnumerable<AssemblySpecification> AllProducts { get; set; }
        = Enumerable.Empty<AssemblySpecification>();

    public void OnGet()
    {
        this.AllProducts = this.Specifications.GetAll().Where(this.IsSupported);
    }

    private bool IsSupported(AssemblySpecification spec) =>
        spec.Components.All(component => this.IsSupported(component.part, component.quantity));

    private bool IsSupported(Part part, DiscreteMeasure required) =>
        this.Inventory.GetAll()
            .Any(item => 
                item.part.Id == part.Id && 
                item.quantity.Unit == required.Unit && 
                item.quantity.Value >= required.Value);

}
