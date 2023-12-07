using System;
using System.Collections.Generic;

namespace Serientermine.Series
{
    internal interface ISerie
    {
        /// <summary>
        /// Art der Serie, z.B. wöchentlich
        /// </summary>
        SerieType Type { get; }

        /// <summary>
        /// Name der Serie
        /// </summary>
        string Name { get; }

        /// <summary>
        /// StartDatum der Serie
        /// </summary>
        DateTime Begin { get; }

        /// <summary>
        /// Optionales Ende der Serie
        /// </summary>
        DateTime? End { get; }

        /// <summary>
        /// Intervall der Serie
        /// </summary>
        int Intervall { get; }

        /// <summary>
        /// Beschreibung des Intervals
        /// </summary>
        string IntervallDescription { get; }

        /// <summary>
        /// Anzahl der Wiederholungen, 0 = keine Begrenzung der Wiederholungen
        /// </summary>
        int Limit { get; }

        int MonthDay { get; }

        int Month {  get; }

        /// <summary>
        /// Liefert die Daten im angegebenen Zeitraum
        /// </summary>
        IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end);
    }
}
