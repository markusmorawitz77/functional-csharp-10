using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Persistence;
using Models.Types;

namespace Web.Pages;

public class IndexModel : PageModel
{
    IReadOnlyRepository<Part> Parts { get; }

    public IndexModel(IReadOnlyRepository<Part> parts)
    {
        this.Parts = parts;
    }

    public IEnumerable<Part> AllParts { get; set; } = Enumerable.Empty<Part>();

    public void OnGet()
    {
        this.AllParts = this.Parts.GetAll().ToList();
    }
}
