using System;
using System.Collections.Generic;
using System.Text;

namespace Hybrid.GeoLocation.DataAccess.Extensions
{
    public static class StringExtensions
    {
        public static string WrapWithQuotes(this string str) => $"\"{str}\"";

        public static bool IsNotNull(this string str) => !string.IsNullOrEmpty(str);

        public static bool IsNull(this string str) => string.IsNullOrEmpty(str);
    }
}
