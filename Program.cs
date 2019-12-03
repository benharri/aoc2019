using System;

namespace aoc2019
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1 && int.TryParse(args[0], out int daynum))
            {
                if (daynum >= 0 && daynum <= 25)
                {
                    Day day = DayFactory.GetDay(daynum);
                    day.AllParts();
                }
                else
                {
                    Console.WriteLine($"{daynum} is an invalid day");
                    return;
                }
            }
            else
            {
                for (var i = 1; i <= 25; ++i)
                {
                    var day = DayFactory.GetDay(i);
                    if (day == null) continue;
                    Console.WriteLine($"Day {i}:");
                    day.AllParts();
                }
            }
        }
    }
}
