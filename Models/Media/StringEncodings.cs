using Models.Types.Media;

namespace Models.Media;

public static class StringEncodings
{
    public static StringEncodedFile EncodeBase64(this FileContent file) =>
        new(file.MimeType + ";base64," + Convert.ToBase64String(file.Content));
}