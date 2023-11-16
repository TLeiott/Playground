using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Serientermine.CalculateDates
{
    internal class Yearly
    {
        /// <summary>
        /// Yearly Calculating system
        /// </summary>
        /// <param name="serie">Die Haubt Klasse "Serie"</param>
        /// <param name="end">das maximale ende als Limitierung</param>
        public static void GetDates(Serientermine.Serie serie, DateTime end)
        {
            DateTime begin = serie.Begin;
            DateTime? endUnsure = serie.End;
            DateTime current = begin;
            List<DateTime> dates = new List<DateTime>();
            List<string> dayList = serie.DayList;
            List<int> weekdayList = serie.WeekdayList;
            List<int> monthsList = serie.MonthsList;
            int intervall = serie.Intervall;

            //switch ((dayList, ListeGefüllt(weekdayList), ListeGefüllt(monthsList)))
            //{
            //    case (false, false, false):
            //        while (current <= end)
            //                {
            //                    dates.Add(current);
            //                    current = current.AddYears(intervall);
            //                }
            //            break;
            //}

            //if (endUnsure != null)
            //{
            //    end = Convert.ToDateTime(endUnsure);
            //}
            //Console.WriteLine();
            //if (dayList.Count <= 0 && weekdayList.Count <= 0)
            //{
            //    while (current <= end)
            //    {
            //        CorrectDate(current, begin, end, dayList);
            //        dates.Add(current);
            //        current = current.AddMonths(intervall * 12);
            //    }
            //}
            //else if (dayList.Count! <= 0 &&  weekdayList.Count <= 0)
            //{
            //
            //}

            UI.ConsoleWriter.LineColor($"[Monthly] ({serie.Name})", ConsoleColor.DarkMagenta);
            UI.ConsoleWriter.Color($"Begin: {begin.ToString("dd.MM.yyyy")}, End: {end.ToString("dd.MM.yyyy")}. Jeden {intervall}ten-Monat. Termine:");
            Console.WriteLine();
            int count = 0;
            int limit = serie.Limit;
            foreach (DateTime date in dates)
            {
                if (limit == 0 || count < limit)
                {
                    UI.ConsoleWriter.Color($"|{date.ToString("dd.MM.yyyy")}| ", ConsoleColor.Magenta);
                    UI.ConsoleWriter.Color(date.DayOfWeek.ToString());
                    Console.SetCursorPosition(25, Console.CursorTop);
                    UI.ConsoleWriter.Color(GetMonthName(date));
                    Console.WriteLine();
                    count++;
                }
            }
            UI.ConsoleWriter.Color($"Stats: {dates.Count} Tage mit Termin.  Duration: {(end - begin).TotalDays} Days. Limit= {limit}");
            Console.WriteLine(); Console.WriteLine();
        }
        private static string GetMonthName(DateTime date)
        {
            return date.Month switch
            {
                1 => "Januar",
                2 => "Februar",
                3 => "März",
                4 => "April",
                5 => "Mai",
                6 => "Juni",
                7 => "Juli",
                8 => "August",
                9 => "September",
                10 => "Oktober",
                11 => "Movember",
                12 => "Dezember",
                _ => string.Empty,
            };
        }
        private static DateTime CorrectDate(DateTime current, DateTime start, DateTime end, List<String> dayList)
        {
            return current;
        }
        private static bool ListeGefüllt<T>(List<T> liste)
        {
            return liste.Count > 0;
        }
    }
}
