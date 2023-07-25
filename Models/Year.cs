namespace Models;

public record struct Year(int Number)
{
    public Month GetMonth(int ordinal) => 
        ordinal >= 1 && ordinal <= 12 ? new(this, ordinal)
        : throw new ArgumentOutOfRangeException(nameof(ordinal));
}