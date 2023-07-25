namespace Models;

public record struct Year(int Number)
{
    public Month GetMonth(int ordinal) => 
        ordinal >= 1 && ordinal <= 12 ? new(this, ordinal)
        : throw new ArgumentOutOfRangeException(nameof(ordinal));
    
    public IEnumerable<Month> TryGetMonth(int ordinal)
    {
        if (ordinal >= 1 && ordinal <= 12) yield return new(this, ordinal);
    }

    public IEnumerable<Month> Months => this.GetMonths(this);

    public Year DecadeBeginning => new(this.Number / 10 * 10 + 1);

    public IEnumerable<Year> Decade =>
        Enumerable.Range(this.DecadeBeginning.Number, 10)
            .Select(number => new Year(number));
    
    private IEnumerable<Month> GetMonths(Year year) =>
        Enumerable.Range(1,12).Select(ordninal => new Month(year, ordninal));
}   