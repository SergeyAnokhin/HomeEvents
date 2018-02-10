using System.Text.RegularExpressions;

namespace Common
{
    public static class StringExtensions
    {
        public static string URL_PATTERN =
            @"(https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*))";

        public static string Crop(this string value, int size)
        {
            if (value.Length <= size) return value;

            return value.Substring(0, size) + $" ...({value.Length})";
        }

        public static string UrlToAFref(this string value)
        {
            return Regex.Replace(value, URL_PATTERN, "<a href='$1'>$1</a>");
        }
    }
}
