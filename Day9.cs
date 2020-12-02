using System.Linq;
using aoc2019.lib;

namespace aoc2019
{
    internal sealed class Day9 : Day
    {
        private readonly IntCodeVM vm;

        public Day9()
        {
            vm = new IntCodeVM(Input.First());
        }

        public override int DayNumber => 9;

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
}