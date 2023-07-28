using Models.Types;
using Models.Types.Media;
using SkiaSharp;

namespace Models.Media;

public static class BarcodeGeneration
{
    public static FileContent ToCode39(this StockKeepingUnit sku, int barHeight) =>
        sku.Value.ToCode39Bars().ToCode39Bitmap(barHeight).ToPng();

    private static IEnumerable<int> ToCode39Bars(this string value) => Enumerable.Empty<int>();

    private static SKBitmap ToCode39Bitmap(this IEnumerable<int> bars, int barHeight) => new();

    private static FileContent ToPng(this SKBitmap bitmap) =>
        new(bitmap.Encode(SKEncodedImageFormat.Png, 100).ToArray(), "image/png");

    private static SKPaint ThickBar { get; } = new()
    {
        Color = SKColors.Black,
        Style = SKPaintStyle.Stroke,
        StrokeCap = SKStrokeCap.Butt,
        StrokeWidth = 4.0f,
        IsAntialias = true,
    };

    private static SKPaint ThinBar { get; } = new()
    {
        Color = SKColors.Black,
        Style = SKPaintStyle.Stroke,
        StrokeCap = SKStrokeCap.Butt,
        StrokeWidth = 1.5f,
        IsAntialias = true,
    };

    private static SKPaint Space { get; } = new()
    {
        Color = SKColors.Transparent,
        Style = SKPaintStyle.Stroke,
        StrokeCap = SKStrokeCap.Butt,
        StrokeWidth = 2.0f,
        IsAntialias = true,
    };
}
