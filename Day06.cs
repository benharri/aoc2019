using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal sealed class Day06 : Day
    {
        private readonly Dictionary<string, string> input;

        public Day06() : base(6, "Universal Orbit Map")
        {
            input = Input.ToDictionary(i => i.Split(')')[1], i => i.Split(')')[0]);
        }

        private List<string> GetParents(string obj)
        {
            var res = new List<string>();
            for (var curr = obj; curr != "COM"; curr = input[curr])
                res.Add(curr);
            res.Add("COM");
            return res;
        }

        protected override string Part1()
        {
            return $"{input.Keys.Sum(o => GetParents(o).Count - 1)}";
        }

        protected override string Part2()
        {
            var you = GetParents("YOU");
            var san = GetParents("SAN");
            var common = 1;
            for (; you[^common] == san[^common]; common++) ;
            return $"{you.Count + san.Count - common * 2}";
        }
    }
}