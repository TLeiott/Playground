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
            UI.ConsoleWriter.LineColor($"[Daily] ({serie.Name})", ConsoleColor.DarkCyan);
            UI.ConsoleWriter.Color($"Beginn der Serie: {serie.Begin:d}, End: {serie.End:d}. {serie.IntervallDescription}. Termine:");
            Console.WriteLine();
            int count = 0;
            int limit = serie.Limit;
            foreach (DateTime date in serie.GetDatesInRange(start, end))
            {
                UI.ConsoleWriter.Color($"|{date.ToString("dd.MM.yyyy")}| ", ConsoleColor.Cyan);
                UI.ConsoleWriter.Color(date.DayOfWeek.ToString());
                Console.SetCursorPosition(25, Console.CursorTop);
                UI.ConsoleWriter.Color(date.ToString("MMMM"));
                UI.ConsoleWriter.Color(date.Month.ToString(), ConsoleColor.Red);
                Console.WriteLine();
                count++;
            }
            UI.ConsoleWriter.Color($"Stats: {count} Tage mit Termin.  Duration: {(end - start).TotalDays} Days. Limit={limit}");
            Console.WriteLine(); Console.WriteLine();
        }
    }
}
