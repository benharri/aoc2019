using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal sealed class Day12 : Day
    {
        private readonly List<Position> moons;
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

        public override int DayNumber => 12;

        public static long LCM(long a, long b)
        {
            return a * b / GCD(a, b);
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

            step++;
        }

        protected override string Part1()
        {
            while (step < 1000)
                Step();

            return $"{moons.Sum(p => p.TotalEnergy)}";
        }

        protected override string Part2()
        {
            int cycleX = 0, cycleY = 0, cycleZ = 0;

            while (cycleX == 0 || cycleY == 0 || cycleZ == 0)
            {
                Step();
                if (cycleX == 0 && moons.All(m => m.dx == 0)) cycleX = step * 2;
                if (cycleY == 0 && moons.All(m => m.dy == 0)) cycleY = step * 2;
                if (cycleZ == 0 && moons.All(m => m.dz == 0)) cycleZ = step * 2;
            }

            return $"{LCM(cycleX, LCM(cycleY, cycleZ))}";
        }

        public class Position
        {
            public int dx, dy, dz;
            private List<Position> siblings;
            public int x, y, z;

            public Position(IList<int> moon)
            {
                x = moon[0];
                y = moon[1];
                z = moon[2];
                dx = 0;
                dy = 0;
                dz = 0;
            }

            internal int KineticEnergy =>
                Math.Abs(x) + Math.Abs(y) + Math.Abs(z);

            internal int PotentialEnergy =>
                Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz);

            internal int TotalEnergy =>
                KineticEnergy * PotentialEnergy;

            public void SetSiblings(List<Position> positions)
            {
                siblings = positions.Where(p => p != this).ToList();
            }

            public override string ToString()
            {
                return $"pos=<x={x}, y={y}, z={z}> vel=<x={dx}, y={dy}, z={dz}>";
            }

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
        }
    }
}