﻿namespace aoc2019;

public sealed class Day19 : Day
{
    private readonly long[,] grid;
    private readonly IntCodeVM vm;

    public Day19() : base(19, "Tractor Beam")
    {
        vm = new(Input.First());
        grid = new long[50, 50];
    }

    public override string Part1()
    {
        for (var x = 0; x < 50; x++)
            for (var y = 0; y < 50; y++)
            {
                vm.Reset();
                vm.Run(x, y);
                grid[x, y] = vm.Result;
            }

        return $"{grid.Cast<long>().Sum()}";
    }

    public override string Part2()
    {
        for (int x = 101, y = 0; ; x++)
        {
            while (true)
            {
                vm.Reset();
                vm.Run(x, y);
                if (vm.Result == 1) break;
                y++;
            }

            vm.Reset();
            vm.Run(x - 99, y + 99);
            if (vm.Result == 1)
                return $"{(x - 99) * 1e4 + y}";
        }
    }
}
