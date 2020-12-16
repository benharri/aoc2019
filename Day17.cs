using System;
using System.Linq;
using System.Text;
using aoc2019.lib;

namespace aoc2019
{
    internal sealed class Day17 : Day
    {
        private const bool Verbose = false;

        private readonly IntCodeVM vm;

        public Day17() : base(17, "Set and Forget")
        {
            vm = new IntCodeVM(Input.First());
        }

        protected override string Part1()
        {
            vm.Reset();
            vm.Run();
            var sb = new StringBuilder();
            while (vm.output.Any())
                sb.Append((char) vm.Result);
            if (Verbose) Console.Write(sb);
            var grid = sb.ToString().Trim().Split().Select(s => s.ToCharArray()).ToArray();

            var sum = 0;
            for (var y = 1; y < grid.Length - 1; y++)
            for (var x = 1; x < grid[y].Length - 1; x++)
                if (grid[y][x] == '#' &&
                    grid[y - 1][x] == '#' &&
                    grid[y + 1][x] == '#' &&
                    grid[y][x - 1] == '#' &&
                    grid[y][x + 1] == '#')
                    sum += x * y;

            return $"{sum}";
        }

        protected override string Part2()
        {
            //vm.Reset();
            //vm.memory[0] = 2;
            //var halt = IntCodeVM.HaltType.Waiting;
            //while (halt == IntCodeVM.HaltType.Waiting)
            //{
            //    halt = vm.Run();
            //}
            return "";
        }
    }
}