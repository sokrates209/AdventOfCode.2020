using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent.App.Puzzle
{
    public class Day4
    {
        public static void PartOne()
        {
            var passList = Inputs.Day4.Data;
            var tempPassData = new List<string>();
            var passports = new List<Passport>();

            foreach (var line in passList)
            {
                if (string.IsNullOrEmpty(line) || line.Length == 0)
                {
                    if (!tempPassData.Any()) continue;
                    
                    passports.Add(new Passport(tempPassData));
                    tempPassData.Clear();
                }
                else
                    tempPassData.Add(line);
            }

            var validPassports = passports.Count(p => p.IsValid_PartOne);
            Console.WriteLine($"Number of valid passports for part one: {validPassports}");
        }
        
        public static void PartTwo()
        {
            var passList = Inputs.Day4.Data;
            var tempPassData = new List<string>();
            var passports = new List<Passport>();

            foreach (var line in passList)
            {
                if (string.IsNullOrEmpty(line) || line.Length == 0)
                {
                    if (!tempPassData.Any()) continue;
                    
                    passports.Add(new Passport(tempPassData));
                    tempPassData.Clear();
                }
                else
                    tempPassData.Add(line);
            }

            var validPassports = passports.Count(p => p.IsValid_PartTwo());
            Console.WriteLine($"Number of valid passports for part two: {validPassports}");
        }
    }

    class Passport
    {
        public int? BirthYear { get; set; }
        private const string BirthYearKey = "byr";
        
        public int? IssueYear { get; set; }
        private const string IssueYearKey = "iyr";
        
        public int? ExpirationYear { get; set; }
        private const string ExpirationYearKey = "eyr";
        
        public Height Height { get; set; }
        private const string HeightKey = "hgt";
        
        public string HairColor { get; set; }
        private const string HairColorKey = "hcl";
        
        public string EyeColor { get; set; }
        private const string EyeColorKey = "ecl";
        
        public string PassportId { get; set; }
        private const string PassportIdKey = "pid";
        
        public int? CountryId { get; set; }
        private const string CountryIdKey = "cid";
        
        public Passport(List<string> passportData)
        {
            SetData(passportData);
            ColorCheck = new ColorConverter();
        }

        private ColorConverter ColorCheck { get; }

        public bool IsValid_PartOne => BirthYear.HasValue && 
                               IssueYear.HasValue && 
                               ExpirationYear.HasValue && 
                               Height?.HeightValue != null && 
                               !string.IsNullOrEmpty(HairColor) && 
                               !string.IsNullOrEmpty(EyeColor) && 
                               !string.IsNullOrEmpty(PassportId);

        public bool IsValid_PartTwo()
        {
            if (!IsValid_PartOne) return false;
            if (BirthYear.Value < 1920 || BirthYear.Value > 2002) return false;
            if (IssueYear.Value < 2010 || IssueYear.Value > 2020) return false;
            if (ExpirationYear.Value < 2020 || ExpirationYear.Value > 2030) return false;
            if(string.IsNullOrEmpty(Height.Unit)) return false;
            if (Height.Unit.Equals("cm", StringComparison.InvariantCultureIgnoreCase) &&
                (Height.HeightValue.Value < 150 || Height.HeightValue.Value > 193)) return false;
            if (Height.Unit.Equals("in", StringComparison.InvariantCultureIgnoreCase) &&
                (Height.HeightValue.Value < 59 || Height.HeightValue.Value > 76)) return false;
            if (!ColorCheck.IsValid(HairColor)) return false;
            if (!_validEyeColors.Contains(EyeColor)) return false;

            var passportArray = PassportId.ToCharArray();
            if (passportArray.Length != 9) return false;
            foreach (var c in passportArray)
            {
                if (!int.TryParse(c.ToString(), out var passDigit)) return false;
            }

            return true;
        }
        
        private readonly List<string> _validEyeColors = new List<string>()
        {
            "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
        };
            

        private void SetData(IEnumerable<string> passportData)
        {
            foreach (var line in passportData)
            {
                var lineData = line.Split(' ');
                foreach (var data in lineData)
                {
                    var keyValue = data.Split(':');
                    if(!keyValue.Any()) throw new ArgumentException("Missing key or value.");
                    
                    SetPassportKey(keyValue[0], keyValue[1]);
                }
            }
        }

        private void SetPassportKey(string key, string value)
        {
            switch (key)
            {
                case BirthYearKey:
                    if(!int.TryParse(value, out var year)) throw new ArgumentException($"Birth year has a wrong value: {value}");
                    BirthYear = year;
                    break;
                case IssueYearKey:
                    if(!int.TryParse(value, out var issue)) throw new ArgumentException($"Issue year has a wrong value: {value}");
                    IssueYear = issue;
                    break;
                case ExpirationYearKey:
                    if(!int.TryParse(value, out var exp))throw new ArgumentException($"Expiration year has a wrong value: {value}");
                    ExpirationYear = exp;
                    break;
                case HeightKey:
                    Height = new Height(value);
                    break;
                case HairColorKey:
                    HairColor = value;
                    break;
                case EyeColorKey:
                    EyeColor = value;
                    break;
                case PassportIdKey:
                    PassportId = value;
                    break;
                case CountryIdKey:
                    if(!int.TryParse(value, out var cid)) throw new ArgumentException($"Country id has a wrong value: {value}");
                    CountryId = cid;
                    break;
            }
        }
    }

    internal class Height
    {
        private const string Pattern = @"(?<value>\d*)(?<unit>\D*)";
        private readonly Regex _rgx = new Regex(Pattern);
        public Height(string data)
        {
            var groups = _rgx.Match(data);
            if (!groups.Success) return;
            
            var heightValue = groups.Groups.Values.SingleOrDefault(v => v.Name == "value")?.Value;
            HeightValue = int.TryParse(heightValue, out var height) ? height : (int?)null;
            var unitValue = groups.Groups.Values.SingleOrDefault(v => v.Name == "unit")?.Value;
            Unit = unitValue;
        }

        public string Unit { get; }

        public int? HeightValue { get; }
    }
}