using System;

namespace aoc2019
{
    public abstract class Day
    {
        public abstract int DayNumber { get; }
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
