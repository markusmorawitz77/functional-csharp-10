using Application.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Types.Components;
using Models.Types.Media;
using Models.Media;
using Models.Media.Types;
using Web.Configuration;
using Microsoft.AspNetCore.Mvc;

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

    public IActionResult OnGet(Guid id)  =>
        this.Parts.TryFind(id)
            .Select(part => {
                this.Part = part;
                this.BarcodeImage = this.GenerateBarcode(part.Sku);
                return (IActionResult)Page();
            })
            .SingleOrDefault(NotFound());

    private BarcodeGenerator GenerateBarcode { get; }
}
