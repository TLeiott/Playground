using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Serientermine
{
    /// <summary>
    /// Generelle Klasse für die Terminserien
    /// </summary>
    internal class Serie
    {
        /// <summary>
        /// Name der Serie
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Typ der Serie (Täglich, Monatlich)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Intervall der Serie
        /// </summary>
        public int Intervall { get; set; }
        /// <summary>
        /// StartDatum der Serie
        /// </summary>
        public DateTime Begin { get; set; }
        /// <summary>
        /// Mögliches Ende der Serie
        /// </summary>
        public DateTime? End { get; set; }
        /// <summary>
        /// Liste der Tage in einer Woche
        /// </summary>
        public List<string> DayList { get; set; }
        /// <summary>
        /// Liste der Wochentage (jeder 2. (Sonntag/Montag/etc.) )
        /// </summary>
        public List<int> WeekdayList { get; set; }
        /// <summary>
        /// Wochentag (Monday, Sunnday, etc..)
        /// </summary>
        public List<int> MonthsList {  get; set; }
        /// <summary>
        /// Das Maximaledatum
        /// </summary>
        public int Limit { get; set; }

        public Serie(string name, string type, int intervall, DateTime begin, DateTime? end, List<string> dayList, List<int> weekdayList, List<int> monthsList, int limit)
        {
            Name = name;
            Type = type;
            Intervall = intervall;
            Begin = begin;
            End = end;
            DayList = dayList;
            WeekdayList = weekdayList;
            MonthsList = monthsList;
            Limit = limit;
        }
    }
}
