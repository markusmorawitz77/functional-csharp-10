using Models.Types.Components;
using Models.Types.Media;
using Models.Common;
using SkiaSharp;
using Models.Media.Types;

namespace Models.Media;


public static class Code39Generator
{
    public static BarcodeGeneratorEx ToCode39 => (margins, style, sku) =>
        sku.Value.ToCode39Bars().ToCode39Bitmap(margins, style).ToPng();
    
    private static SKBitmap ToCode39Bitmap(
        this IEnumerable<int> bars, BarcodeMargins margins, Code39Style style) =>
        bars.ToGraphicalLines(style).ToBarcodeBitmap(margins, style);
    
    private static SKPaint[] ToGraphicalLines(
        this IEnumerable<int> bars, Code39Style style) =>
        bars.ToGraphicalLines(Gap(style), ThinBar(style), ThickBar(style));
    
    private static SKPaint[] ToGraphicalLines(
        this IEnumerable<int> bars, params SKPaint[] lines) =>
        bars.Select(bar => lines[bar]).ToArray();
    
    private static SKPaint ThickBar(Code39Style style) =>
        Bar(SKColors.Black, style.ThickBarWidth, style.Antialias);

    private static SKPaint ThinBar(Code39Style style) =>
        Bar(SKColors.Black, style.ThinBarWidth, style.Antialias);

    private static SKPaint Gap(Code39Style style) =>
        Bar(SKColors.Transparent, style.GapWidth, style.Antialias);
    
    private static SKPaint Bar(SKColor color, float thickness, bool antialias) => new SKPaint
    {
        Color = color,
        Style = SKPaintStyle.Stroke,
        StrokeCap = SKStrokeCap.Butt,
        StrokeWidth = thickness,
        IsAntialias = antialias,
    };
    
    private static SKBitmap ToBarcodeBitmap(
        this SKPaint[] bars, BarcodeMargins margins, Code39Style style)
    {
        float barsWidth = bars.Sum(bar => bar.StrokeWidth);
        float height = margins.BarHeight + 2 * margins.Vertical;
        float width = barsWidth + (bars.Length - 1) * style.Padding + 2 * margins.Horizontal;

        SKBitmap bitmap = InitializeBitmap(width, height);
        SKCanvas canvas = new(bitmap);

        float offset = margins.Horizontal;
        foreach (SKPaint bar in bars)
        {
            float x = offset + bar.StrokeWidth / 2;
            canvas.DrawLine(x, margins.Vertical, x, margins.BarHeight + margins.Vertical, bar);
            offset += bar.StrokeWidth + style.Padding;
        }

        return bitmap;
    }

    private static SKBitmap InitializeBitmap(float width, float height)
    {
        SKBitmap bitmap = new((int)Math.Ceiling(width), (int)Math.Ceiling(height));
        SKCanvas canvas = new(bitmap);
        canvas.Clear(SKColors.White);
        return bitmap;
    }
}
