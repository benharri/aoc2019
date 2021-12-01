namespace aoc2019;

public sealed class Day07 : Day
{
    private readonly IntCodeVM[] amplifiers = new IntCodeVM[5];

    public Day07() : base(7, "Amplification Circuit")
    {
        for (var i = 0; i < 5; i++) amplifiers[i] = new IntCodeVM(Input.First());
    }

    public override string Part1()
    {
        var largest = 0L;

        foreach (var phaseSeq in Enumerable.Range(0, 5).Permute())
        {
            var i = 0L;
            foreach (var (vm, phase) in amplifiers.Zip(phaseSeq))
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
        var largest = 0L;

        foreach (var phaseSeq in Enumerable.Range(5, 5).Permute())
        {
            var i = 0L;
            foreach (var (vm, phase) in amplifiers.Zip(phaseSeq))
            {
                vm.Reset();
                vm.AddInput(phase);
            }

            var vms = new Queue<IntCodeVM>(amplifiers);
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
