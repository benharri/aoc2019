namespace aoc2019;

internal static class Util
{
    public static long Lcm(long a, long b) => a * b / Gcd(a, b);

    private static long Gcd(long a, long b)
    {
        while (true)
        {
            if (b == 0) return a;
            var a1 = a;
            a = b;
            b = a1 % b;
        }
    }
}