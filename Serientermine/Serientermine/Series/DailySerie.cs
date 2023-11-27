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
    internal sealed class DailySerie : SerieBase
    {
        /// <summary>
        /// Liste der Tage in einer Woche
        /// </summary>
        public List<string> DayList { get; set; }

        public override SerieType Type => SerieType.Daily;

        public override string IntervallDescription => $"Jede(n) {Intervall}. Tag";

        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            List<string> dayList = DayList;
            int intervall = Intervall;

            //startdatum festlegen
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = checkedStart;


            if (dayList.Count <= 0)
            {
                while (current <= checkedEnd)
                {
                    yield return current;
                    current = current.AddDays(intervall);
                }
            }
            else
            {
                string dayOfWeekString = "";
                while (current <= checkedEnd)//wenn im zeitraum
                {
                    foreach (string str in dayList)
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
                    }
                    current = current.AddDays(intervall);// Tag
                }
            }
        }
    }
}
