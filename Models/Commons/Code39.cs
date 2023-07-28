namespace Models.Commons;

public static class Code39 
{
    public static IEnumerable<int> ToCode39Bars(this string value) =>
        ("*" + value + "*").Encode();

    private static IEnumerable<int> Encode(this IEnumerable<char> values) =>
        values.SelectMany(Encode);

    private static int[] Encode(char value) => Code[value];

    private static IDictionary<char, int[]> Code { get; } = new Dictionary<char, int[]>()
    {
        {'1', new[]{2,1,0,1,1,2}}, {'2', new[]{1,2,0,1,1,2}}, {'3', new[]{2,2,0,1,1,1}}, {'4', new[]{1,1,0,2,1,2}}, {'5', new[]{2,1,0,2,1,1}},
        {'6', new[]{1,2,0,2,1,1}}, {'7', new[]{1,1,0,1,2,2}}, {'8', new[]{2,1,0,1,2,1}}, {'9', new[]{1,2,0,1,2,1}}, {'0', new[]{1,1,0,2,2,1}},
        {'A', new[]{2,1,1,0,1,2}}, {'B', new[]{1,2,1,0,1,2}}, {'C', new[]{2,2,1,0,1,1}}, {'D', new[]{1,1,2,0,1,2}}, {'E', new[]{2,1,2,0,1,1}},
        {'F', new[]{1,2,2,0,1,1}}, {'G', new[]{1,1,1,0,2,2}}, {'H', new[]{2,1,1,0,2,1}}, {'I', new[]{1,2,1,0,2,1}}, {'J', new[]{1,1,2,0,2,1}},
        {'K', new[]{2,1,1,1,0,2}}, {'L', new[]{1,2,1,1,0,2}}, {'M', new[]{2,2,1,1,0,1}}, {'N', new[]{1,1,2,1,0,2}}, {'O', new[]{2,1,2,1,0,1}},
        {'P', new[]{1,2,2,1,0,1}}, {'Q', new[]{1,1,1,2,0,2}}, {'R', new[]{2,1,1,2,0,1}}, {'S', new[]{1,2,1,2,0,1}}, {'T', new[]{1,1,2,2,0,1}},
        {'U', new[]{2,0,1,1,1,2}}, {'V', new[]{1,0,2,1,1,2}}, {'W', new[]{2,0,2,1,1,1}}, {'X', new[]{1,0,1,2,1,2}}, {'Y', new[]{2,0,1,2,1,1}},
        {'Z', new[]{1,0,2,2,1,1}}, {'-', new[]{1,0,1,1,2,2}}, {'.', new[]{2,0,1,1,2,1}}, {' ', new[]{1,0,2,1,2,1}}, {'*', new[]{1,0,1,2,2,1}},
        {'$', new[]{1,0,1,0,1,0,1,1}}, {'/', new[]{1,0,1,0,1,1,0,1}}, {'+', new[]{1,0,1,1,0,1,0,1}}, {'%', new[]{1,1,0,1,0,1,0,1}},
    };   
}