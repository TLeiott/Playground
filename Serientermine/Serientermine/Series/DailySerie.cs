﻿using System;
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
            //DEBUGING
            DateTime debug_startTime = DateTime.Now;

            var intervall = Intervall;

            // Startdatum festlegen
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = checkedStart;
            current = Begin;
            var count = 0;

            TimeSpan difference = checkedStart - Begin;
            int gap = (int)Math.Floor(difference.Days / (double)intervall);
            current=current.AddDays(gap*Intervall);
            count = gap;

            //DEBUGING
            DateTime debug_stepTime = DateTime.Now;

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