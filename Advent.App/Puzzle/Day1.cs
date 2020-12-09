using System;

namespace Advent.App.Puzzle
{
    public class Day1
    {
        public static void PartTwo()
        {
            var inputData = Advent.Inputs.Day1.Data;
            for (int i = 0; i < inputData.Count - 2; i++)
            {
                var x = inputData[i];
                for (int j = i + 1; j < inputData.Count - 1; j++)
                {
                    var y = inputData[j];
                    for (int k = i + 2; k < inputData.Count; k++)
                    {
                        var z = inputData[k];
                        if (x + y + z == 2020)
                        {
                            Console.WriteLine("Got the answer for Day1.Part2");
                            Console.WriteLine($"{x * y * z}");
                            return;
                        }
                    }
                }
            }
        }

        public static void PartOne()
        {
            var inputData = Advent.Inputs.Day1.Data;
            for (int i = 0; i < inputData.Count - 1; i++)
            {
                var x = inputData[i];
                for (int j = i + 1; j < inputData.Count; j++)
                {
                    var y = inputData[j];
                    if (x + y == 2020)
                    {
                        Console.WriteLine("Got the answer for Day1.Part1");
                        Console.WriteLine($"{x * y}");
                        return;
                    }
                }
            }
        }
    }
}