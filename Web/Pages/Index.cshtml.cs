using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Components;
using Models.Types.Common;
using Models.Types.Products;

namespace Web.Pages;

public class IndexModel : PageModel
{
    IReadOnlyRepository<AssemblySpecification> Specifications { get; }

    public IndexModel(
        IReadOnlyRepository<AssemblySpecification> parts)
    {
        this.Specifications = parts;
    }

    public IEnumerable<AssemblySpecification> AllProducts { get; set; }
        = Enumerable.Empty<AssemblySpecification>();

    public void OnGet()
    {
        this.AllProducts = this.Specifications.GetAll();
    }
}
