using System.Collections.Generic;
using System.Linq;

namespace aoc2019.lib
{
    public class IntCodeVM
    {
        private int i;
        public List<int> v;
        public Queue<int> input, output;
        private readonly List<int> program;

        public IntCodeVM(List<int> tape)
        {
            i = 0;
            program = tape;
            v = tape;
            input = new Queue<int>();
            output = new Queue<int>();
        }

        public enum HaltType
        {
            Terminate,
            Waiting
        }

        enum Op : int
        {
            ADD = 1, MUL = 2, INPUT = 3, OUTPUT = 4, JMP = 5, JNE = 6, LT = 7, EQ = 8, HALT = 99
        }

        public void Reset()
        {
            i = 0;
            v = program;
            input.Clear();
            output.Clear();
        }

        public int Result => output.Dequeue();

        public HaltType Run(params int[] additionalInput)
        {
            foreach (var i in additionalInput) input.Enqueue(i);
            return Run();
        }
        public HaltType Run()
        {
            while (i < v.Count)
            {
                int Val(int mode, int val) =>
                    mode == 0 ? v[val] : val;

                int Val1() => Val(v[i] / 100 % 10, v[i + 1]);
                int Val2() => Val(v[i] / 1000, v[i + 2]);

                switch ((Op)(v[i] % 100))
                {
                    case Op.ADD:
                        v[v[i + 3]] = Val1() + Val2();
                        i += 4; break;
                    case Op.MUL:
                        v[v[i + 3]] = Val1() * Val2();
                        i += 4; break;
                    case Op.INPUT:
                        if (!input.Any())
                            return HaltType.Waiting;
                        v[v[i + 1]] = input.Dequeue();
                        i += 2; break;
                    case Op.OUTPUT:
                        output.Enqueue(Val1());
                        i += 2; break;
                    case Op.JMP:
                        i = Val1() == 0 ? i + 3 : Val2();
                        break;
                    case Op.JNE:
                        i = Val1() != 0 ? i + 3 : Val2();
                        break;
                    case Op.LT:
                        v[v[i + 3]] = Val1() < Val2() ? 1 : 0;
                        i += 4; break;
                    case Op.EQ:
                        v[v[i + 3]] = Val1() == Val2() ? 1 : 0;
                        i += 4; break;
                    case Op.HALT:
                        return HaltType.Terminate;
                }
            }

            return HaltType.Terminate;
        }
    }
}
