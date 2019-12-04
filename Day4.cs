using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc2019
{
    internal class Day4 : Day
    {
        int start, end;

        public Day4()
        {
            var range = File.ReadLines("input/day4.in")
                .First()
                .Split('-')
                .Select(i => int.Parse(i))
                .ToList();

            start = range[0]; end = range[1];
        }

        private bool IsValid(int i)
        {
            int prev = 0; bool hasDup = false;
            foreach (var c in i.ToString())
            {
                int curr = c - '0';
                if (curr < prev) return false;
                if (curr == prev) hasDup = true;
                prev = curr;
            }
            
            return i >= start && i <= end && hasDup;
        }

        public override void Part1()
        {
            Console.WriteLine(Enumerable.Range(start, end).Count(i => IsValid(i)));
        }

        private bool HasOnePair(int i)
        {
            var s = i.ToString();
            return s.Select(c => s.Count(j => j == c)).Any(c => c == 2);
        }

        public override void Part2()
        {
            Console.WriteLine(Enumerable.Range(start,end).Count(i => IsValid(i) && HasOnePair(i)));
        }
    }
}

