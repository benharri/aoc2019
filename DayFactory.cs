using System;

namespace aoc2019
{
    internal class DayFactory
    {
        internal static Day GetDay(int daynum)
        {
            switch (daynum)
            {
                case 1: return new Day1();
                case 2: return new Day2();
                case 3: return new Day3();
                default: return null;
            }
        }
    }
}