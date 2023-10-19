using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Coordinatensystem.CMD_Line
{
    internal class SaveAsFile
    {
        public static void Save(List<(int, int)> values)
        {
            File.WriteAllLines("C:\\Temp\\MyFile.csv", values.Select(x => $"{x.Item1};{x.Item2}").ToArray());
        }
    }
}
