using System.Linq;
namespace Models;


public static class DateTimeExtensions
{
    public static Year ToYear(this DateTime time) => new(time.Year);

    public static IEnumerable<Month> GetDecadeMonths(this DateTime time) =>
        Enumerable.Range(time.Year.ToDecadeBeginning(), 10)
            .Select(year => new Year(year))
            .SelectMany(year => year.Months);

    private static int ToDecadeBeginning(this int year) => year / 10 * 10 + 1;

}
