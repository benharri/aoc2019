using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc2019
{
    public class Day1 : Day
    {
        public override int DayNumber => 1;

        private static readonly IEnumerable<int> masses =
            File.ReadLines("input/day1.in").Select(int.Parse);

        private static int FuelCost(int weight) => weight / 3 - 2;
        private static int FullCost(int cost)
        {
            int total = 0, newcost, tmp = cost;

            while ((newcost = FuelCost(tmp)) >= 0)
            {
                total += newcost;
                tmp = newcost;
            }

            return total;
        }

        public override string Part1() => $"{masses.Select(FuelCost).Sum()}";

        public override string Part2() => $"{masses.Select(FullCost).Sum()}";
    }
}
