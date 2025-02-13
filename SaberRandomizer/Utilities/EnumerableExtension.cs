using System;
using System.Collections.Generic;

namespace SaberRandomizer.Utilities;

internal static class EnumerableExtension
{
    public static void ForEach<T>(this IEnumerable<T> seq, Action<T> action)
    {
        foreach (var item in seq) action(item);
    }
}