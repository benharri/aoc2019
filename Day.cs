using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace aoc2019
{
    public abstract class Day
    {
        public abstract int DayNumber { get; }
        public virtual IEnumerable<string> Input =>
            File.ReadLines($"input/day{DayNumber}.in");

        public virtual void AllParts()
        {
            Console.WriteLine($"Day {DayNumber}:");
            Console.WriteLine(Part1());
            Console.WriteLine(Part2());
        }

        public abstract string Part1();
        public abstract string Part2();
    }
}
