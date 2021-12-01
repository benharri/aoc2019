namespace aoc2019;

public sealed class Day06 : Day
{
    private readonly Dictionary<string, string> input;

    public Day06() : base(6, "Universal Orbit Map")
    {
        input = Input.ToDictionary(i => i.Split(')')[1], i => i.Split(')')[0]);
    }

    private List<string> GetParents(string obj)
    {
        var res = new List<string>();
        for (var curr = obj; curr != "COM"; curr = input[curr])
            res.Add(curr);
        res.Add("COM");
        return res;
    }

    public override string Part1() =>
        $"{input.Keys.Sum(o => GetParents(o).Count - 1)}";

    public override string Part2()
    {
        var you = GetParents("YOU");
        var san = GetParents("SAN");
        var common = 1;
        for (; you[^common] == san[^common]; common++) ;
        return $"{you.Count + san.Count - common * 2}";
    }
}
