using System.Linq;

namespace aoc2019
{
    internal sealed class Day4 : Day
    {
        private readonly int end;

        private readonly int start;

        public Day4()
        {
            var range = Input.First().Split('-').Select(int.Parse).ToList();
            start = range[0];
            end = range[1];
        }

        public override int DayNumber => 4;

        private bool IsValid(int i)
        {
            var prev = 0;
            var hasDup = false;
            foreach (var c in i.ToString())
            {
                var curr = c - '0';
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

        protected override string Part1()
        {
            return $"{Enumerable.Range(start, end).Count(IsValid)}";
        }

        protected override string Part2()
        {
            return $"{Enumerable.Range(start, end).Count(HasOnePair)}";
        }
    }
}