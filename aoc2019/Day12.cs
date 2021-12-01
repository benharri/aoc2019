namespace aoc2019;

public sealed class Day12 : Day
{
    private readonly List<Position> moons;
    private int step;

    public Day12() : base(12, "The N-Body Problem")
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
    }

    private static long Lcm(long a, long b) => a * b / Gcd(a, b);

    private static long Gcd(long a, long b)
    {
        while (true)
        {
            if (b == 0) return a;
            var a1 = a;
            a = b;
            b = a1 % b;
        }
    }

    private void Step()
    {
        foreach (var moon in moons)
            moon.Gravitate();

        foreach (var moon in moons)
            moon.Move();

        step++;
    }

    public override string Part1()
    {
        while (step < 1000)
            Step();

        return $"{moons.Sum(p => p.TotalEnergy)}";
    }

    public override string Part2()
    {
        int cycleX = 0, cycleY = 0, cycleZ = 0;

        while (cycleX == 0 || cycleY == 0 || cycleZ == 0)
        {
            Step();
            if (cycleX == 0 && moons.All(m => m.Dx == 0)) cycleX = step * 2;
            if (cycleY == 0 && moons.All(m => m.Dy == 0)) cycleY = step * 2;
            if (cycleZ == 0 && moons.All(m => m.Dz == 0)) cycleZ = step * 2;
        }

        return $"{Lcm(cycleX, Lcm(cycleY, cycleZ))}";
    }

    public class Position
    {
        public int Dx, Dy, Dz;
        private List<Position> siblings;
        private int x, y, z;

        public Position(IList<int> moon)
        {
            x = moon[0];
            y = moon[1];
            z = moon[2];
            Dx = 0;
            Dy = 0;
            Dz = 0;
            siblings = new();
        }

        private int KineticEnergy =>
            Math.Abs(x) + Math.Abs(y) + Math.Abs(z);

        private int PotentialEnergy =>
            Math.Abs(Dx) + Math.Abs(Dy) + Math.Abs(Dz);

        internal int TotalEnergy =>
            KineticEnergy * PotentialEnergy;

        public void SetSiblings(IEnumerable<Position> positions)
        {
            siblings = positions.Where(p => p != this).ToList();
        }

        public override string ToString()
        {
            return $"pos=<x={x}, y={y}, z={z}> vel=<x={Dx}, y={Dy}, z={Dz}>";
        }

        internal void Gravitate()
        {
            foreach (var m in siblings)
            {
                if (x != m.x) Dx += x > m.x ? -1 : 1;
                if (y != m.y) Dy += y > m.y ? -1 : 1;
                if (z != m.z) Dz += z > m.z ? -1 : 1;
            }
        }

        internal void Move()
        {
            x += Dx;
            y += Dy;
            z += Dz;
        }
    }
}
