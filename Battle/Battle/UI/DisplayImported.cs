using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.UI
{
    internal class DisplayImported
    {
        public static void WriteFromFile(string filePath)
        {
            Console.CursorVisible = false;
            try
            {
                // Den Inhalt der Textdatei lesen
                string[] lines = File.ReadAllLines(filePath);

                // Zeilen in der Konsole ausgeben
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }

                // Benutzer dazu auffordern, Enter zu drücken
                Console.WriteLine("\nDrücken Sie Enter, um fortzufahren...");

                // Warten, bis Enter gedrückt wird
                Console.ReadLine();

                // Löschen des Konsolenfensters
                Console.Clear();

                // Zeilen in der Konsole mit SetCursorPosition ausgeben
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(lines[i]);
                }

                // Benutzer dazu auffordern, Enter zu drücken
                Console.WriteLine("\nDrücken Sie Enter, um das Programm zu beenden.");
                Console.ReadLine();
            }
            catch (IOException e)
            {
                Console.WriteLine($"Fehler beim Lesen der Datei: {e.Message}");
            }
            Console.SetWindowPosition(0,0);
            Console.SetCursorPosition(0,0);
        }
    }
}
