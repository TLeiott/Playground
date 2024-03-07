using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Serientermine.Series
{
    /// <summary> Serie für wöchentliche Termine </summary>
    public sealed class WeeklySerie : SerieBase
    {
        /// <summary> Liste der Wochentage, an denen der Serienttermin stattfinden soll </summary>
        //public List<string> DayList { get; set; }

        /// <inheritdoc />
        public override SerieType Type1 => SerieType.Weekly;

        /// <inheritdoc />
        public override string IntervallDescription => $"Jede(n) {Intervall}. Woche";

        /// <inheritdoc />
        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            // Vorabprüfung auf leere Eingabe
            if (string.IsNullOrEmpty(WeekDay))
                yield break;

            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = Begin;

            while (WeekDay != current.DayOfWeek.ToString())
            {
                current = current.AddDays(1);
            }
            DateTime startDate = current;
            // Schleife durch die Termine
            var count = 0;
            while (current <= checkedEnd && (Limit == 0 || current <= Begin.AddDays(Limit * Intervall * 7).AddDays(-7).AddDays(startDate.Day - Begin.Day)))
            {
                if (current >= checkedStart && IsInRange(checkedStart, checkedEnd, current))
                {
                    yield return current;
                    count++;
                }

                // Schleife um Intervall anpassen
                current = current.AddDays(7 * Intervall);
            }
        }

        private static bool IsInRange(DateTime checkedStart, DateTime checkedEnd, DateTime current)
            => current >= checkedStart && current <= checkedEnd;
    }
}
