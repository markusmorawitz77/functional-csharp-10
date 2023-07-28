using Models.Types;
using Models.Types.Media;
using Models.Commons;
using SkiaSharp;
using System.Reflection.Emit;

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

    private static SKBitmap ToBarcodeBitmap(this SKPaint[] bars, int barHeight) => new(10,10);  

}


