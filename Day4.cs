using System.IO;
using System.Linq;

namespace aoc2019
{
    internal class Day4 : Day
    {
        public override int DayNumber => 4;

        private readonly int start;
        private readonly int end;

        public Day4()
        {
            var range = Input.First().Split('-').Select(int.Parse).ToList();
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

        private bool HasOnePair(int i)
        {
            var s = i.ToString();
            return IsValid(i) && s.Select(c => s.Count(j => j == c)).Any(c => c == 2);
        }

        public override string Part1()
        {
            return $"{Enumerable.Range(start, end).Count(IsValid)}";
        }

        public override string Part2()
        {
            return $"{Enumerable.Range(start, end).Count(HasOnePair)}";
        }
    }
}

