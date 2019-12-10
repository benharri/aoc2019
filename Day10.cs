using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace aoc2019
{
    internal class Day10 : Day
    {
        public override int DayNumber => 10;

        private readonly List<Point> asteroids = new List<Point>();
        private Point best = new Point(-1, -1);
        private IGrouping<double, Point>[] bestgroups;
        private int bestcansee;

        public Day10()
        {
            var starmap = Input.Select(x => x.Select(y => y == '#').ToArray()).ToArray();

            for (var i = 0; i < starmap.Length; i++)
                for (var j = 0; j < starmap[i].Length; j++)
                    if (starmap[i][j])
                        asteroids.Add(new Point(i, j));

            foreach (var asteroid in asteroids)
            {
                var groups = asteroids.Except(new[] { asteroid })
                    .Select(a => new Point(a.X - asteroid.X, a.Y - asteroid.Y))
                    .GroupBy(a => Math.Atan2(a.Y, a.X))
                    .ToArray();
                var cansee = groups.Count();

                if (cansee > bestcansee)
                {
                    best = asteroid;
                    bestcansee = cansee;
                    bestgroups = groups;
                }
            }
        }

        public override string Part1()
        {
            return $"{bestcansee}";
        }

        public override string Part2()
        {
            var removals = bestgroups
                .Select(g => new { 
                    Angle = g.Key, 
                    Targets = new Queue<Point>(g.OrderBy(a => 
                        Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2))
                    ))
                })
                .OrderBy(g => g.Angle > Math.PI / 2)
                .ThenByDescending(g => g.Angle);

            var removed = 0;
            while (true)
                foreach (var removal in removals)
                    if (removal.Targets.Count > 0)
                    {
                        var toremove = removal.Targets.Dequeue();
                        removed++;
                        if (removed == 200)
                        {
                            return $"{(toremove.X * 100) + toremove.Y}";
                        }
                    }
        }
    }
}
