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
            // Startdatum festlegen
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = Begin;
            // Berechnen des Abstands zwischen checkedStart und Begin
            int gap = (int)Math.Floor((checkedStart - Begin).Days / (double)Intervall);
            current = current.AddDays(gap * Intervall);
            // Generieren der Daten im Bereich

            while (current <= checkedEnd && (Limit == 0 || current <= Begin.AddDays(Limit * Intervall).AddDays(-1)))
            {
                yield return current;
                current = current.AddDays(Intervall);
            }
        }

    }
}