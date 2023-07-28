using Models.Types.Media;
using SkiaSharp;

namespace Models.Media;

internal static class ImageEncoding
{
    public static FileContent ToPng(this SKBitmap bitmap) =>
        new(bitmap.Encode(SKEncodedImageFormat.Png, 100).ToArray(), "image/png");
}
