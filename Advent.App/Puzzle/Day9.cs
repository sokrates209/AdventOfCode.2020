using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.App.Puzzle
{
    public class Day9
    {
        public static long PartOne(IEnumerable<string> input, int preamble)
        {
            var numbers = input.Select(long.Parse).ToList();
            long invalidNumber = 0;

            for (var i = preamble; i < numbers.Count; i++)
            {
                var number = numbers[i];
                if (!IsValid(number, numbers.Skip(i - preamble).Take(preamble).ToList()))
                {
                    invalidNumber = number;
                    break;
                }
            }

            Console.WriteLine($"Invalid number: {invalidNumber}");
            return invalidNumber;
        }

        private static bool IsValid(in long number, ICollection<long> numbers)
        {
            foreach (var n in numbers)
            {
                var part = number - n;
                if (numbers.Contains(part) && part * 2 != number)
                    return true;
            }

            return false;
        }

        public static long PartTwo(IEnumerable<string> input, long invalidNumber)
        {
            var numbers = input.Select(long.Parse).Where(n => n <= invalidNumber).ToList();
            var partialSet = FindPartialSet(invalidNumber, numbers);
            if (!partialSet.Any())
                throw new InvalidOperationException();

            var min = partialSet.Min();
            var max = partialSet.Max();

            var result = min + max;
            Console.WriteLine($"Encryption weakness is: {result}");
            return result;
        }

        private static List<long> FindPartialSet(in long targetSum, IReadOnlyList<long> numbersSet)
        {
            for (var i = 0; i < numbersSet.Count; i++)
            {
                var idx = i + 1;
                long sum = 0;
                var partialSet = new List<long>();
                while (true)
                {
                    if (sum == targetSum)
                    {
                        return partialSet;
                    }

                    if (sum > targetSum)
                        break;
                    
                    partialSet.Add(numbersSet[idx]);

                    sum = sum + numbersSet[idx];
                    idx++;
                }
            }
            
            return new List<long>();
        }
    }
}