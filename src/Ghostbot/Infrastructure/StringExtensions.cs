using System;

namespace Ghostbot.Infrastructure
{
    public static class StringExtensions
    {
        public static T ToEnum<T>(this string platformValue) where T : struct
        {
            return (T)Enum.Parse(typeof(T), platformValue);
        }
    }
}