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

            UI.ConsoleWriter.LineColor($"[Daily]", ConsoleColor.DarkCyan);
            UI.ConsoleWriter.Color($"Begin: {begin.ToString("dd.MM.yyyy")}, End: {end.ToString("dd.MM.yyyy")}. Jeden {intervallNummer}ten-Tag. Termine:");
            Console.WriteLine();
            foreach (DateTime date in dates)
            {
                UI.ConsoleWriter.Color($"|{date.ToString("dd.MM.yyyy")}| ",ConsoleColor.Cyan);
                UI.ConsoleWriter.Color(date.DayOfWeek.ToString());
                Console.SetCursorPosition(25, Console.CursorTop);
                UI.ConsoleWriter.Color(GetMonthName(date));
                Console.WriteLine();
            }
            UI.ConsoleWriter.Color($"Stats: {dates.Count} Tage mit Termin.  Duration: {(end-begin).TotalDays} Days");
            Console.WriteLine();Console.WriteLine();
        }
        private static string GetMonthName(DateTime date)
        {
            string name = "";
            switch (date.Month)
            {
                case 1: name = "Januar"; break;
                case 2: name = "Februar"; break;
                case 3: name = "März"; break;
                case 4: name = "April"; break;
                case 5: name = "Mai"; break;
                case 6: name = "Juni"; break;
                case 7: name = "Juli"; break;
                case 8: name = "August"; break;
                case 9: name = "September"; break;
                case 10: name = "Oktober"; break;
                case 11: name = "Movember"; break;
                case 12: name = "Dezember"; break;
            }
            return name;
        }
    }
}
