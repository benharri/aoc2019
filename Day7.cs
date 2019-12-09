using aoc2019.lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal class Day7 : Day
    {
        public override int DayNumber => 7;

        private readonly IntCodeVM[] Amplifiers = new IntCodeVM[5];
        public Day7()
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
                    vm.input.Enqueue(phase);
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
