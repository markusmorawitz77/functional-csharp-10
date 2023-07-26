using Models.Types;
using Models.Types.Media;
using SkiaSharp;

namespace Models.Media;

public static class BarcodeGeneration
{
    public static FileContent ToCode39(this StockKeepingUnit sku, int barHeight)
    {
        float horizontalMargin = 5;
        float verticalMargin = 3;
        float padding = 2.0f;

        SKPaint[] bars = new[]
        {
            ThinBar, Space, ThinBar, ThickBar, ThickBar, ThinBar,
            ThickBar, ThinBar, ThinBar, Space, ThickBar, ThinBar,
            ThinBar, ThickBar, ThinBar, Space, ThickBar, ThinBar,
            ThinBar, Space, ThinBar, ThickBar, ThickBar, ThinBar,
        };

        int height = (int)Math.Ceiling(barHeight + verticalMargin * 2);
        int width = (int)Math.Ceiling(bars.Sum(bar => bar.StrokeWidth) + (bars.Length - 1) * padding + horizontalMargin * 2);

        SKBitmap bitmap = new(width, height);
        SKCanvas canvas = new(bitmap);
        canvas.Clear(SKColors.White);

        float x = horizontalMargin;
        foreach (SKPaint bar in bars)
        {
            float lineX = x + bar.StrokeWidth / 2;
            canvas.DrawLine(lineX, verticalMargin, lineX, barHeight + verticalMargin, bar);
            x += bar.StrokeWidth + padding;
        }

        return bitmap.ToPng();
    }

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
