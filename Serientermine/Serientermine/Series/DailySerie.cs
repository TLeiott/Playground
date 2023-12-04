using System;
using System.Collections.Generic;

namespace Serientermine.Series
{
    /// <summary> Serie für tägliche Termine </summary>
    public sealed class DailySerie : SerieBase
    {
        /// <summary> Liste der Tage in einer Woche </summary>
        public List<string> DayList { get; set; } = new();

        /// <inheritdoc />
        public override SerieType Type => SerieType.Daily;

        /// <inheritdoc />
        public override string IntervallDescription => $"Jede(n) {Intervall}. Tag";

        /// <inheritdoc />
        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            var dayList = DayList;
            var intervall = Intervall;

            // Startdatum festlegen
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = checkedStart;

            // Leeres Limit hochsetzen
            if (Limit == 0)
            {
                Limit = 999999999;  // BUG: Wenn kein Limit gesetzt ist, dann wird davon ausgegangen, dass die Serie nie endet
            }

            //Leeres dayList hochsetzen
            if (dayList.Count == 0)
            {
                for (var i = 0; i < 7; i++)
                {
                    dayList.Add(current.DayOfWeek.ToString());
                    current = current.AddDays(1);
                }
            }
            current = Begin;

            var count = 0;//Counter für das Terminlimit

            // BUG: Das Limit muss im Zweifel um die bereits stattgefundenen Termine reduziert werden
            while (current <= checkedEnd && count < Limit) //wenn im zeitraum
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
        private static bool IsInRange(DateTime checkedStart, DateTime checkedEnd, DateTime current) 
            => current >= checkedStart && current <= checkedEnd;
    }
}
//Daily sollte neu geschrieben werden, wie der versuch limit zu integrieren gezeigt hat. In der aktuellen version von Daily sind noch funktionen drinnen, die nicht gebraucht werden. Beim neuschreiben muss sich dieses mal an den Kalender gehalten werden.    