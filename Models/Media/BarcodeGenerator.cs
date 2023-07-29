using Models.Types.Components;
using Models.Types.Media;
using Models.Media.Types;

namespace Models.Media;

public delegate FileContent BarcodeGenerator(StockKeepingUnit sku);

public delegate FileContent BarcodeGeneratorEx(
    Margins margins, Style style, StockKeepingUnit sku);

public static class BarcodeGeneratorExtensions
{
    public static BarcodeGenerator Apply(this BarcodeGeneratorEx f,
        Margins margins, Style style) =>
        sku => f(margins, style, sku);
}