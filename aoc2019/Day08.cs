namespace aoc2019;

public sealed class Day08 : Day
{
    private readonly List<List<char>> photo;

    public Day08() : base(8, "Space Image Format")
    {
        photo = Input.First().Chunk(25 * 6).Select(s => s.ToList()).ToList();
    }

    public override string Part1()
    {
        var l = photo.OrderBy(layer => layer.Count(pixel => pixel == '0')).First();
        return $"{l.Count(p => p == '1') * l.Count(p => p == '2')}";
    }

    public override string Part2()
    {
        return "\n" + Enumerable.Range(0, 25 * 6)
            .Select(p => Enumerable.Range(0, photo.Count)
                .Select(l => photo[l][p])
                .Aggregate('2', (acc, next) =>
                    acc != '2' ? acc : next == '0' ? ' ' : next
                )
            )
            .ToDelimitedString()
            .Chunk(25)
            .ToDelimitedString("\n")
            .Replace('1', 'x');
    }
}
