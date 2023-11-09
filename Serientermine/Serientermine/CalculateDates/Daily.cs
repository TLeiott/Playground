using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.CalculateDates
{
    internal class Daily
    {
        public static void GetDates(Serientermine.Serie serie, DateTime end)
        {
            DateTime begin = serie.Begin;
            DateTime? endUnsure = serie.End;
            DateTime current = begin;
            List<DateTime> dates = new List<DateTime>();
            int intervallNummer = serie.IntervallNummer;
            string DayOfWeek;

            if (endUnsure!=null)
            {
                end = Convert.ToDateTime(endUnsure);
            }
            Console.WriteLine();
            while (current <= end)
            {
                dates.Add(current);
                current = current.AddDays(intervallNummer);
            }

            UI.ConsoleWriter.LineColor($"[Daily]", ConsoleColor.DarkGreen);
            UI.ConsoleWriter.Color($"Begin: {begin.ToString("dd.MM.yyyy")}, End: {end.ToString("dd.MM.yyyy")}. Termine:");
            Console.WriteLine();
            foreach (DateTime date in dates)
            {
                UI.ConsoleWriter.Color($"|{date.ToString("dd.MM.yyyy")} | ",ConsoleColor.Green);
                UI.ConsoleWriter.Color(date.DayOfWeek.ToString());
                Console.WriteLine();
            }
            UI.ConsoleWriter.Color($"Stats: {dates.Count} Tage mit Termin.  Duration: {(end-begin).TotalDays} Days");
            Console.WriteLine();Console.WriteLine();
        }
    }
}
