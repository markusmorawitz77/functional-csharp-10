using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Products;

namespace Web.Pages;

public class SpecDetailsModel : PageModel
{
    IReadOnlyRepository<AssemblySpecification> Specifications { get; }

    public SpecDetailsModel(
        IReadOnlyRepository<AssemblySpecification> specifications)
    {
        this.Specifications = specifications;
    }

    public AssemblySpecification Specification { get; set; } = null!;

    public void OnGet(Guid id)
    {
        this.Specification = this.Specifications.TryFind(id).First();
    }
}
