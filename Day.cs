namespace aoc2019
{
    public abstract class Day
    {
        public virtual void AllParts()
        {
            Part1();
            Part2();
        }
        public abstract void Part1();
        public abstract void Part2();
    }
}
