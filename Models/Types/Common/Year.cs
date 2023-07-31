namespace Models.Types.Common;

public record Year(int Number)
{
    public Month GetMonth(int ordinal) =>
        ordinal >= 1 && ordinal <= 12 ? new(this, ordinal)
        : throw new ArgumentException(nameof(ordinal));
    
    public IEnumerable<Month> TryGetMonth(int ordinal)
    {
        if (ordinal >= 1 && ordinal <= 12) yield return new(this, ordinal);
    }
    
    public IEnumerable<Month> Months =>
        Enumerable.Range(1, 12).Select(ordinal => new Month(this, ordinal));
    
    public Year DecadeBeginning => new(this.Number / 10 * 10 + 1);

    public IEnumerable<Year> Decade =>
        Enumerable.Range(this.DecadeBeginning.Number, 10)
            .Select(number => new Year(number));
}
