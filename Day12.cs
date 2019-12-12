using aoc2019.lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal class Day12 : Day
    {
        public override int DayNumber => 12;

        private List<Position> moons;
        private readonly List<Position> startingPositions;
        private int step;

        public Day12()
        {
            moons = Input
                .Select(moon => 
                    moon
                    .TrimStart('<')
                    .TrimEnd('>')
                    .Split(",")
                    .Select(val => int.Parse(val.Split("=").Last()))
                )
                .Select(moon => new Position(moon.ToList()))
                .ToList();

            foreach (var moon in moons)
                moon.SetSiblings(moons);

            startingPositions = moons;
        }

        public class Position
        {
            public int x, y, z;
            public int dx, dy, dz;
            List<Position> siblings;

            public Position(IList<int> moon)
            {
                x = moon[0];
                y = moon[1];
                z = moon[2];
                dx = 0; dy = 0; dz = 0;
            }

            public void SetSiblings(List<Position> positions)
            {
                siblings = positions.Where(p => p != this).ToList();
            }

            public override string ToString() =>
                $"pos=<x={x}, y={y}, z={z}> vel=<x={dx}, y={dy}, z={dz}>";

            internal void Gravitate()
            {
                foreach (var m in siblings)
                {
                    if (x != m.x) dx += x > m.x ? -1 : 1;
                    if (y != m.y) dy += y > m.y ? -1 : 1;
                    if (z != m.z) dz += z > m.z ? -1 : 1;
                }
            }

            internal void Move()
            {
                x += dx;
                y += dy;
                z += dz;
            }

            internal int KineticEnergy =>
                Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
            internal int PotentialEnergy =>
                Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz);
            internal int TotalEnergy =>
                KineticEnergy * PotentialEnergy;
        }

        public static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }

        public static long GCD(long a, long b)
        {
            if (b == 0) return a;
            return GCD(b, a % b);
        }

        private void Step()
        {
            foreach (var moon in moons)
                moon.Gravitate();

            foreach (var moon in moons)
                moon.Move();
        }

        public override string Part1()
        {
            for (step = 0; step < 1000; step++)
                Step();

            return $"{moons.Sum(p => p.TotalEnergy)}";
        }

        public override string Part2()
        {
            moons = startingPositions;
            step = 0;
            var seenX = new HashSet<string>();
            var seenY = new HashSet<string>();
            var seenZ = new HashSet<string>();
            int repX = 0, repY = 0, repZ = 0;

            while (true)
            {
                if (repX != 0 && repY != 0 && repZ != 0) break;
                Step();

                if (repX == 0)
                {
                    var xcoords = moons.Select(m => (m.x, m.dx)).ToDelimitedString();
                    if (seenX.Contains(xcoords)) repX = step;
                    seenX.Add(xcoords);
                }
                if (repY == 0)
                {
                    var ycoords = moons.Select(m => (m.y, m.dy)).ToDelimitedString();
                    if (seenY.Contains(ycoords)) repY = step;
                    seenY.Add(ycoords);
                }
                if (repZ == 0)
                {
                    var zcoords = moons.Select(m => (m.z, m.dz)).ToDelimitedString();
                    if (seenZ.Contains(zcoords)) repZ = step;
                    seenZ.Add(zcoords);
                }
                step++;
            }

            return $"{LCM(repX, LCM(repY, repZ))}";
        }
    }
}
