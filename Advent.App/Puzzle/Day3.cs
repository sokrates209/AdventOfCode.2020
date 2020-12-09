using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.App.Puzzle
{
    public class Day3
    {
        public static void PartOne()
        {
            var input = Inputs.Day3.Data;

            var map = GetMap(input);
            var treesCount = TraverseMap(map, 1, 3);
            
            Console.WriteLine($"Number of trees for part one: {treesCount}");
        }

        public static void PartTwo()
        {
            var input = Inputs.Day3.Data;
            var slopeData = new[]
            {
                (1, 1),
                (1, 3),
                (1, 5),
                (1, 7),
                (2, 1)
            };

            var map = GetMap(input);
            var treesCount = slopeData.Select(data => TraverseMap(map, data.Item1, data.Item2))
                .Aggregate<int, long>(1, (current, result) => current * result);


            Console.WriteLine($"Number of trees for part two: {treesCount}");
        }

        private static int TraverseMap(IReadOnlyList<Slope> map, int moveX, int moveY)
        {
            var trees = 0;
            var (x, y) = (0, 0);
            var extendedBy = 0;
            for (var i = x; i < map.Count; i += moveX)
            {
                var path = map[i];
                if (extendedBy > 0 && path.ExtendedBy == 0)
                    path.Extend(extendedBy);
                var position = path[y];
                if (position == '#')
                    trees++;
                y += moveY;
                extendedBy = path.ExtendedBy;
            }

            return trees;
        }

        private static List<Slope> GetMap(IEnumerable<string> input)
        {
            var result = new List<Slope>();
            foreach (var slope in input)
            {
                result.Add(new Slope(slope));
            }

            return result;
        }

        private class Slope
        {
            public Slope(string originalPath)
            {
                InfinitePath = OriginalPart = originalPath;
            }

            private string OriginalPart { get; }

            private string InfinitePath { get; set; }
            
            public int ExtendedBy { get; private set; }

            public char this[int i]
            {
                get
                {
                    if (InfinitePath.Length > i) return InfinitePath[i];
                    while (InfinitePath.Length <= i)
                    {
                        InfinitePath += OriginalPart;
                        ExtendedBy++;
                    }
                    return InfinitePath[i];
                }
            }

            public void Extend(in int extendedBy)
            {
                ExtendedBy = extendedBy;
                for (var i = 0; i < ExtendedBy; i++)
                {
                    InfinitePath += OriginalPart;
                }
            }
        }
    }
}