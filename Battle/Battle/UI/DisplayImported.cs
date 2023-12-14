using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battle.UI
{
    internal class DisplayImported
    {
        public static void WriteFromFile(string filePath, int x = 0, int y = 0)
        {
            // Definieren Sie den regulären Ausdruck, um den gewünschten Teil zu extrahieren
            Regex regex = new Regex(@"__(.+)\.txt");

            // Suche nach Übereinstimmungen im Eingabestring
            Match match = regex.Match(filePath);

            ConsoleColor color = Console.ForegroundColor;
            if (match.Success)
            {
                color = GetColorFromFile(match);
            }

            // Definieren Sie den relativen Pfad zur Textdatei
            string relativePath = $"..\\..\\UI\\DisplayTemplates\\{filePath}";

            // Kombinieren Sie den relativen Pfad mit dem aktuellen Arbeitsverzeichnis, um den absoluten Pfad zu erhalten
            filePath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            if (!File.Exists(filePath))
            {
                return;
            }

            Console.CursorVisible = false;
            try
            {
                // Den Inhalt der Textdatei lesen
                string[] lines = File.ReadAllLines(filePath);

                // Zeilen in der Konsole ausgeben
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    foreach (char c in lines[i])
                    {
                        if (c != ' ')
                        {
                            UI.ConsoleWriter.Color(c.ToString(), color);
                        }
                        else
                        {
                            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Fehler beim Lesen der Datei: {e.Message}");
            }
            Console.SetCursorPosition(0, 0);
        }
        private static ConsoleColor GetColorFromFile(Match match)
        {
            string colorName = match.Groups[1].Value;

            // Versuchen Sie, den gefundenen Teil in eine ConsoleColor-Variable zu parsen
            if (Enum.TryParse(colorName, true, out ConsoleColor consoleColor))
            {
                return consoleColor;
            }
            return ConsoleColor.White;
        }
    }
}
