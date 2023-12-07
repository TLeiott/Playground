using System;
using System.Collections.Generic;

namespace Serientermine.Series
{
    /// <summary> Serie für tägliche Termine </summary>
    public sealed class DailySerie : SerieBase
    {
        /// <inheritdoc />
        public override SerieType Type => SerieType.Daily;

        /// <inheritdoc />
        public override string IntervallDescription => $"Jede(n) {Intervall}. Tag";

        /// <inheritdoc />
        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            var intervall = Intervall;

            // Startdatum festlegen
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = checkedStart;
            current = Begin;

            var count = 0;//Counter für das Terminlimit

            while (current <= checkedEnd && (count < Limit || Limit == 0)) //wenn im zeitraum
            {
                if (IsInRange(checkedStart, checkedEnd, current))
                {
                    yield return current;
                }
                
                count++;
                current = current.AddDays(intervall);// Tag
            }
        }
        private static bool IsInRange(DateTime checkedStart, DateTime checkedEnd, DateTime current) 
            => current >= checkedStart && current <= checkedEnd;
    }
}
//Daily sollte neu geschrieben werden, wie der versuch limit zu integrieren gezeigt hat. In der aktuellen version von Daily sind noch funktionen drinnen, die nicht gebraucht werden. Beim neuschreiben muss sich dieses mal an den Kalender gehalten werden.    