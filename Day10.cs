using aoc2019.lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal sealed class Day10 : Day
    {
        public override int DayNumber => 10;

        private readonly HashSet<(int x, int y)> asteroids = new HashSet<(int x, int y)>();
        private (int x, int y) best = (x: -1, y: -1);
        private int bestcansee;

        public Day10()
        {
            asteroids = Input
                .Select((r, y) => r.Select((c, x) => (x, y, isAsteroid: c == '#')).ToArray())
                .SelectMany(r => r)
                .Where(a => a.isAsteroid)
                .Select(a => (a.x, a.y))
                .ToHashSet();
        }

        public override string Part1()
        {
            foreach (var asteroid in asteroids)
            {
                var cansee = asteroids
                    .Except(new[] { asteroid })
                    .Select(a => (x: a.x - asteroid.x, y: a.y - asteroid.y))
                    .GroupBy(a => Math.Atan2(a.y, a.x))
                    .Count();

                if (cansee > bestcansee)
                {
                    best = asteroid;
                    bestcansee = cansee;
                }
            }
            return $"{bestcansee}";
        }

        public override string Part2()
        {
            static IEnumerable<(int x, int y, double angle, double dist)> GetValue(Queue<(int x, int y, double angle, double dist)> q)
            {
                if (q.Count > 0) yield return q.Dequeue();
            }

            return asteroids
                .Where(a => a != best)
                .Select(a =>
                {
                    var xdist = a.x - best.x;
                    var ydist = a.y - best.y;
                    var angle = Math.Atan2(xdist, ydist);
                    return (a.x, a.y, angle, dist: Math.Sqrt(xdist * xdist + ydist * ydist));
                })
                .ToLookup(a => a.angle)
                .OrderByDescending(a => a.Key)
                .Select(a => new Queue<(int x, int y, double angle, double dist)>(a.OrderBy(b => b.dist)))
                .Repeat()
                .SelectMany(GetValue)
                .Skip(199)
                .Take(1)
                .Select(a => a.x * 100 + a.y)
                .Single()
                .ToString();
        }
    }
}
