using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Components;
using Models.Types.Media;
using Models.Media;

namespace Web.Pages;

public class PartDetailsModel : PageModel
{
    IReadOnlyRepository<Part> Parts { get; }

    public PartDetailsModel(IReadOnlyRepository<Part> parts)
    {
        this.Parts = parts;
    }

    public Part Part { get; set; } = null!;
    public FileContent BarcodeImage { get; set; } = null!;

    public void OnGet(Guid id)
    {
        this.Part = this.Parts.Find(id);
        this.BarcodeImage = this.Part.Sku.ToCode39(this.Margins, this.Style);
    }

    private BarcodeGeneration.Margins Margins => new(
        Horizontal: 20, Vertical: 10, BarHeight: 200);
    
    private BarcodeGeneration.Style Style => new(
        ThinBarWidth: 4, ThickBarWidth: 10, GapWidth: 6, Padding: 6, Antialias: false);
}
