using System;
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
        public static List<(int, int)> Load(List<(int, int)> list)
        {
            if (!File.Exists("C:\\Temp\\MyFile.csv"))
            {
                throw new FileNotFoundException("Datei nicht gefunden.");
            }

            var lines = File.ReadAllLines("C:\\Temp\\MyFile.csv");
            var parsedValues = lines.Select(line =>
            {
                var parts = line.Split(';');
                if (parts.Length != 2 || !int.TryParse(parts[0], out int item1) || !int.TryParse(parts[1], out int item2))
                {
                    throw new FormatException("Ungültiges Dateiformat.");
                }
                return (item1, item2);
            }).ToList();

            list.AddRange(parsedValues);
            return list;
        }
    }
}
