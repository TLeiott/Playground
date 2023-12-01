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

            //Leeres dayList hochsetzen
            if (dayList.Count == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    dayList.Add(current.DayOfWeek.ToString());
                    current = current.AddDays(1);
                }
            }
            current = Begin;

            int count = 0;//Counter für das Terminlimit

            while (current <= checkedEnd && count < Limit)//wenn im zeitraum
            {
                if (IsInRange(checkedStart, checkedEnd, current) && dayList.Contains(current.DayOfWeek.ToString()))
                {
                    yield return current;
                }
                if (dayList.Contains(current.DayOfWeek.ToString()))
                {
                    count++;
                }
                current = current.AddDays(intervall);// Tag
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
//Daily sollte neu geschrieben werden, wie der versuch limit zu integrieren gezeigt hat. In der aktuellen version von Daily sind noch funktionen drinnen, die nicht gebraucht werden. Beim neuschreiben muss sich dieses mal an den Kalender gehalten werden.    