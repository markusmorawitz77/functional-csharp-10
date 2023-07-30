using Application.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Products;
using Models.Types.Common;

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

    public IActionResult OnGet(Guid id) =>
        this.Specifications.TryFind(id)
            .Map(spec => 
            {
                this.Specification = spec;
                return (IActionResult)Page();
            })
            .Reduce(NotFound);
    
}
