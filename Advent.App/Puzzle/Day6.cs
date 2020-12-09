using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.App.Puzzle
{
    public class Day6
    {
        public static void PartOne(List<string> input)
        {
            var groups = new List<PassengerGroup>();
            var currentGroup = new PassengerGroup();
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line) || line.Length == 0)
                {
                    groups.Add(currentGroup);
                    currentGroup = new PassengerGroup();
                    continue;
                }

                currentGroup.AddPassengerAnswers(line);
            }
            
            groups.Add(currentGroup);

            var sumOfCorrectAnswers = groups.Sum(c => c.NumberOfCorrectAnswers);
            
            Console.WriteLine($"Sum of all correct answers is: {sumOfCorrectAnswers}");
        }

        public static void PartTwo(List<string> input)
        {
            var groups = new List<PassengerGroup>();
            var currentGroup = new PassengerGroup();
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line) || line.Length == 0)
                {
                    groups.Add(currentGroup);
                    currentGroup = new PassengerGroup();
                    continue;
                }

                currentGroup.AddPassengerAnswers(line);
            }
            
            groups.Add(currentGroup);

            var sumOfCorrectAnswers = groups.Sum(p => p.NumberOfAnswersThatAllPassengersSayYes);
            
            Console.WriteLine($"Sum of all correct answers is: {sumOfCorrectAnswers}");
        }
    }

    public class PassengerGroup
    {
        public PassengerGroup()
        {
            _correctAnswers = new Dictionary<char, int>();
        }

        private readonly Dictionary<char, int> _correctAnswers;

        public int NumberOfAnswersThatAllPassengersSayYes =>
            _correctAnswers.Count(z => z.Value == NumberOfPassengersInTheGroup);
        
        public int NumberOfCorrectAnswers => _correctAnswers.Count;
        public void AddPassengerAnswers(string line)
        {
            NumberOfPassengersInTheGroup++;
            foreach (var answer in line.ToCharArray())
            {
                if (_correctAnswers.ContainsKey(answer))
                    _correctAnswers[answer] += 1;
                else
                    _correctAnswers.Add(answer, 1);
            }
        }

        public int NumberOfPassengersInTheGroup { get; set; }
    }
}