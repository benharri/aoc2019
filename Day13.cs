using aoc2019.lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    internal class Day13 : Day
    {
        public override int DayNumber => 13;

        private readonly IntCodeVM vm;
        private readonly Dictionary<(int x, int y), int> board;

        public Day13()
        {
            vm = new IntCodeVM(Input.First());
            board = new Dictionary<(int, int), int>();
        }

        private void UpdateTiles(IEnumerable<long> queue)
        {
            var input = queue.Select(i => (int)i).ToList();

            for (var i = 0; i < input.Count - 2; i += 3)
            {
                var x = input[i];
                var y = input[i + 1];
                var val = input[i + 2];

                if (board.ContainsKey((x, y)))
                    board[(x, y)] = val;
                else
                    board.Add((x, y), val);
            }

        }

        private void PrintBoard()
        {
            foreach (var tile in board)
            {
                var (x, y) = tile.Key;
                if (x < 0 || y < 0) continue;
                Console.SetCursorPosition(x, y);
                Console.Write(tile.Value switch
                {
                    0 => " ",
                    1 => "|",
                    2 => "B",
                    3 => "_",
                    4 => ".",
                    _ => tile.Value
                });
            }
        }

        public override string Part1()
        {
            vm.Reset();
            vm.Run();
            return $"{vm.output.Where((v, i) => (i + 1) % 3 == 0 && v == 2).Count()}";
        }

        public override string Part2()
        {
            vm.Reset();
            vm.memory[0] = 2;
            var printboard = false;
            var gameTicks = 0;
            if (printboard) Console.Clear();

            var haltType = IntCodeVM.HaltType.Waiting;
            while (haltType == IntCodeVM.HaltType.Waiting)
            {
                haltType = vm.Run();
                UpdateTiles(vm.output);

                var (ball, _) = board.First(t => t.Value == 4).Key;
                var (paddle, _) = board.First(t => t.Value == 3).Key;
                vm.AddInput(ball > paddle ? 1 : ball < paddle ? -1 : 0);

                gameTicks++;
                if (printboard) PrintBoard();
            }

            return $"after {gameTicks} moves, the score is: {board[(-1, 0)]}";
        }
    }
}
