using System.Collections.Generic;
using System.Linq;
using aoc2019.lib;

namespace aoc2019
{
    public sealed class Day07 : Day
    {
        private readonly IntCodeVM[] Amplifiers = new IntCodeVM[5];

        public Day07() : base(7, "Amplification Circuit")
        {
            for (var i = 0; i < 5; i++) Amplifiers[i] = new IntCodeVM(Input.First());
        }

        public override string Part1()
        {
            long i, largest = 0;

            foreach (var phaseSeq in Enumerable.Range(0, 5).Permute())
            {
                i = 0;
                foreach (var (vm, phase) in Amplifiers.Zip(phaseSeq))
                {
                    vm.Reset();
                    vm.Run(phase, i);
                    i = vm.Result;
                }

                if (i > largest)
                    largest = i;
            }

            return $"{largest}";
        }

        public override string Part2()
        {
            long i, largest = 0;

            foreach (var phaseSeq in Enumerable.Range(5, 5).Permute())
            {
                i = 0;
                foreach (var (vm, phase) in Amplifiers.Zip(phaseSeq))
                {
                    vm.Reset();
                    vm.AddInput(phase);
                }

                var vms = new Queue<IntCodeVM>(Amplifiers);
                while (vms.Count > 0)
                {
                    var vm = vms.Dequeue();
                    var haltType = vm.Run(i);
                    if (haltType == IntCodeVM.HaltType.Waiting)
                        vms.Enqueue(vm);
                    i = vm.Result;
                }

                if (i > largest)
                    largest = i;
            }

            return $"{largest}";
        }
    }
}