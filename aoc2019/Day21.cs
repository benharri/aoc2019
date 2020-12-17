using System.Linq;
using aoc2019.lib;

namespace aoc2019
{
    public sealed class Day21 : Day
    {
        private readonly IntCodeVM vm;

        public Day21() : base(21, "Springdroid Adventure")
        {
            vm = new IntCodeVM(Input.First());
        }

        public override string Part1()
        {
            vm.Reset();
            var halt = vm.Run();
            return "";
        }

        public override string Part2()
        {
            return "";
        }
    }
}