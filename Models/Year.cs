namespace Models;

public record struct Year(int Number)
{
    public Month GetMonth(int ordinal) => 
        ordinal >= 1 && ordinal <= 12 ? new(this, ordinal)
        : throw new ArgumentOutOfRangeException(nameof(ordinal));

    
    public IEnumerable<Month> Months => this.GetMonths(this);
    
    private IEnumerable<Month> GetMonths(Year year) =>
        Enumerable.Range(1,12).Select(ordninal => new Month(year, ordninal));
}   