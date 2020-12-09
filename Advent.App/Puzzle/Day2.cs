using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Advent.App.Puzzle
{
    public class Day2
    {
        public static void PartOne()
        {
            var input = Advent.Inputs.Day2.Data;
            var passList = GetPasswords(input);
            var policy = new DayOnePolicy();
            
            var count = passList.Count(p => policy.IsValid(p));

            Console.WriteLine($"Number of correct passwords is: {count}");
        }

        public static void PartTwo()
        {
            var input = Advent.Inputs.Day2.Data;
            var passList = GetPasswords(input);
            var policy = new DayTwoPolicy();

            var count = passList.Count(p => policy.IsValid(p));

            Console.WriteLine($"Number of correct passwords is: {count}");
        }

        private static IEnumerable<PasswordPolicy> GetPasswords(IReadOnlyList<string> input)
        {
            foreach (var passInput in input)
            {
                var parts = passInput.Split(' ');
                if(parts.Length != 3) throw new InvalidEnumArgumentException(passInput);

                var (min, max) = GetBoundaries(parts[0]);
                var letter = GetLetter(parts[1]);
                var passwordToValidate = GetPassword(parts[2]);
                yield return new PasswordPolicy(min, max, letter, passwordToValidate);
            }
        }

        private static string GetPassword(string input)
        {
            return input;
        }

        private static string GetLetter(string input)
        {
            var letter = input.Substring(0, input.IndexOf(':'));
            return letter;
        }

        private static (int, int) GetBoundaries(string input)
        {
            var splitArray = input.Split('-');
            if(splitArray.Length != 2) throw new ArgumentException(input);

            var minString = splitArray[0];
            var maxString = splitArray[1];
            
            if(!int.TryParse(minString, out var min)) throw new InvalidEnumArgumentException(minString);
            if(!int.TryParse(maxString, out var max)) throw new InvalidEnumArgumentException(maxString);
            return (min, max);
        }
    }

    internal interface IPolicy
    {
        bool IsValid(PasswordPolicy passwordPolicy);
    }

    internal class DayOnePolicy : IPolicy
    {
        public bool IsValid(PasswordPolicy passwordPolicy)
        {
            var chars = new Dictionary<char, int>();
            foreach (var t in passwordPolicy.Password)
            {
                if(!chars.ContainsKey(t))
                    chars.Add(t, 0);

                chars[t]++;
            }

            if (chars.ContainsKey(passwordPolicy.Letter))
                return passwordPolicy.Val1 <= chars[passwordPolicy.Letter] && chars[passwordPolicy.Letter] <= passwordPolicy.Val2;

            return false;
        }
    }

    internal class DayTwoPolicy : IPolicy
    {
        public bool IsValid(PasswordPolicy passwordPolicy)
        {
            var passwordArray = passwordPolicy.Password.ToCharArray();
            if(passwordArray[passwordPolicy.Val1 - 1] == passwordPolicy.Letter) 
                return passwordArray[passwordPolicy.Val2 - 1] != passwordPolicy.Letter;
            
            return passwordArray[passwordPolicy.Val2 - 1] == passwordPolicy.Letter;
        }
    }

    internal class PasswordPolicy
    {
        public int Val1 { get; }
        public int Val2 { get; }
        public char Letter { get; }
        public string Password { get; }

        public PasswordPolicy(in int val1, in int val2, string letter, string password)
        {
            Val1 = val1;
            Val2 = val2;
            Letter = Convert.ToChar(letter);
            Password = password;
        }
    }
}