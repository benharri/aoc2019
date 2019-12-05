using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aoc2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var days = GetDays();

            if (args.Length == 1 && int.TryParse(args[0], out int daynum))
            {
                var d = days.Where(d => d.DayNumber == daynum);
                if (d.Any())
                    d.First().AllParts();
                else
                    Console.WriteLine($"{daynum} invalid or not yet implemented");
            }
            else
            {
                foreach (var d in days)
                {
                    d.AllParts();
                }
            }
        }

        private static IEnumerable<Day> GetDays() =>
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.BaseType == typeof(Day))
                .Select(t => (Day)Activator.CreateInstance(t));
    }
}
