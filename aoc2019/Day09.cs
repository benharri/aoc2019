namespace aoc2019;

public sealed class Day09 : Day
{
    private readonly IntCodeVM vm;

    public Day09() : base(9, "Sensor Boost")
    {
        vm = new IntCodeVM(Input.First());
    }

    public override string Part1()
    {
        vm.Reset();
        vm.Run(1);
        return $"{vm.output.ToDelimitedString(",")}";
    }

    public override string Part2()
    {
        vm.Reset();
        vm.Run(2);
        return $"{vm.output.ToDelimitedString(",")}";
    }
}
