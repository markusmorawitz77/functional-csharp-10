using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Components;
using Models.Types.Common;
using Models.Types.Products;
using Microsoft.AspNetCore.Http.Features;
using System.Data;

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
        spec.Components
            .Join(this.Inventory.GetAll(),
                component => component.part.Id, item => item.part.Id,
                (c,i) => (part: c.part, requested: c.quantity, available: i.quantity))
            .Where(row => row.requested.Unit == row.available.Unit)
            .All(row => row.requested.Value <= row.available.Value);

}
