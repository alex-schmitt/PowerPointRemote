using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace PowerPointRemote.WebApi.Extensions
{
    public static class StringExtensions
    {
        // Source: https://www.30secondsofcode.org/c-sharp/s/to-camel-case
        public static string ToCamelCase(this string str)
        {
            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            return new string(
                new CultureInfo("en-US", false)
                    .TextInfo
                    .ToTitleCase(
                        string.Join(" ", pattern.Matches(str)).ToLower()
                    )
                    .Replace(@" ", "")
                    .Select((x, i) => i == 0 ? char.ToLower(x) : x)
                    .ToArray()
            );
        }
    }
}