using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Components;

namespace Web.Pages;

public class PartDetailsModel : PageModel
{
    IReadOnlyRepository<Part> Parts { get; }

    public PartDetailsModel(IReadOnlyRepository<Part> parts)
    {
        this.Parts = parts;
    }

    public Part Part { get; set; } = null!;

    public void OnGet(Guid id)
    {
        this.Part = this.Parts.Find(id);
    }
}
