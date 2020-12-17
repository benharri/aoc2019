using System.Linq;
using aoc2019.lib;

namespace aoc2019
{
    public sealed class Day19 : Day
    {
        private readonly IntCodeVM vm;

        public Day19() : base(19, "Tractor Beam")
        {
            vm = new IntCodeVM(Input.First());
        }

        public override string Part1()
        {
            var count = 0;

            for (var x = 0; x < 50; x++)
                for (var y = 0; y < 50; y++)
                {
                    vm.AddInput(x, y);
                    vm.Run();
                    if (vm.Result == 1) count++;
                }

            return $"{count}";
        }

        public override string Part2()
        {
            return "";
        }
    }
}
