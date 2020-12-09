using System.Collections.Generic;

namespace Advent.Inputs
{
    public class Day6
    {
        private static IEnumerable<string> _data;

        public static IEnumerable<string> Data
        {
            get
            {
                if (_data != null)
                    return _data;
                _data = GetData();
                return _data;
            }
        }

        private static IEnumerable<string> GetData()
        {
            return FileExtensions.GetResource("Advent.Inputs.Files.Day6.txt");
        }
    }
}