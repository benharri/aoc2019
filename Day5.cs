using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal sealed class Day5 : Day
    {
        private readonly IEnumerable<int> tape;

        private int output;

        public Day5()
        {
            tape = Input.First().Split(',').Select(int.Parse);
        }

        public override int DayNumber => 5;

        public void RunIntCode(List<int> v, int input)
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

        protected override string Part1()
        {
            RunIntCode(tape.ToList(), 1);
            return $"{output}";
        }

        protected override string Part2()
        {
            RunIntCode(tape.ToList(), 5);
            return $"{output}";
        }
    }
}