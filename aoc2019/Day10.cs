using System;
using System.Collections.Generic;
using System.Linq;
using aoc2019.lib;

namespace aoc2019
{
    public sealed class Day10 : Day
    {
        private readonly HashSet<(int x, int y)> asteroids;
        private (int x, int y) best = (x: -1, y: -1);
        private int bestCanSee;

        public Day10() : base(10, "Monitoring Station")
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
                var canSee = asteroids
                    .Except(new[] {asteroid})
                    .Select(a => (x: a.x - asteroid.x, y: a.y - asteroid.y))
                    .GroupBy(a => Math.Atan2(a.y, a.x))
                    .Count();

                if (canSee > bestCanSee)
                {
                    best = asteroid;
                    bestCanSee = canSee;
                }
            }

            return $"{bestCanSee}";
        }

        public override string Part2()
        {
            static IEnumerable<(int x, int y, double angle, double dist)> GetValue(
                Queue<(int x, int y, double angle, double dist)> q)
            {
                if (q.Count > 0) yield return q.Dequeue();
            }

            return asteroids
                .Where(a => a != best)
                .Select(a =>
                {
                    var xDist = a.x - best.x;
                    var yDist = a.y - best.y;
                    var angle = Math.Atan2(xDist, yDist);
                    return (a.x, a.y, angle, dist: Math.Sqrt(xDist * xDist + yDist * yDist));
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