using System.Collections.Generic;
using System.Linq;
using aoc2019.lib;

namespace aoc2019
{
    public sealed class Day11 : Day
    {
        private readonly IntCodeVM vm;
        private Direction heading;
        private long x, y;

        public Day11() : base(11, "Space Police")
        {
            vm = new IntCodeVM(Input.First());
        }

        private void Move()
        {
            switch (heading)
            {
                case Direction.Up:
                    y++;
                    break;
                case Direction.Down:
                    y--;
                    break;
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
            }

            ;
        }

        private void Turn(long direction)
        {
            switch (heading)
            {
                case Direction.Up:
                    heading = direction == 0 ? Direction.Left : Direction.Right;
                    break;
                case Direction.Down:
                    heading = direction == 0 ? Direction.Right : Direction.Left;
                    break;
                case Direction.Left:
                    heading = direction == 0 ? Direction.Down : Direction.Up;
                    break;
                case Direction.Right:
                    heading = direction == 0 ? Direction.Up : Direction.Down;
                    break;
            }

            Move();
        }

        private Dictionary<(long x, long y), long> PaintShip(int initialVal)
        {
            var map = new Dictionary<(long, long), long>();
            vm.Reset();
            heading = Direction.Up;
            x = 0;
            y = 0;
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
            var minX = (int) map.Keys.Select(i => i.x).Min();
            var maxX = (int) map.Keys.Select(i => i.x).Max();
            var minY = (int) map.Keys.Select(i => i.y).Min();
            var maxY = (int) map.Keys.Select(i => i.y).Max();

            return "\n" + Enumerable.Range(minY, maxY - minY + 1)
                .Select(j =>
                    Enumerable.Range(minX, maxX - minX + 1)
                        .Select(i => map.GetValueOrDefault((x: i, y: j)) == 0 ? ' ' : '#')
                        .ToDelimitedString()
                )
                .Reverse()
                .ToDelimitedString("\n");
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}