﻿namespace aoc2019;

public sealed class Day05 : Day
{
    private readonly IEnumerable<int> tape;
    private int output;

    public Day05() : base(5, "Sunny with a Chance of Asteroids")
    {
        tape = Input.First().Split(',').Select(int.Parse);
    }

    private void RunIntCode(IList<int> v, int input)
    {
        var i = 0;
        while (i < v.Count && v[i] != 99)
        {
            int Val(int mode, int val)
            {
                return mode != 0 ? val : v[val];
            }

            var mode1 = v[i] / 100 % 10;
            var mode2 = v[i] / 1000;

            switch (v[i] % 100)
            {
                case 1:
                    v[v[i + 3]] = Val(mode1, v[i + 1]) + Val(mode2, v[i + 2]);
                    i += 4;
                    break;
                case 2:
                    v[v[i + 3]] = Val(mode1, v[i + 1]) * Val(mode2, v[i + 2]);
                    i += 4;
                    break;
                case 3:
                    v[v[i + 1]] = input;
                    i += 2;
                    break;
                case 4:
                    output = Val(mode1, v[i + 1]);
                    i += 2;
                    break;
                case 5:
                    i = Val(mode1, v[i + 1]) == 0 ? i + 3 : Val(mode2, v[i + 2]);
                    break;
                case 6:
                    i = Val(mode1, v[i + 1]) != 0 ? i + 3 : Val(mode2, v[i + 2]);
                    break;
                case 7:
                    v[v[i + 3]] = Val(mode1, v[i + 1]) < Val(mode2, v[i + 2]) ? 1 : 0;
                    i += 4;
                    break;
                case 8:
                    v[v[i + 3]] = Val(mode1, v[i + 1]) == Val(mode2, v[i + 2]) ? 1 : 0;
                    i += 4;
                    break;
            }
        }
    }

    public override string Part1()
    {
        RunIntCode(tape.ToList(), 1);
        return $"{output}";
    }

    public override string Part2()
    {
        RunIntCode(tape.ToList(), 5);
        return $"{output}";
    }
}
