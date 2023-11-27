﻿using Serientermine.Serientermine;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Series
{
    internal class MonthlySerie : SerieBase
    {
        public List<string> DayList { get; set; }

        public override SerieType Type => SerieType.Monthly;

        public override string IntervallDescription => $"Jede(n) {Intervall}. Monat";
        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            if (DayList == null || DayList.Count == 0)
                yield break;
            Console.WriteLine();
            List<string> dayList = DayList;
            int intervall = Intervall;

            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = checkedStart;
            UI.ConsoleWriter.LineColor($"{current} <= {checkedEnd}", ConsoleColor.Red);
            while (current <= checkedEnd)//wenn im zeitraum
            {
                string dayOfWeekString = "";
                string monthSaved = current.Month.ToString();
                while (current.Month.ToString() == monthSaved)
                {
                    foreach (string str in dayList)//für jeden Tag in der Liste
                    {
                        if (int.TryParse(str, out int dayDate)) // für jedes TagesDatum in der Liste
                        {
                            if (current.Day == dayDate)
                            {
                                yield return current;
                                break;
                            }
                        }
                        else
                        {
                            dayOfWeekString = current.DayOfWeek.ToString();
                            if (dayOfWeekString == str)
                            {
                                yield return current;
                                break;
                            }
                        }
                        // DEBUG Console.SetCursorPosition(0,Console.CursorTop-1); UI.ConsoleWriter.Color($"loop={loop}, str={str}, dayDate{dayDate}, dayOfWeekString={dayOfWeekString}, Current={current}, CurrentMonth={current.Month}, savedMonth={monthSaved}", ConsoleColor.Magenta); Console.SetCursorPosition(0, Console.CursorTop + 1);
                    }
                    current = current.AddDays(1);//+1 Tag

                }
                current = current.AddMonths(-1);
                current = current.AddMonths(intervall);
            }
        }
    }
}
