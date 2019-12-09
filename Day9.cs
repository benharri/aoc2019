using aoc2019.lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal class Day9 : Day
    {
        public override int DayNumber => 9;
        private readonly IntCodeVM vm;

        public Day9()
        {
            vm = new IntCodeVM(Input.First().Split(',').Select(long.Parse).ToList());
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
}
