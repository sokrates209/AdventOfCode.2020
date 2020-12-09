using System;
using System.Linq;
using Advent.Inputs;

namespace Advent.App
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Select day You would like to run. 1-24.");

            var day = Console.ReadLine();
            if (!int.TryParse(day, out var dayNumber))
                throw new ArgumentOutOfRangeException($"Expects day number in format 1 - 24, but {day} was provided.");
            switch (dayNumber)
            {
                case 1:
                    Console.WriteLine("Day 1:");
                    Puzzle.Day1.PartOne();
                    Puzzle.Day1.PartTwo();

                    break;
                case 2:
                    Console.WriteLine("Day 2:");
                    Puzzle.Day2.PartOne();
                    Puzzle.Day2.PartTwo();

                    break;
                case 3:
                    Console.WriteLine("Day 3:");
                    Puzzle.Day3.PartOne();
                    Puzzle.Day3.PartTwo();

                    break;
                case 4:
                    Console.WriteLine("Day 4:");
                    Puzzle.Day4.PartOne();
                    Puzzle.Day4.PartTwo();

                    break;
                case 5:
                    Console.WriteLine("Day 5:");
                    Puzzle.Day5.PartOne(Inputs.Day5.Data.ToList());
                    Puzzle.Day5.PartTwo(Inputs.Day5.Data.ToList());

                    break;
                case 6:
                    Console.WriteLine("Day 6:");
                    Puzzle.Day6.PartOne(Inputs.Day6.Data.ToList());
                    Puzzle.Day6.PartTwo(Inputs.Day6.Data.ToList());

                    break;

                case 7:
                    Console.WriteLine("Day 7:");
                    Puzzle.Day7.PartOne(DayInputLoader.GetData(InputFileNames.Day7));
                    Puzzle.Day7.PartTwo(DayInputLoader.GetData(InputFileNames.Day7));

                    break;

                case 8:
                    Console.WriteLine("Day 8:");
                    Puzzle.Day8.PartOne(DayInputLoader.GetData(InputFileNames.Day8));
                    Puzzle.Day8.PartTwo(DayInputLoader.GetData(InputFileNames.Day8));

                    break;

                case 9:
                {
                    Console.WriteLine("Day 9:");
                    var result = Puzzle.Day9.PartOne(DayInputLoader.GetData(InputFileNames.Day9), 25);
                    Puzzle.Day9.PartTwo(DayInputLoader.GetData(InputFileNames.Day9), result);

                    break;
                }
            }
        }
    }
}