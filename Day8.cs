using aoc2019.lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal class Day8 : Day
    {
        public override int DayNumber => 8;

        private readonly List<List<char>> photo;
        public Day8()
        {
            photo = Input.First().Chunk(25 * 6).Select(s => s.ToList()).ToList();
        }

        public override string Part1()
        {
            var l = photo.OrderBy(layer => layer.Count(pixel => pixel == '0')).First();
            return $"{l.Count(p => p == '1') * l.Count(p => p == '2')}";
        }

        public override string Part2()
        {
            return Enumerable.Range(0, 25 * 6)
                .Select(p => Enumerable.Range(0, photo.Count)
                    .Select(l => photo[l][p])
                    .Aggregate('2', (acc, next) =>
                        acc != '2' ? acc : next == '0' ? ' ' : next
                    )
                )
                .ToDelimitedString()
                .Chunk(25)
                .ToDelimitedString(Environment.NewLine)
                .Replace('1', 'x');
        }
    }
}