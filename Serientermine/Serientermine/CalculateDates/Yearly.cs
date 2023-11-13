﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Serientermine.CalculateDates
{
    internal class Yearly
    {
        public static void GetDates(Serientermine.Serie serie, DateTime end)
        {
            DateTime begin = serie.Begin;
            DateTime? endUnsure = serie.End;
            DateTime current = begin;
            List<DateTime> dates = new List<DateTime>();
            List<string> dayList = serie.DayList;
            int intervall = serie.Intervall;

            if (endUnsure != null)
            {
                end = Convert.ToDateTime(endUnsure);
            }
            Console.WriteLine();
            if (serie.DayList.Count <= 0)
            {
                while (current <= end)
                {
                    CorrectDate(current, begin, end, dayList);
                    dates.Add(current);
                    current = current.AddMonths(intervall*12);
                }
            }
            else
            {
                while (current <= end)//wenn im zeitraum
                {
                    string dayOfWeekString = "";
                    bool loop = true;
                    string monthSaved = current.Month.ToString();
                    while (current.Month.ToString() == monthSaved)
                    {
                        foreach (string str in dayList)//für jeden Tag in der Liste
                        {
                            if (int.TryParse(str, out int dayDate)) // für jedes TagesDatum in der Liste
                            {
                                if (current.Day == dayDate)
                                {
                                    loop = false;
                                    dates.Add(current);
                                }
                            }
                            else
                            {
                                dayOfWeekString = current.DayOfWeek.ToString();
                                if (dayOfWeekString == str)
                                {
                                    loop = false;
                                    dates.Add(current);
                                }
                            }
                            /// DEBUG Console.SetCursorPosition(0,Console.CursorTop-1); UI.ConsoleWriter.Color($"loop={loop}, str={str}, dayDate{dayDate}, dayOfWeekString={dayOfWeekString}, Current={current}, CurrentMonth={current.Month}, savedMonth={monthSaved}", ConsoleColor.Magenta); Console.SetCursorPosition(0, Console.CursorTop + 1);
                        }
                        current = current.AddDays(1);//+1 Tag

                    }
                    current = current.AddMonths(-1);
                    current = current.AddMonths(intervall);
                }
            }

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
        private static DateTime CorrectDate(DateTime current, DateTime start, DateTime end, List<String> dayList)
        {
            return current;
        }
    }
}
