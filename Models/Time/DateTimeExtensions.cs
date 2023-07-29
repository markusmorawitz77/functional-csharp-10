namespace Models.Time;

using Models.Types.Time;

public static class DateTimeExtensions
{
    public static Year ToYear(this DateTime time) => new(time.Year);
}
