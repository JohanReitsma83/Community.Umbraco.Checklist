using System.Text.RegularExpressions;

namespace Community.Umbraco.Checklist.Core.Extensions;
public static class StringExtensions
{
    public static string ToKebabCase(this string str)
    {
        str = Regex.Replace(str ?? string.Empty, "[\\W_]+", " ").Trim();
        str = Regex.Replace(str, "[ ]{2,}", " ");
        return Regex.Replace(str, "(\\p{Ll})(\\p{Lu})", "$1-$2").Replace(" ", "-").Replace("--", "-")
            .ToLower();
    }
}