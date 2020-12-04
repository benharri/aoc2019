using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.lib
{
    public class IntCodeVM
    {
        public enum HaltType
        {
            Terminate,
            Waiting
        }

        private readonly long[] program;
        private long i;
        public Queue<long> input, output;
        public long[] memory;
        private long relbase;

        public IntCodeVM(string tape)
        {
            i = 0;
            relbase = 0;
            program = tape.Split(',').Select(long.Parse).ToArray();
            memory = program;
            input = new Queue<long>();
            output = new Queue<long>();
        }

        public long Result => output.Dequeue();

        public void Reset()
        {
            i = 0;
            relbase = 0;
            memory = program;
            input.Clear();
            output.Clear();
        }

        public void AddInput(params long[] values)
        {
            foreach (var v in values) AddInput(v);
        }

        public void AddInput(long value)
        {
            input.Enqueue(value);
        }

        private long MemGet(long addr)
        {
            return addr < memory.Length ? memory[addr] : 0;
        }

        private void MemSet(long addr, long value)
        {
            if (addr < 0) addr = 0;
            if (addr >= memory.Length)
                Array.Resize(ref memory, (int) addr + 1);
            memory[addr] = value;
        }

        private long Mode(long idx)
        {
            var mode = MemGet(i) / 100;
            for (var s = 1; s < idx; s++)
                mode /= 10;
            return mode % 10;
        }

        private long Get(long idx)
        {
            var param = MemGet(i + idx);
            switch (Mode(idx))
            {
                case 0: return MemGet(param);
                case 1: return param;
                case 2: return MemGet(relbase + param);
                default: throw new Exception("invalid parameter mode");
            }
        }

        private void Set(long idx, long val)
        {
            var param = MemGet(i + idx);
            switch (Mode(idx))
            {
                case 0:
                    MemSet(param, val);
                    break;
                case 1: throw new Exception("cannot set in immediate mode");
                case 2:
                    MemSet(relbase + param, val);
                    break;
                default: throw new Exception("invalid parameter mode");
            }
        }

        public HaltType Run(params long[] additionalInput)
        {
            foreach (var s in additionalInput) AddInput(s);
            return Run();
        }

        public HaltType Run()
        {
            while (i < memory.Length)
            {
                var op = MemGet(i) % 100;
                switch (op)
                {
                    case 1:
                        Set(3, Get(1) + Get(2));
                        i += 4;
                        break;
                    case 2:
                        Set(3, Get(1) * Get(2));
                        i += 4;
                        break;
                    case 3:
                        if (!input.Any())
                            return HaltType.Waiting;
                        Set(1, input.Dequeue());
                        i += 2;
                        break;
                    case 4:
                        output.Enqueue(Get(1));
                        i += 2;
                        break;
                    case 5:
                        i = Get(1) == 0 ? i + 3 : Get(2);
                        break;
                    case 6:
                        i = Get(1) != 0 ? i + 3 : Get(2);
                        break;
                    case 7:
                        Set(3, Get(1) < Get(2) ? 1 : 0);
                        i += 4;
                        break;
                    case 8:
                        Set(3, Get(1) == Get(2) ? 1 : 0);
                        i += 4;
                        break;
                    case 9:
                        relbase += Get(1);
                        i += 2;
                        break;
                    case 99:
                        return HaltType.Terminate;
                    default:
                        throw new Exception($"unknown op {op} at {i}");
                }
            }

            return HaltType.Terminate;
        }
    }
}