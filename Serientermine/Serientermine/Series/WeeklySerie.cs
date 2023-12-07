using System;
using System.Collections.Generic;
using System.Linq;

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
            //leere angaben bei "Wochentage" herausfiltern
            if (DayList == null || DayList.Count == 0)
                yield break;

            var dayList = DayList
                .Select(x => Enum.TryParse<DayOfWeek>(x, out var y) ? (DayOfWeek?)y : null)
                .Where(x => x != null)
                .Select(x => x.Value)
                .Distinct()
                .ToList();
            var intervall = Intervall;



            //Startdatum definieren
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = Begin;


            //auf den Letzten Montag zurückgehen um eine verschobene Wochenberechnung zu verhindern
            var daysUntilLastMonday = ((int)current.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            current = current.AddDays(-daysUntilLastMonday);

            var count = 0;//Counter für das Terminlimit

            // BUG: Das Limit muss im Zweifel um die bereits stattgefundenen Termine reduziert werden
            while (current <= checkedEnd && (count < Limit || Limit == 0))//wenn im zeitraum
            {
                for (var i = 0; i < 7; i++)
                {
                    // BUG: Du grast hier zwar alle möglichen Tage ab, brichst dann aber nach dem ersten Treffer ab
                    foreach (var weekday in dayList) //für jeden Tag in der Liste
                    {
                        if (count >= Limit && Limit != 0)
                            yield break;

                        if (weekday == current.DayOfWeek)
                        {
                            if (current >= checkedStart && IsInRange(checkedStart, checkedEnd, current))
                                yield return current;

                            if (current >= Begin)
                                count++;

                            break;
                        }
                    }

                    current = current.AddDays(1);//+1 Tag
                }

                current = current.AddDays(-7);
                current = current.AddDays(7 * intervall);

                // Alternative:
                //if (intervall > 1) 
                //    current = current.AddDays(7 * (intervall - 1));
            }
        }

        private static bool IsInRange(DateTime checkedStart, DateTime checkedEnd, DateTime current)
            => current >= checkedStart && current <= checkedEnd;
    }
}
