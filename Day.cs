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

        public virtual void AllParts(bool verbose = false)
        {
            Console.WriteLine($"Day {DayNumber}:");
            var s = new Stopwatch();
            s.Start();
            var part1 = Part1();
            s.Stop();
            if (verbose) Console.WriteLine($"part 1 elapsed ticks: {s.ElapsedTicks}");
            Console.WriteLine(part1);

            s.Reset();
            s.Start();
            var part2 = Part2();
            s.Stop();
            if (verbose) Console.WriteLine($"part 2 elapsed ticks: {s.ElapsedTicks}");
            Console.WriteLine(part2);
            Console.WriteLine();
        }

        public abstract string Part1();
        public abstract string Part2();
    }
}