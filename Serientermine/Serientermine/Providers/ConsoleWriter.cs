using Serientermine.Series;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Providers
{
    internal sealed class ConsoleWriter : IWriter
    {
        public void Write(ISerie serie, DateTime start, DateTime end)
        {
            ConsoleColor color;
            ConsoleColor darkColor;
            switch (serie.Type.ToString())
            {
                default:
                    color = ConsoleColor.Red;
                    darkColor = ConsoleColor.DarkRed;
                    break;
                case "Daily":
                    color = ConsoleColor.Cyan;
                    darkColor = ConsoleColor.DarkCyan;
                    break;
                case "Weekly":
                    color = ConsoleColor.Green;
                    darkColor = ConsoleColor.DarkGreen;
                    break;
                case "Monthly":
                    color = ConsoleColor.Yellow;
                    darkColor = ConsoleColor.DarkYellow;
                    break;
                case "Yearly":
                    color = ConsoleColor.Magenta;
                    darkColor = ConsoleColor.DarkMagenta;
                    break;
            }
            UI.ConsoleWriter.LineColor($"[{serie.Type}] ({serie.Name})", darkColor);
            UI.ConsoleWriter.Color($"Beginn der Serie: {serie.Begin:d}, End: {serie.End:d}. {serie.IntervallDescription}. Termine:");
            Console.WriteLine();
            int count = 0;
            int limit = serie.Limit;
            foreach (DateTime date in serie.GetDatesInRange(start, end))
            {
                UI.ConsoleWriter.Color($"|{date.ToString("dd.MM.yyyy")}| ", color);
                UI.ConsoleWriter.Color(date.DayOfWeek.ToString());
                Console.SetCursorPosition(25, Console.CursorTop);
                UI.ConsoleWriter.Color(date.ToString("MMMM"));
                Console.WriteLine();
                count++;
            }
            UI.ConsoleWriter.Color($"Stats: {count} Tage mit Termin.  Duration: {(end - start).TotalDays} Days. Limit={limit}");
            Console.WriteLine(); Console.WriteLine();
        }
    }
}
