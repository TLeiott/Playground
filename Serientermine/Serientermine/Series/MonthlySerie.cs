using System;
using System.Collections.Generic;

namespace Serientermine.Series
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MonthlySerie : SerieBase
    {
        public List<string> DayList { get; set; } = new();

        public override SerieType Type => SerieType.Monthly;

        public override string IntervallDescription => $"Jede(n) {Intervall}. Monat";

        /// <inheritdoc cref="ISerie.GetDatesInRange(DateTime, DateTime)"/>
        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            //leere angaben bei "TagImMonat" herausfiltern
            if (MonthDay == 0)
                yield break;

            //mehrere angaben bei "Wochentage" herausfiltern
            if (DayList.Count > 1)
                yield break;

            List<string> dayList = DayList;

            //Startdatum definieren
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = Begin;

            string targetDay = "";
            foreach (var day in dayList)
            {
                targetDay = day;
            }

            if (DayList == null || DayList.Count == 0)//WOchentag nich angegeben
            {
                var result = CalculateDatesWithoutWeekday(checkedStart, checkedEnd, current, Limit, MonthDay, targetDay);
                foreach (var item in result)
                    yield return item;
            }
            else//Wochentag angegeben
            {
                if (MonthDay < 5)
                {
                    var result = CalculateDatesWithWeekday(checkedStart, checkedEnd, current, Limit, MonthDay, targetDay);
                    foreach (var item in result)
                        yield return item;
                }
                else
                {
                    var result = CalculateLastDatesWithWeekday(checkedStart, checkedEnd, current, Limit, MonthDay, targetDay);
                    foreach (var item in result)
                        yield return item;
                }

            }
        }

        /// <summary>
        /// Funktion zum berechnen von den Datums wenn kein Wochentag angegeben ist
        /// </summary>
        /// <param name="checkedStart">Start der anzuzeigenden Range</param>
        /// <param name="checkedEnd">Ende der anzuzeigenden Range</param>
        /// <param name="current">Das aktuelle datum welches genutzt wird zum durchskippen Tag für Tag</param>
        /// <param name="limit">Maximales Limit für die anzahl an auszugebenden Datums</param>
        /// <param name="MonthDay">Der wievielte angegebene Wochentag ausgegeben werden soll</param>
        /// <param name="targetDay">Wochentag der bei einer angegebenen zahl ausgegeben werden soll</param>
        /// <returns></returns>
        private IEnumerable<DateTime> CalculateDatesWithoutWeekday(DateTime checkedStart, DateTime checkedEnd, DateTime current, int limit, int MonthDay, string targetDay = "")
        {
            int count = 0;//Counter für das Terminlimit
            while (current <= checkedEnd && (count < Limit || Limit == 0))
            {
                while (current < checkedEnd)
                {
                    DateTime lastDay = GetLastDayOfMonth(current);
                    if (current >= lastDay)
                    {
                        if (MonthDay > 28)
                        {
                            count++;
                            if (IsInRange(checkedStart, checkedEnd, current))
                            {
                                yield return current;
                            }
                        }
                        break;
                    }
                    else if (current.Day == MonthDay)
                    {
                        count++;
                        if (IsInRange(checkedStart, checkedEnd, current))
                        {
                            yield return current;
                        }
                        break;
                    }
                    else
                    {
                        current = current.AddDays(1);
                    };
                }
                current = current.AddMonths(Intervall);
                current = new DateTime(current.Year, current.Month, 1);
            }
        }

        /// <summary>
        /// Funktion zum berechnen von den Datums wenn ein Wochentag angegeben ist
        /// </summary>
        /// <param name="checkedStart">Start der anzuzeigenden Range</param>
        /// <param name="checkedEnd">Ende der anzuzeigenden Range</param>
        /// <param name="current">Das aktuelle datum welches genutzt wird zum durchskippen Tag für Tag</param>
        /// <param name="limit">Maximales Limit für die anzahl an auszugebenden Datums</param>
        /// <param name="MonthDay">Der wievielte angegebene Wochentag ausgegeben werden soll</param>
        /// <param name="targetDay">Wochentag der bei einer angegebenen zahl ausgegeben werden soll</param>
        /// <returns></returns>
        private IEnumerable<DateTime> CalculateDatesWithWeekday(DateTime checkedStart, DateTime checkedEnd, DateTime current, int limit, int MonthDay, string targetDay = "")
        {
            int count = 0;//Counter für das Terminlimit
            while (current <= checkedEnd && (count < Limit || Limit == 0))
            {
                string monthSaved = current.Month.ToString();
                int weekDayCount = 0;//Counter für die Wochentage 
                while (current.Month.ToString() == monthSaved)
                {
                    if (current.DayOfWeek.ToString() == targetDay)
                    {
                        weekDayCount++;
                        if (weekDayCount == MonthDay)
                        {
                            if (IsInRange(checkedStart, checkedEnd, current))
                            {
                                yield return current;
                            }
                            count++;
                            break;
                        }
                    }
                    current = current.AddDays(1);//Einen Tag hinzufügen zum Durchspulen des Monats
                }
                current = current.AddMonths(Intervall);
                current = new DateTime(current.Year, current.Month, 1); // Auf den ersten Tag des Monats setzen
            }
        }

        /// <summary>
        /// Funktion zum berechnen des letzten angegebenen Wochentages im Monat
        /// </summary>
        /// <param name="checkedStart">Start der anzuzeigenden Range</param>
        /// <param name="checkedEnd">Ende der anzuzeigenden Range</param>
        /// <param name="current">Das aktuelle datum welches genutzt wird zum durchskippen Tag für Tag</param>
        /// <param name="limit">Maximales Limit für die anzahl an auszugebenden Datums</param>
        /// <param name="MonthDay">Der wievielte angegebene Wochentag ausgegeben werden soll</param>
        /// <param name="targetDay">Wochentag der bei einer angegebenen zahl ausgegeben werden soll</param>
        /// <returns></returns>
        private IEnumerable<DateTime> CalculateLastDatesWithWeekday(DateTime checkedStart, DateTime checkedEnd, DateTime current, int limit, int MonthDay, string targetDay = "")
        {
            int count = 0;//Counter für das Terminlimit
            while (current <= checkedEnd && (count < Limit || Limit == 0))
            {
                string monthSaved = current.Month.ToString();
                while (current.Month.ToString() == monthSaved)
                {
                    current = current.AddMonths(1);
                    current = new DateTime(current.Year, current.Month, 1);
                    current = current.AddDays(-1);
                    while (current.DayOfWeek.ToString() != targetDay)
                    {
                        current = current.AddDays(-1);
                    }
                    if (IsInRange(checkedStart, checkedEnd, current))
                    {
                        yield return current;
                    }
                    count++;
                    current = current.AddMonths(Intervall);
                }
            }
        }

        private DateTime GetLastDayOfMonth(DateTime current)
        {
            current = current.AddMonths(1);
            current = new DateTime(current.Year, current.Month, 1);
            current = current.AddDays(-1);
            return current;
        }
        private bool IsInRange(DateTime checkedStart, DateTime checkedEnd, DateTime current)
        {
            if (current >= checkedStart && current <= checkedEnd)
            {
                return true;
            }
            return false;
        }
    }
}