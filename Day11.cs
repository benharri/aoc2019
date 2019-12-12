using aoc2019.lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal class Day11 : Day
    {
        public override int DayNumber => 11;

        private readonly IntCodeVM vm;
        private long x, y;
        private Direction heading;

        public Day11()
        {
            vm = new IntCodeVM(Input.First());
        }

        enum Direction
        {
            Up, Down, Left, Right
        }

        private void Move()
        {
            switch (heading)
            {
                case Direction.Up: y++; break;
                case Direction.Down: y--; break;
                case Direction.Left: x--; break;
                case Direction.Right: x++; break;
            };
        }

        private void Turn(long direction)
        {
            switch (heading)
            {
                case Direction.Up: heading = direction == 0 ? Direction.Left : Direction.Right; break;
                case Direction.Down: heading = direction == 0 ? Direction.Right : Direction.Left; break;
                case Direction.Left: heading = direction == 0 ? Direction.Down : Direction.Up; break;
                case Direction.Right: heading = direction == 0 ? Direction.Up : Direction.Down; break;
            }
            Move();
        }

        private Dictionary<(long x, long y), long> PaintShip(int initialVal)
        {
            var map = new Dictionary<(long, long), long>();
            vm.Reset();
            heading = Direction.Up;
            x = 0; y = 0;
            map[(x, y)] = initialVal;

            var haltType = IntCodeVM.HaltType.Waiting;
            while (haltType == IntCodeVM.HaltType.Waiting)
            {
                haltType = vm.Run(map.GetValueOrDefault((x, y)));
                map[(x, y)] = vm.Result;
                Turn(vm.Result);
            }

            return map;
        }

        public override string Part1()
        {
            return $"{PaintShip(0).Count}";
        }

        public override string Part2()
        {
            var map = PaintShip(1);
            int minX = (int)map.Keys.Select(x => x.x).Min();
            int maxX = (int)map.Keys.Select(x => x.x).Max();
            int minY = (int)map.Keys.Select(x => x.y).Min();
            int maxY = (int)map.Keys.Select(x => x.y).Max();

            return Enumerable.Range(minY, maxY - minY + 1)
                .Select(y =>
                    Enumerable.Range(minX, maxX - minX + 1)
                    .Select(x => map.GetValueOrDefault((x, y)) == 0 ? ' ' : '#')
                    .ToDelimitedString()
                )
                .Reverse()
                .ToDelimitedString(Environment.NewLine);
        }
    }
}
