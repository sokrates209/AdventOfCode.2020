using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Advent.Inputs
{
    public class FileExtensions
    {
        public static List<string> GetResource(string resourceName)
        {
            var data = new List<string>();
            
            var assembly = Assembly.GetAssembly(typeof(FileExtensions));
            
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    data.Add(reader.ReadLine());
                }
            }
            
            return data;
        }
    }
}