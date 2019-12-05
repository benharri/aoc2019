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

        public int RunIntCode(int noun, int verb)
        {
            var v = input.ToList();
            v[1] = noun; v[2] = verb;

            for (var i = 0; v[i] != 99; i += 4)
                v[v[i + 3]] = v[i] switch
                {
                    1 => v[v[i + 1]] + v[v[i + 2]],
                    2 => v[v[i + 1]] * v[v[i + 2]]
                };

            return v[0];
        }

        public override string Part1()
        {
            return $"{RunIntCode(12, 2)}";
        }

        public override string Part2()
        {
            for (var i = 0; i < 100; i++)
                for (var j = 0; j < 100; j++)
                    if (RunIntCode(i, j) == 19690720)
                        return $"{100 * i + j}";

            return string.Empty;
        }
    }
}

