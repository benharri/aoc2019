namespace aoc2019.test;

[TestClass]
public class Tests
{
    [DataTestMethod]
    [DataRow(typeof(Day01), "3394106", "5088280")]
    [DataRow(typeof(Day02), "3085697", "9425")]
    [DataRow(typeof(Day03), "1195", "91518")]
    [DataRow(typeof(Day04), "1079", "699")]
    [DataRow(typeof(Day05), "7692125", "14340395")]
    [DataRow(typeof(Day06), "145250", "274")]
    [DataRow(typeof(Day07), "19650", "35961106")]
    [DataRow(typeof(Day08), "2413",
        "\nxxx   xx  xxx  xxxx xxx  \nx  x x  x x  x    x x  x \nxxx  x    x  x   x  xxx  \nx  x x    xxx   x   x  x \nx  x x  x x    x    x  x \nxxx   xx  x    xxxx xxx  ")]
    [DataRow(typeof(Day09), "3409270027", "82760")]
    [DataRow(typeof(Day10), "260", "608")]
    [DataRow(typeof(Day11), "2054",
        "\n #  # ###  #### ####  ##    ## #  # ###    \n # #  #  #    # #    #  #    # #  # #  #   \n ##   #  #   #  ###  #  #    # #### ###    \n # #  ###   #   #    ####    # #  # #  #   \n # #  # #  #    #    #  # #  # #  # #  #   \n #  # #  # #### #### #  #  ##  #  # ###    ")]
    [DataRow(typeof(Day12), "10635", "583523031727256")]
    // [DataRow(typeof(Day13), "361", "after 7133 moves, the score is: 17590")] // this one takes a LONG time to run
    [DataRow(typeof(Day14), "397771", "3126714")]
    [DataRow(typeof(Day15), "280", "400")]
    [DataRow(typeof(Day16), "90744714", "82994322")]
    [DataRow(typeof(Day17), "2804", "")]
    //[DataRow(typeof(Day18), "", "")]
    [DataRow(typeof(Day19), "114", "10671712")]
    //[DataRow(typeof(Day20), "", "")]
    //[DataRow(typeof(Day21), "", "")]
    //[DataRow(typeof(Day22), "", "")]
    [DataRow(typeof(Day23), "23626", "19019")]
    //[DataRow(typeof(Day24), "", "")]
    //[DataRow(typeof(Day25), "", "")]
    public void TestAllDays(Type dayType, string part1, string part2)
    {
        var s = Stopwatch.StartNew();
        var day = Activator.CreateInstance(dayType) as Day;
        s.Stop();
        Assert.IsNotNull(day, "failed to instantiate day object");
        Assert.IsTrue(File.Exists(day!.FileName));
        Console.Write($"Day {day.DayNumber,2}: {day.PuzzleName,-15} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed in constructor");

        // part 1
        s.Reset();
        s.Start();
        var part1Actual = day.Part1();
        s.Stop();
        Console.Write($"Part 1: {part1Actual,-15} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed");
        Assert.AreEqual(part1, part1Actual, $"Incorrect answer for Day {day.DayNumber} Part1");

        // part 2
        s.Reset();
        s.Start();
        var part2Actual = day.Part2();
        s.Stop();
        Console.Write($"Part 2: {part2Actual,-15} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed");
        Assert.AreEqual(part2, part2Actual, $"Incorrect answer for Day {day.DayNumber} Part2");
    }
}