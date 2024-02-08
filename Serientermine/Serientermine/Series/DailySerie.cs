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
            var count = 0;

            TimeSpan difference = checkedStart - Begin;
            int gap = (int)Math.Floor(difference.Days / (double)intervall);
            current=current.AddDays(gap*Intervall);
            count = gap;

            if (Limit == 0)
            {
                while (current <= checkedEnd) //wenn im zeitraum
                {
                    if (current >= checkedStart)
                    {
                        yield return current;
                    }
                    current = current.AddDays(intervall);// Tag
                }
            }
            else
            {
                while (current <= checkedEnd && (count < Limit)) //wenn im zeitraum
                {
                    if (current >= checkedStart)
                    {
                        yield return current;
                    }

                    count++;
                    current = current.AddDays(intervall);// Tag
                }
            }
        }
    }
}