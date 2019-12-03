using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc2019
{
    public class Day2
    {
        private static IEnumerable<int> input = 
            File.ReadLines("input/day2.in")
            .First()
            .Split(',')
            .Select(num => int.Parse(num));

        public static void RunIntCode(ref List<int> v)
        {
            for (var i = 0; v[i] != 99; i += 4)
                switch (v[i])
                {
                    case 1: v[v[i + 3]] = v[v[i + 1]] + v[v[i + 2]]; break;
                    case 2: v[v[i + 3]] = v[v[i + 1]] * v[v[i + 2]]; break;
                }
        }

        public static void Part1()
        {
            var output = input.ToList();
            output[1] = 12;
            output[2] = 2;

            RunIntCode(ref output);

            Console.WriteLine($"{output[0]}");
        }

        public static void Part2()
        {
            List<int> output;

            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    output = input.ToList();
                    output[1] = i;
                    output[2] = j;

                    RunIntCode(ref output);

                    if (output[0] == 19690720)
                    {
                        Console.WriteLine($"{100 * i + j}");
                        return;
                    }
                }
            }
        }
    }
}

