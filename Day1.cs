using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc2019
{
    public class Day1 : Day
    {
        private static readonly IEnumerable<int> lines =
            File.ReadLines("input/day1.in").Select(line => int.Parse(line));

        private static int FuelCost(int weight) => weight / 3 - 2;

        public override void Part1()
        {
            Console.WriteLine(lines.Select(num => FuelCost(num)).Sum());
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

        public override void Part2()
        {
            Console.WriteLine(lines.Select(cost => FullCost(cost)).Sum());
        }
    }
}

