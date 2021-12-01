namespace aoc2019;

public sealed class Day02 : Day
{
    private readonly IEnumerable<int> input;

    public Day02() : base(2, "1202 Program Alarm")
    {
        input = Input.First().Split(',').Select(int.Parse);
    }

    private int RunIntCode(int noun, int verb)
    {
        var v = input.ToList();
        v[1] = noun;
        v[2] = verb;

        for (var i = 0; v[i] != 99; i += 4)
            v[v[i + 3]] = v[i] switch
            {
                1 => v[v[i + 1]] + v[v[i + 2]],
                2 => v[v[i + 1]] * v[v[i + 2]],
                _ => throw new ArgumentOutOfRangeException(nameof(verb))
            };

        return v[0];
    }

    public override string Part1()
    {
        return $"{RunIntCode(12, 2)}";
    }

    public override string Part2()
    {
        for (var i = 0; i < 100; i++)
            for (var j = 0; j < 100; j++)
                if (RunIntCode(i, j) == 19690720)
                    return $"{100 * i + j}";

        return string.Empty;
    }
}
