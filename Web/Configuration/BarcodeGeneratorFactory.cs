using Microsoft.Extensions.Options;
using Models.Media;
using Models.Media.Types;

namespace Web.Configuration;

public class BarcodeGeneratorFactory
{
    private IDictionary<string, BarcodeGenerator> Generators { get; }

    public BarcodeGeneratorFactory(IOptions<BarcodeFormatOptions> options)
    {
        this.Generators = options.Value.Formats.ToDictionary(
            format => format.Name,
            format => CreateBarcodeGenerator(format));
    }

    public BarcodeGenerator Inline => this.GetGenerator(BarcodeFormatOptions.Inline);
    public BarcodeGenerator Print => this.GetGenerator(BarcodeFormatOptions.Print);

    public BarcodeGenerator this[string name] => this.GetGenerator(name);

    private static BarcodeGenerator CreateBarcodeGenerator(BarcodeFormat format) =>
        Code39Generator.ToCode39.Apply(format.Margins, format.Style);

    private static BarcodeMargins DefaultMargins { get; } = new(0, 0, 20);
    private static Code39Style DefaultStyle { get; } = new(1, 3, 1, 1, true);

    private static BarcodeGenerator DefaultBarcodeGenerator { get; } = 
        Code39Generator.ToCode39.Apply(DefaultMargins, DefaultStyle);

    private BarcodeGenerator GetGenerator(string name) =>
        this.Generators.TryGetValue(name, out var generator) ? generator
        : DefaultBarcodeGenerator;
}