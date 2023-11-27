using Serientermine.Serientermine;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Series
{
    internal sealed class WeeklySerie : SerieBase
    {
        public List<string> DayList { get; set; }

        public override SerieType Type => SerieType.Weekly;

        public override string IntervallDescription => $"Jede(n) {Intervall}. Woche";
        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            //leere angaben bei "Wochentage" herausfiltern
            if (DayList == null || DayList.Count == 0)
                yield break;
            List<string> dayList = DayList;
            int intervall = Intervall;
            Console.WriteLine();

            //Startdatum definieren
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = checkedStart;

            //auf den Letzten Montag zurückgehen um eine verschobene Wochenberechnung zu verhindern
            int daysUntilLastMonday = ((int)current.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            current = current.AddDays(-daysUntilLastMonday);

            int count = 0;//Counter für das Terminlimit

            while (current <= checkedEnd&&count<Limit)//wenn im zeitraum
            {
                string dayOfWeekString = "";
                string weekSaved = current.ToString();
                for (int i = 0; i < 7; i++)
                {
                    foreach (string str in dayList)//für jeden Tag in der Liste
                    {
                        if (int.TryParse(str, out int dayDate)) // für jedes TagesDatum in der Liste
                        {
                            if (current.Day == dayDate)
                            {
                                if (current >= checkedStart)
                                {
                                    yield return current;
                                    count++;
                                }
                                break;
                            }
                        }
                        else
                        {
                            dayOfWeekString = current.DayOfWeek.ToString();
                            if (dayOfWeekString == str)
                            {
                                if (current >= checkedStart)
                                {
                                    yield return current;
                                    count++;
                                }
                                break;
                            }
                        }
                        //Console.SetCursorPosition(0,Console.CursorTop-1); UI.ConsoleWriter.Color($"loop={loop}, str={str}, dayDate{dayDate}, dayOfWeekString={dayOfWeekString}, Current={current}, savedWeek={weekSaved}", ConsoleColor.Magenta); Console.SetCursorPosition(0, Console.CursorTop + 1);
                    }
                    current = current.AddDays(1);//+1 Tag

                }
                current = current.AddDays(-7);
                current = current.AddDays(7 * intervall);
            }

        }
    }
}
