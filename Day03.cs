using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal sealed class Day03 : Day
    {
        private readonly IEnumerable<(int, int)> intersections;
        private readonly List<Dictionary<(int, int), int>> wires;

        public Day03() : base(3, "Crossed Wires")
        {
            wires = Input.Select(ParseWire).ToList();
            intersections = wires[0].Keys.Intersect(wires[1].Keys);
        }

        protected override string Part1()
        {
            return $"{intersections.Min(x => Math.Abs(x.Item1) + Math.Abs(x.Item2))}";
        }

        protected override string Part2()
        {
            // add 2 to count (0, 0) on both lines
            return $"{intersections.Min(x => wires[0][x] + wires[1][x]) + 2}";
        }

        private static Dictionary<(int, int), int> ParseWire(string line)
        {
            var r = new Dictionary<(int, int), int>();
            int x = 0, y = 0, c = 0, i;

            foreach (var step in line.Split(','))
            {
                var d = int.Parse(step.Substring(1));
                switch (step[0])
                {
                    case 'U':
                        for (i = 0; i < d; i++) r.TryAdd((x, ++y), c++);
                        break;
                    case 'D':
                        for (i = 0; i < d; i++) r.TryAdd((x, --y), c++);
                        break;
                    case 'R':
                        for (i = 0; i < d; i++) r.TryAdd((++x, y), c++);
                        break;
                    case 'L':
                        for (i = 0; i < d; i++) r.TryAdd((--x, y), c++);
                        break;
                }
            }

            return r;
        }
    }
}