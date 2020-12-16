using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal sealed class Day01 : Day
    {
        private readonly IEnumerable<int> masses;

        public Day01() : base(1, "The Tyranny of the Rocket Equation")
        {
            masses = Input.Select(int.Parse);
        }

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