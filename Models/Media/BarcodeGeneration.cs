using Models.Types.Components;
using Models.Types.Media;
using Models.Common;
using SkiaSharp;

namespace Models.Media;

public static class BarcodeGeneration
{
    public static FileContent ToCode39(this StockKeepingUnit sku, int barHeight) =>
        sku.Value.ToCode39Bars().ToCode39Bitmap(barHeight).ToPng();
    
    private static SKBitmap ToCode39Bitmap(this IEnumerable<int> bars, int barHeight) =>
        bars.ToGraphicalLines().ToBarcodeBitmap(barHeight);
    
    private static SKPaint[] ToGraphicalLines(this IEnumerable<int> bars) =>
        bars.ToGraphicalLines(Gap, ThinBar, ThickBar);
    
    private static SKPaint[] ToGraphicalLines(this IEnumerable<int> bars, params SKPaint[] lines) =>
        bars.Select(bar => lines[bar]).ToArray();
    
    private static SKPaint ThickBar => Bar(SKColors.Black, 4.0f);
    private static SKPaint ThinBar => Bar(SKColors.Black, 1.5f);
    private static SKPaint Gap => Bar(SKColors.Transparent, 2.0f);
    
    private static SKPaint Bar(SKColor color, float thickness) => new SKPaint
    {
        Color = color,
        Style = SKPaintStyle.Stroke,
        StrokeCap = SKStrokeCap.Butt,
        StrokeWidth = thickness,
        IsAntialias = true,
    };
    
    private static SKBitmap ToBarcodeBitmap(this SKPaint[] bars, int barHeight)
    {
        float horizontalMargin = 5.0f;
        float verticalMargin = 3.0f;
        float padding = 2.0f;
        float barsWidth = bars.Sum(bar => bar.StrokeWidth);
        float height = barHeight + 2 * verticalMargin;
        float width = barsWidth + (bars.Length - 1) * padding + 2 * horizontalMargin;

        SKBitmap bitmap = InitializeBitmap(width, height);
        SKCanvas canvas = new(bitmap);

        float offset = horizontalMargin;
        foreach (SKPaint bar in bars)
        {
            float x = offset + bar.StrokeWidth / 2;
            canvas.DrawLine(x, verticalMargin, x, barHeight + verticalMargin, bar);
            offset += bar.StrokeWidth + padding;
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
