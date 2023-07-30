using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Components;
using Models.Types.Media;
using Models.Media;
using Models.Media.Types;
using Web.Configuration;

namespace Web.Pages;

public class PartDetailsModel : PageModel
{
    IReadOnlyRepository<Part> Parts { get; }

    public PartDetailsModel(
        IReadOnlyRepository<Part> parts, BarcodeGeneratorFactory barcodeGenerators)
    {
        this.Parts = parts;
        this.GenerateBarcode = barcodeGenerators.Print;
    }

    public Part Part { get; set; } = null!;
    public FileContent BarcodeImage { get; set; } = null!;

    public void OnGet(Guid id)
    {
        this.Part = this.Parts.TryFind(id).First();
        this.BarcodeImage = this.GenerateBarcode(this.Part.Sku);
    }

    private BarcodeGenerator GenerateBarcode { get; }
}
