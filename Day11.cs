using aoc2019.lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace aoc2019
{
    internal class Day11 : Day
    {
        public override int DayNumber => 11;

        private IntCodeVM vm;
        List<List<bool>> paintmap;
        long x, y;
        Direction heading;

        public Day11()
        {
            vm = new IntCodeVM(Input.First());
            paintmap = new List<List<bool>>();
            x = 0; y = 0;
            heading = Direction.Up;
        }

        enum Direction
        {
            Up, Down, Left, Right
        }

        private (long, long) DxDy()
        {
            return heading switch
            {
                Direction.Up => (0, 1),
                Direction.Down => (0, -1),
                Direction.Left => (-1, 0),
                Direction.Right => (1, 0)
            };
        }

        private void Turn(long direction)
        {
            switch (heading)
            {
                case Direction.Up:    heading = direction == 0 ? Direction.Left : Direction.Right; break;
                case Direction.Down:  heading = direction == 0 ? Direction.Right : Direction.Left; break;
                case Direction.Left:  heading = direction == 0 ? Direction.Down : Direction.Up; break;
                case Direction.Right: heading = direction == 0 ? Direction.Up : Direction.Down; break;
            }
        }

        public override string Part1()
        {
            vm.Reset();
            vm.Run();
            var output = vm.output.ToList();
            long dx, dy;
            for (var i = 0; i < output.Count; i += 2)
            {
                long color = output[i];
                Turn(output[i + 1]);
                paintmap[x][y] = color == 0;
                (dx, dy) = DxDy();
                x += dx; y += dy;
            }
            return $"{paintmap.Count(x => x != null)}";
        }

        public override string Part2()
        {
            return "";
        }
    }
}
