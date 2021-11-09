using System.Diagnostics;

namespace aoc2019;

public static class Extensions
{
    public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> list)
    {
        if (list.Count() == 1) return new[] { list };
        return list.SelectMany(t => Permute(list.Where(x => !x!.Equals(t))), (v, p) => p.Prepend(v));
    }

    public static IEnumerable<string> Chunk(this string str, int chunkSize)
    {
        for (var i = 0; i < str.Length; i += chunkSize)
            yield return str.Substring(i, chunkSize);
    }

    public static string ToDelimitedString<T>(this IEnumerable<T> enumerable, string delimiter = "")
    {
        return string.Join(delimiter, enumerable);
    }

    public static IEnumerable<T> Repeat<T>(this IEnumerable<T> sequence, int? count = null)
    {
        while (count == null || count-- > 0)
            foreach (var item in sequence)
                yield return item;
    }

    /// <summary>
    ///     increased accuracy for stopwatch based on frequency.
    ///     <see
    ///         href="http://geekswithblogs.net/BlackRabbitCoder/archive/2012/01/12/c.net-little-pitfalls-stopwatch-ticks-are-not-timespan-ticks.aspx">
    ///         blog
    ///         details here
    ///     </see>
    /// </summary>
    /// <param name="stopwatch"></param>
    /// <returns></returns>
    public static double ScaleMilliseconds(this Stopwatch stopwatch)
    {
        return 1_000 * stopwatch.ElapsedTicks / (double)Stopwatch.Frequency;
    }
}
