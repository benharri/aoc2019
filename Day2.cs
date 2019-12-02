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

        private static bool debug = false;

        public static void Log(string log)
        {
            if (debug)
                Console.WriteLine(log);
        }

        public static void RunIntCode(ref List<int> output)
        {
            for (var i = 0; i < output.Count(); i++)
            {
                if (output[i] == 1)
                {
                    var val = output[output[i + 1]] + output[output[i + 2]];
                    Log($"saving {val} to {output[i + 3]}");
                    output[output[i + 3]] = val;
                    i += 3;
                }
                else if (output[i] == 2)
                {
                    var val = output[output[i + 1]] * output[output[i + 2]];
                    Log($"saving {val} to {output[i + 3]}");
                    output[output[i + 3]] = val;
                    i += 3;
                }
                else
                {
                    Log($"invalid operation: found {output[i]} at {i}");
                    break;
                }
                Log(string.Join(',', output));
            }
        }

        public static void Part1()
        {
            List<int> output = input.ToList();
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
                    }
                }
            }
        }
    }
}

