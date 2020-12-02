using System.Linq;
using aoc2019.lib;

namespace aoc2019
{
    internal sealed class Day15 : Day
    {
        private readonly IntCodeVM vm;

        public Day15()
        {
            vm = new IntCodeVM(Input.First());
        }

        public override int DayNumber => 15;

        public override string Part1()
        {
            return "intcode solution";
        }

        public override string Part2()
        {
            return "";
        }
    }
}