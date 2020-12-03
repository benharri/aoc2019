using System;
using System.Collections.Generic;
using System.Linq;
using aoc2019.lib;

namespace aoc2019
{
    internal sealed class Day15 : Day
    {
        private readonly IntCodeVM vm;
        private readonly bool verbose = false;

        public Day15()
        {
            vm = new IntCodeVM(Input.First());
        }

        public override int DayNumber => 15;

        protected override string Part1()
        {
            vm.Reset();
            var currentLocation = new Location(0, 0);
            var halt = IntCodeVM.HaltType.Waiting;
            while (halt == IntCodeVM.HaltType.Waiting)
            {
                var direction = currentLocation.NextDirection();
                if (direction <= 4)
                {
                    var (x, y) = currentLocation.Neighbor(direction);
                    if (Location.GetLocation(x, y) == null)
                    {
                        halt = vm.Run(direction);
                        switch (vm.Result)
                        {
                            case Location.Wall:
                                new Location(x, y, Location.Opposites[direction], Location.Wall);
                                break;
                            case Location.Empty:
                                currentLocation = new Location(x, y, Location.Opposites[direction]);
                                break;
                            case Location.System:
                                currentLocation = new Location(x, y, Location.Opposites[direction], Location.System);
                                break;
                            default:
                                throw new Exception($"Unknown IntCodeVM response: {vm.Result}");
                        }
                    }
                }
                else
                {
                    direction = currentLocation.PreviousDirection;
                    if (direction > 0)
                    {
                        halt = vm.Run(direction);
                        switch (vm.Result)
                        {
                            case Location.Empty:
                            case Location.System:
                                currentLocation = Location.GetLocation(currentLocation.Neighbor(direction));
                                break;
                            default:
                                throw new Exception($"Unknown or unexpected response for previous room: {vm.Result}");
                        }
                    }
                    else
                    {
                        if (verbose)
                        {
                            // find extents of canvas
                            int xMin, xMax, yMin, yMax;
                            xMin = yMin = int.MaxValue;
                            xMax = yMax = int.MinValue;
                            foreach (var (x, y) in Location.AllLocations.Keys)
                            {
                                if (x < xMin) xMin = x;
                                if (x > xMax) xMax = x;
                                if (y < yMin) yMin = y;
                                if (y > yMax) yMax = y;
                            }

                            Console.WriteLine($"Canvas extends from ({xMin}, {yMin}) to ({xMax}, {yMax})");

                            // print board
                            for (var y = yMin; y <= yMax; y++)
                            {
                                var line = "";
                                for (var x = xMin; x <= xMax; x++)
                                    if (Location.AllLocations.ContainsKey((x, y)))
                                        line += Location.AllLocations[(x, y)].Image();
                                    else
                                        line += "@";

                                Console.WriteLine(line);
                            }
                        }

                        currentLocation = Location.OxygenLocation;
                        var distance = 0;
                        while (currentLocation.PreviousDirection != 0)
                        {
                            distance++;
                            currentLocation = Location.GetLocation(currentLocation.PreviousLocation());
                        }

                        return $"{distance}";
                    }
                }
            }

            return "";
        }

        protected override string Part2()
        {
            var changed = true;
            while (changed)
            {
                changed = false;
                foreach (var location in Location.AllLocations.Values)
                    changed = location.UpdateDistanceToOxygenSystem() || changed;
            }

            return Location.AllLocations.Values
                .Where(l => !l.IsWall)
                .Max(l => l.DistanceToOxygenSystem)
                .ToString();
        }

        private class Location
        {
            public const int Wall = 0;
            public const int Empty = 1;
            public const int System = 2;

            private static readonly int[] Dx = {0, 0, 0, 1, -1};
            private static readonly int[] Dy = {0, 1, -1, 0, 0};
            public static readonly int[] Opposites = {0, 2, 1, 4, 3};

            public static readonly Dictionary<(int x, int y), Location>
                AllLocations = new Dictionary<(int x, int y), Location>();

            private readonly int currentType;
            public int DistanceToOxygenSystem = int.MaxValue - 1;

            private int searchDirection = 1;

            public Location(int x, int y, int prev = 0, int type = Empty)
            {
                PreviousDirection = prev;
                currentType = type;
                X = x;
                Y = y;

                if (type == System)
                {
                    OxygenLocation = this;
                    DistanceToOxygenSystem = 0;
                    // Console.WriteLine($"Found Oxygen System at ({x}, {y})");
                }

                AllLocations.Add((x, y), this);
            }

            public static Location OxygenLocation { get; private set; }
            public int PreviousDirection { get; }
            private int X { get; }
            private int Y { get; }

            public bool IsWall => currentType == Wall;

            public string Image()
            {
                return currentType switch
                {
                    Wall => "\u2587",
                    Empty => X == 0 && Y == 0 ? "S" : " ",
                    System => "O",
                    _ => "?"
                };
            }

            public bool UpdateDistanceToOxygenSystem()
            {
                if (currentType != Empty) return false;

                foreach (var direction in Enumerable.Range(1, 4))
                {
                    var distance = GetLocation(Neighbor(direction))?.DistanceToOxygenSystem ?? int.MaxValue;
                    if (distance + 1 < DistanceToOxygenSystem)
                    {
                        DistanceToOxygenSystem = distance + 1;
                        return true;
                    }
                }

                return false;
            }

            public (int, int) Neighbor(int direction)
            {
                return (X + Dx[direction], Y + Dy[direction]);
            }

            public (int, int) PreviousLocation()
            {
                return Neighbor(PreviousDirection);
            }

            public int NextDirection()
            {
                return searchDirection++;
            }

            public static Location GetLocation(int x, int y)
            {
                return AllLocations.ContainsKey((x, y)) ? AllLocations[(x, y)] : null;
            }

            public static Location GetLocation((int x, int y) coords)
            {
                return GetLocation(coords.x, coords.y);
            }
        }
    }
}