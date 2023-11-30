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
    public sealed class DailySerie : SerieBase
    {
        /// <summary>
        /// Liste der Tage in einer Woche
        /// </summary>
        public List<string> DayList { get; set; } = new();

        public override SerieType Type => SerieType.Daily;

        public override string IntervallDescription => $"Jede(n) {Intervall}. Tag";

        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            List<string> dayList = DayList;
            int intervall = Intervall;

            //startdatum festlegen
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = checkedStart;

            //Leeres Limit hochsetzen
            if (Limit == 0)
            {
                Limit = 999999999;
            }

            int count = 0;//Counter für das Terminlimit

            if (dayList.Count <= 0)
            {
                while (current <= checkedEnd && count < Limit)
                {
                    if (IsInRange(checkedStart, checkedEnd, current))
                    {
                        yield return current;
                    }
                    count++;
                    current = current.AddDays(intervall);
                }
            }
            else
            {
                string dayOfWeekString = "";
                while (current <= checkedEnd && count < Limit)//wenn im zeitraum
                {
                    foreach (string str in dayList)
                    {
                        if (int.TryParse(str, out int dayDate)) // für jedes TagesDatum in der Liste
                        {
                            if (current.Day == dayDate)
                            {
                                if (IsInRange(checkedStart, checkedEnd, current))
                                {
                                    yield return current;
                                }
                                count++;
                                break;
                            }
                        }
                        else
                        {
                            dayOfWeekString = current.DayOfWeek.ToString();
                            if (dayOfWeekString == str)
                            {
                                if (IsInRange(checkedStart, checkedEnd, current))
                                {
                                    yield return current;
                                }
                                count++;
                                break;
                            }
                        }
                    }
                    current = current.AddDays(intervall);// Tag
                }
            }
        }
        private bool IsInRange(DateTime checkedStart, DateTime checkedEnd, DateTime current)
        {
            if (current >= checkedStart && current <= checkedEnd)
            {
                return true;
            }
            return false;
        }
    }
}
//Daily sollte neu geschrieben werden wie der versuch limit zu integrieren gezeigt hat. In der aktuellen version von Daily sind noch funktionen drinnen, die nicht gebraucht werden. Beim neuschreiben muss sich dieses mal an den Kalender gehalten werden.    