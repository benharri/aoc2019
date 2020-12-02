using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal sealed class Day6 : Day
    {
        private readonly Dictionary<string, string> input;

        public Day6()
        {
            input = Input.ToDictionary(i => i.Split(')')[1], i => i.Split(')')[0]);
        }

        public override int DayNumber => 6;

        private List<string> GetParents(string obj)
        {
            var res = new List<string>();
            for (var curr = obj; curr != "COM"; curr = input[curr])
                res.Add(curr);
            res.Add("COM");
            return res;
        }

        public override string Part1()
        {
            return $"{input.Keys.Sum(o => GetParents(o).Count - 1)}";
        }

        public override string Part2()
        {
            var you = GetParents("YOU");
            var san = GetParents("SAN");
            var common = 1;
            for (; you[^common] == san[^common]; common++) ;
            return $"{you.Count + san.Count - common * 2}";
        }
    }
}