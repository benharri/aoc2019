using System.Linq;
using aoc2019.lib;

namespace aoc2019
{
    internal sealed class Day09 : Day
    {
        private readonly IntCodeVM vm;

        public Day09() : base(9, "Sensor Boost")
        {
            vm = new IntCodeVM(Input.First());
        }

        protected override string Part1()
        {
            vm.Reset();
            vm.Run(1);
            return $"{vm.output.ToDelimitedString(",")}";
        }

        protected override string Part2()
        {
            vm.Reset();
            vm.Run(2);
            return $"{vm.output.ToDelimitedString(",")}";
        }
    }
}