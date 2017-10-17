using System;

namespace Influence.Common.Extensions
{
    public static class StringExtensions
    {
        public static string DefaultTo(this string str, string defaultValue)
        {
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return str;
        }

        public static Guid ToGuid(this string str)
        {
            Guid g;
            return Guid.TryParse(str, out g) ? g : Guid.Empty;
        }

        public static bool IsEmpty(this string str) => string.IsNullOrEmpty(str);
        public static bool NotEmpty(this string str) => !string.IsNullOrEmpty(str);
    }
}