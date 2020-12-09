using System.Collections.Generic;
using NUnit.Framework;

namespace Advent.UnitTests
{
    public class PuzzleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Day9_PartOne()
        {
            var result = Advent.App.Puzzle.Day9.PartOne(new List<string>()
            {
                "35",
                "20",
                "15",
                "25",
                "47",
                "40",
                "62",
                "55",
                "65",
                "95",
                "102",
                "117",
                "150",
                "182",
                "127",
                "219",
                "299",
                "277",
                "309",
                "576"
            }, 5);

            Assert.AreEqual(127, result);
        }

        [Test]
        public void Day9_PartTwo()
        {
            var result = App.Puzzle.Day9.PartTwo(new List<string>()
            {
                "35",
                "20",
                "15",
                "25",
                "47",
                "40",
                "62",
                "55",
                "65",
                "95",
                "102",
                "117",
                "150",
                "182",
                "127",
                "219",
                "299",
                "277",
                "309",
                "576"
            }, 127);

            Assert.AreEqual(62, result);
        }

        [Test]
        public void Day5_PartOne()
        {
            Advent.App.Puzzle.Day5.PartOne(new List<string>() {"FBFBBFFRLR"});
        }

        [Test]
        public void Day6_PartOne()
        {
            Advent.App.Puzzle.Day6.PartOne(new List<string>()
                {"abc", "", "a", "b", "c", "", "ab", "ac", "", "a", "a", "a", "a", "", "b"});
        }

        [Test]
        public void Day6_PartTwo()
        {
            Advent.App.Puzzle.Day6.PartTwo(new List<string>()
                {"abc", "", "a", "b", "c", "", "ab", "ac", "", "a", "a", "a", "a", "", "b"});
        }

        [Test]
        public void Day7_PartOne()
        {
            var result = App.Puzzle.Day7.PartOne(new List<string>()
            {
                "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                "bright white bags contain 1 shiny gold bag.",
                "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                "faded blue bags contain no other bags.",
                "dotted black bags contain no other bags."
            });

            Assert.AreEqual(4, result);
        }

        [Test]
        public void Day7_PartTwo_ExampleOne()
        {
            var result = App.Puzzle.Day7.PartTwo(new List<string>()
            {
                "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                "bright white bags contain 1 shiny gold bag.",
                "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                "faded blue bags contain no other bags.",
                "dotted black bags contain no other bags."
            });

            Assert.AreEqual(32, result);
        }

        [Test]
        public void Day7_PartTwo_ExampleTwo()
        {
            var result = App.Puzzle.Day7.PartTwo(new List<string>()
            {
                "shiny gold bags contain 2 dark red bags.",
                "dark red bags contain 2 dark orange bags.",
                "dark orange bags contain 2 dark yellow bags.",
                "dark yellow bags contain 2 dark green bags.",
                "dark green bags contain 2 dark blue bags.",
                "dark blue bags contain 2 dark violet bags.",
                "dark violet bags contain no other bags."
            });

            Assert.AreEqual(126, result);
        }

        [Test]
        public void Day8_PartOne()
        {
            var result = App.Puzzle.Day8.PartOne(new List<string>()
            {
                "nop +0",
                "acc +1",
                "jmp +4",
                "acc +3",
                "jmp -3",
                "acc -99",
                "acc +1",
                "jmp -4",
                "acc +6"
            });

            Assert.AreEqual(5, result);
        }

        [Test]
        public void Day8_PartTwo()
        {
            var result = App.Puzzle.Day8.PartTwo(new List<string>()
            {
                "nop +0",
                "acc +1",
                "jmp +4",
                "acc +3",
                "jmp -3",
                "acc -99",
                "acc +1",
                "jmp -4",
                "acc +6",
                "eof 0"
            });

            Assert.AreEqual(8, result);
        }
    }
}