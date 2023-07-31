using Models.Media.Types;
using Models.Types.Components;
using Models.Types.Media;

namespace Models.Media;

public delegate FileContent BarcodeGenerator(StockKeepingUnit sku);

public delegate FileContent BarcodeGeneratorEx(
    BarcodeMargins margins, Code39Style style,
    StockKeepingUnit sku);

public static class BarcodeGeneratorExtensions
{
    public static BarcodeGenerator Apply(this BarcodeGeneratorEx f,
        BarcodeMargins margins, Code39Style style) =>
        sku => f(margins, style, sku);
}