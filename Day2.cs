using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc2019
{
    public class Day2 : Day
    {
        public override int DayNumber => 2;

        private readonly IEnumerable<int> input =
            File.ReadLines("input/day2.in").First().Split(',').Select(int.Parse);

        public static List<int> RunIntCode(int noun, int verb, List<int> v)
        {
            v[1] = noun; v[2] = verb;

            for (var i = 0; v[i] != 99; i += 4)
                switch (v[i])
                {
                    case 1: v[v[i + 3]] = v[v[i + 1]] + v[v[i + 2]]; break;
                    case 2: v[v[i + 3]] = v[v[i + 1]] * v[v[i + 2]]; break;
                }
            return v;
        }

        public override string Part1()
        {
            return $"{RunIntCode(12, 2, input.ToList())[0]}";
        }

        public override string Part2()
        {
            for (var i = 0; i < 100; i++)
                for (var j = 0; j < 100; j++)
                    if (RunIntCode(i, j, input.ToList())[0] == 19690720)
                        return $"{100 * i + j}";

            return string.Empty;
        }
    }
}

