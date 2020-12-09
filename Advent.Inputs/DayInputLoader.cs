using System.Collections.Generic;

namespace Advent.Inputs
{
    public class DayInputLoader
    {
        private static List<string> _data;

        public static List<string> GetData(string day)
        {
            if (_data != null) return _data;

            _data = FileExtensions.GetResource(day);
            return _data;
        }
    }
}