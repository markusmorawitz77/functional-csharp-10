using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Components;
using Models.Types.Media;
using Models.Media.Types;

namespace Web.Pages;

public class IndexModel : PageModel
{
    IReadOnlyRepository<Part> Parts { get; }

    public IndexModel(IReadOnlyRepository<Part> parts)
    {
        this.Parts = parts;
    }

    public IEnumerable<Part> AllParts { get; set; } = Enumerable.Empty<Part>();

    public BarcodeMargins Margins { get; } = new (
        Horizontal: 5, Vertical: 3, BarHeight: 25);
    
    public Code39Style Style { get; } = new (
        ThinBarWidth: 1.5f, ThickBarWidth: 4, GapWidth: 2, Padding:2, Antialias: true);

    public void OnGet()
    {
        this.AllParts = this.Parts.GetAll().ToList();
    }
}
