using Models.Media.Types;

namespace Web.Configuration;

public class BarcodeFormat
{
    public string Name { get; set; } = string.Empty;
    public float HorizontalMargin { get; set; } = 0;
    public float VerticalMargin { get; set; } = 0;
    public float BarHeight { get; set; } = 0;
    public float ThinBarWidth { get; set; } = 0;
    public float ThickBarWidth { get; set; } = 0;
    public float GapWidth { get; set; } = 0;
    public float Padding { get; set; } = 0;
    public bool Antialias { get; set; } = true;

    public BarcodeMargins Margins => new(
        Horizontal: this.HorizontalMargin,
        Vertical: this.VerticalMargin,
        BarHeight: this.BarHeight);

    public Code39Style Style => new(
        ThinBarWidth: this.ThinBarWidth,
        ThickBarWidth: this.ThickBarWidth,
        GapWidth: this.GapWidth,
        Padding: this.Padding,
        Antialias: this.Antialias);
}
