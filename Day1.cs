using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal sealed class Day1 : Day
    {
        private readonly IEnumerable<int> masses;

        public Day1()
        {
            masses = Input.Select(int.Parse);
        }

        public override int DayNumber => 1;

        private static int FuelCost(int weight)
        {
            return weight / 3 - 2;
        }

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

        protected override string Part1()
        {
            return $"{masses.Sum(FuelCost)}";
        }

        protected override string Part2()
        {
            return $"{masses.Sum(FullCost)}";
        }
    }
}