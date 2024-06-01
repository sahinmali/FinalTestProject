using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class StringListConverter : ValueConverter<List<string>, string>
{
    public StringListConverter() : base(
        v => string.Join(',', v), // Convert List<string> to comma-separated string
        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()) // Convert comma-separated string to List<string>
    {
    }
}
