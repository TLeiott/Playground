using System;
using System.Collections.Generic;

namespace Serientermine.Series
{
    /// <summary> Serie für wöchentliche Termine </summary>
    public sealed class WeeklySerie : SerieBase
    {
        /// <summary> Liste der Wochentage, an denen der Serienttermin stattfinden soll </summary>
        public List<string> DayList { get; set; }

        /// <inheritdoc />
        public override SerieType Type => SerieType.Weekly;

        /// <inheritdoc />
        public override string IntervallDescription => $"Jede(n) {Intervall}. Woche";

        /// <inheritdoc />
        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            // Vorabprüfung auf leere Eingabe
            if (string.IsNullOrEmpty(WeekDay))
                yield break;

            // Prüfen des Wochentags mit vorab festgelegtem Wochentag
            DayOfWeek desiredDay;
            if (!Enum.TryParse(WeekDay, out desiredDay))
                yield break;

            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = Begin;

            // Auf den letzten Montag zurückgehen, um eine verschobene Wochenberechnung zu verhindern
            var daysUntilLastMonday = ((int)current.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            current = current.AddDays(-daysUntilLastMonday);

            // Berechne die Anzahl der Tage zwischen den gewünschten Terminen
            var daysBetweenDesiredDays = (int)desiredDay - (int)current.DayOfWeek;
            if (daysBetweenDesiredDays < 0)
                daysBetweenDesiredDays += 7;

            // Schleife durch die Termine
            var count = 0;
            while (current <= checkedEnd && (Limit == 0 || current <= Begin.AddDays(Limit * Intervall * 7).AddDays(-7)))
            {
                if (current >= checkedStart && IsInRange(checkedStart, checkedEnd, current))
                {
                    yield return current;
                    count++;
                }

                // Fortsetzen zu nächsten Wochentag
                current = current.AddDays(daysBetweenDesiredDays);

                // Schleife um Intervall anpassen
                current = current.AddDays(7 * Intervall);
            }
        }


        private static bool IsInRange(DateTime checkedStart, DateTime checkedEnd, DateTime current)
            => current >= checkedStart && current <= checkedEnd;
    }
}
