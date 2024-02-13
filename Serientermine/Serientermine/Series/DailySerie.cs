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
            var current = Begin;
            // Berechnen des Abstands zwischen checkedStart und Begin
            int gap = (int)Math.Floor((checkedStart - Begin).Days / (double)intervall);
            current = current.AddDays(gap * intervall);
            // Generieren der Daten im Bereich


            ///////////////////////////////////////////////////////
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            while (current <= checkedEnd && (Limit == 0 || current <= checkedStart.AddDays(Limit * intervall)))
            {
                yield return current;
                current = current.AddDays(intervall);
            }

            stopwatch.Stop();
            var runtimeInMs = stopwatch.ElapsedMilliseconds;
            ////////////////////////////////////////////////////
        }

    }
}