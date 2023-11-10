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
            List<string> dayList = serie.DayList;
            int intervallNummer = serie.IntervallNummer;

            if (endUnsure != null)
            {
                end = Convert.ToDateTime(endUnsure);
            }
            Console.WriteLine();
            if (serie.DayList.Count <= 0)
            {
                while (current <= end)
                {
                    dates.Add(current);
                    current = current.AddDays(intervallNummer);
                }
            }
            else
            {
                string dayOfWeekString = "";
                while (current <= end)//wenn im zeitraum
                {
                    foreach (string str in dayList)
                    {
                        if (int.TryParse(str, out int dayDate)) // für jedes TagesDatum in der Liste
                        {
                            if (current.Day == dayDate)
                            {
                                dates.Add(current);
                                break;
                            }
                        }
                        else
                        {
                            dayOfWeekString = current.DayOfWeek.ToString();
                            if (dayOfWeekString == str)
                            {
                                dates.Add(current);
                                break;
                            }
                        }
                    }
                    current = current.AddDays(intervallNummer);// Tag
                }
            }

            UI.ConsoleWriter.LineColor($"[Daily] ({serie.Name})", ConsoleColor.DarkCyan);
            UI.ConsoleWriter.Color($"Begin: {begin.ToString("dd.MM.yyyy")}, End: {end.ToString("dd.MM.yyyy")}. Jeden {intervallNummer}ten-Tag. Termine:");
            Console.WriteLine();
            int count=0;
            int limit=serie.Limit;
            foreach (DateTime date in dates)
            {
                if (limit == 0 || count < limit)
                {
                    UI.ConsoleWriter.Color($"|{date.ToString("dd.MM.yyyy")}| ", ConsoleColor.Cyan);
                    UI.ConsoleWriter.Color(date.DayOfWeek.ToString());
                    Console.SetCursorPosition(25, Console.CursorTop);
                    UI.ConsoleWriter.Color(GetMonthName(date));
                    Console.WriteLine();
                    count++;
                }
            }
            UI.ConsoleWriter.Color($"Stats: {dates.Count} Tage mit Termin.  Duration: {(end - begin).TotalDays} Days. Limit={limit}");
            Console.WriteLine(); Console.WriteLine();
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
