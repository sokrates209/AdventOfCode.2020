using System;
using System.Collections.Generic;

namespace Advent.App.Puzzle
{
    public class Day8
    {
        public static int PartOne(List<string> input)
        {
            var foundLoop = false;
            var accumulator = 0;
            var index = 0;
            var visitedIdx = new HashSet<int>();

            while (!foundLoop)
            {
                if (visitedIdx.Contains(index))
                {
                    break;
                }
                else
                    visitedIdx.Add(index);

                var (operationType, argument) = GetOperation(input[index]);
                switch (operationType)
                {
                    case OperationType.Acc:

                        accumulator += argument;
                        index++;

                        break;
                    case OperationType.Jmp:

                        index = index + argument;

                        break;
                    case OperationType.Nop:

                        index++;

                        break;
                }
            }

            Console.WriteLine($"Found loop, accumulator value: {accumulator}");
            return accumulator;
        }

        private static (OperationType, int) GetOperation(string line)
        {
            var parts = line.Split(' ');
            if (!Enum.TryParse(typeof(OperationType), parts[0], true, out var operation))
                throw new ArgumentException($"Invalid data: {line}");
            if (!int.TryParse(parts[1], out var argument))
                throw new ArgumentException($"Invalid argument data: {line}");
            return ((OperationType) operation, argument);
        }

        public static int PartTwo(List<string> input)
        {
            var accumulator = 0;
            var reachedEof = false;
            var i = 0;
            var replacedOperations = new HashSet<int>();

            while (!reachedEof)
            {
                Console.WriteLine($"Performing iteration: {i}");
                var index = 0;
                var exiting = false;
                var visitedIdx = new HashSet<int>();
                accumulator = 0;
                var operationReplaced = false;
                
                while (!exiting)
                {
                    if (visitedIdx.Count == input.Count)
                    {
                        Console.WriteLine($"Reached maximum number of indexes. Exiting...");
                        break;
                    }

                    if (visitedIdx.Contains(index))
                    {
                        Console.WriteLine($"Found infinite loop at index: {index}");
                        break;
                    }

                    visitedIdx.Add(index);

                    var (operationType, argument) = GetOperation(input[index]);

                    if (operationType == OperationType.Jmp && !replacedOperations.Contains(index) && !operationReplaced)
                    {
                        operationReplaced = true;
                        operationType = OperationType.Nop;
                        replacedOperations.Add(index);
                    }
                    else if (operationType == OperationType.Nop && !replacedOperations.Contains(index) && !operationReplaced)
                    {
                        operationReplaced = true;
                        operationType = OperationType.Jmp;
                        replacedOperations.Add(index);
                    }

                    switch (operationType)
                    {
                        case OperationType.Acc:

                            accumulator += argument;
                            index++;

                            break;
                        case OperationType.Jmp:

                            index = index + argument;

                            break;
                        case OperationType.Nop:

                            index++;

                            break;
                        case OperationType.Eof:
                            Console.WriteLine($"Reached End of Data. Exiting...");
                            reachedEof = true;
                            exiting = true;
                            break;
                        default:
                            exiting = true;
                            break;
                    }
                }

                i++;
            }

            Console.WriteLine($"Accumulator value: {accumulator}");
            return accumulator;
        }
    }

    internal enum OperationType
    {
        Nop,
        Acc,
        Jmp,
        Eof,
        None
    }
}