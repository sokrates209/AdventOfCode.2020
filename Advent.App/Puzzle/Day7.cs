using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent.App.Puzzle
{
    public class Day7
    {
        private const string Pattern = @"(?<count>\d*)";
        private static readonly Regex Rgx = new Regex(Pattern);
        private const string BagToLookFor = "shiny gold";
        
        public static int PartOne(IEnumerable<string> input)
        {
            var bags = new List<Bag>();
            foreach (var line in input)
            {
                var bagColor = GetMainBagColor(line);
                var foundBag = bags.SingleOrDefault(b => b.BagColor.Equals(bagColor));
                if (foundBag == null)
                {
                    foundBag = new Bag(bagColor);
                    bags.Add(foundBag);
                }

                foreach (var contentBag in GetContentBags(line))
                {
                    var foundContentBag = bags.SingleOrDefault(b => b.BagColor.Equals(contentBag.Item1));
                    if (foundContentBag == null)
                    {
                        foundContentBag = new Bag(contentBag.Item1);
                        bags.Add(foundContentBag);
                    }

                    foundBag.AddContentBag(foundContentBag, contentBag.Item2);
                }
            }

            var bagsCount = bags.Count(b => b.HasBag(BagToLookFor)) - 1;
            
            Console.WriteLine($"There is: {bagsCount} that can contain at least one: {BagToLookFor}");
            return bagsCount;
        }

        private static IEnumerable<(string, int)> GetContentBags(string line)
        {
            var bagParts = line.Split("contain");
            if(bagParts == null || bagParts.Length != 2)
                throw new InvalidOperationException("Wrong data.");

            var contentParts = bagParts[1]
                .Replace("bags", string.Empty)
                .Replace("bag", string.Empty)
                .Replace(".", string.Empty)
                .Trim();
            var contentArray = contentParts.Split(',');

            foreach (var content in contentArray)
            {
                if(content.Contains("no other"))
                    continue;
                
                var match = Rgx.Match(content.Trim());
                if(!match.Success)
                    continue;

                var bagCount = match.Groups.Values.SingleOrDefault(v => v.Name == "count")?.Value;
                if(string.IsNullOrEmpty(bagCount) || !int.TryParse(bagCount, out var count))
                    throw new ArgumentException($"Invalid data: {content}");

                var bagColor = content.Replace(bagCount, string.Empty).Trim();
                yield return (bagColor, count);
            }
        }

        private static string GetMainBagColor(string line)
        {
            var bagParts = line.Split("contain");
            if(bagParts == null || bagParts.Length != 2)
                throw new InvalidOperationException("Wrong data.");

            var bagColor = bagParts[0].Replace("bags", string.Empty).Trim();

            return bagColor;
        }

        public static int PartTwo(IEnumerable<string> input)
        {
            var bags = new List<Bag>();
            foreach (var line in input)
            {
                var bagColor = GetMainBagColor(line);
                var foundBag = bags.SingleOrDefault(b => b.BagColor.Equals(bagColor));
                if (foundBag == null)
                {
                    foundBag = new Bag(bagColor);
                    bags.Add(foundBag);
                }

                foreach (var (bag, bagCount) in GetContentBags(line))
                {
                    var foundContentBag = bags.SingleOrDefault(b => b.BagColor.Equals(bag));
                    if (foundContentBag == null)
                    {
                        foundContentBag = new Bag(bag);
                        bags.Add(foundContentBag);
                    }

                    foundBag.AddContentBag(foundContentBag, bagCount);
                }
            }

            var bagsCount = GetBagsCountFor(bags.Single(b => b.BagColor.Equals(BagToLookFor)));
            
            Console.WriteLine($"Your {BagToLookFor} has to carry: {bagsCount} other bags.");
            return bagsCount;
        }

        private static int GetBagsCountFor(Bag bag)
        {
            var sum = 0;
            if (bag.CountOfCarryingBags == 0) return sum;

            return bag.ContentBags.Aggregate(sum,
                (current, contentBag) => current + contentBag.Value +
                                         contentBag.Value * GetBagsCountFor(contentBag.Key));
        }
    }

    internal class Bag : IEquatable<Bag>
    {
        public Bag(string bagColor)
        {
            BagColor = bagColor;
            ContentBags = new Dictionary<Bag, int>();
        }

        public string BagColor { get; }
        
        public Dictionary<Bag, int> ContentBags { get; }

        public int CountOfCarryingBags => ContentBags.Any() ? ContentBags.Sum(b => b.Value) : 0;

        public bool Equals(Bag other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return BagColor == other.BagColor && Equals(ContentBags, other.ContentBags);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Bag) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BagColor, ContentBags);
        }

        public void AddContentBag(Bag bag, int bagsCount)
        {
            if(!ContentBags.ContainsKey(bag))
                ContentBags.Add(bag, 0);

            ContentBags[bag] = bagsCount;
        }

        public bool HasBag(string bagColor)
        {
            return BagColor == bagColor || ContentBags.Any(b => b.Key.HasBag(bagColor));
        }
    }
}