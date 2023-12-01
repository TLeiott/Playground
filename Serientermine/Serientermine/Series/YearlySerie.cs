using Serientermine.Serientermine;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Series
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class YearlySerie : SerieBase
    {
        public List<string> DayList { get; set; }

        public override SerieType Type => SerieType.Yearly;

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

            //Zu hohe angaben bei Tag im Monat wenn kein Wochentag gegeben
            if (DayList.Count != 0 && DayList != null && MonthDay > 5)
            {
                yield break;
            }

            List<string> dayList = DayList;

            //Startdatum definieren
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = Begin;

            //Leeres Limit hochsetzen
            if (Limit == 0)
            {
                Limit = 999999999;
            }

            string targetDay = "";
            foreach (var day in dayList)
            {
                targetDay = day;
            }

            if (DayList == null || DayList.Count == 0)//Wochentag nich angegeben
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
        /// <param name="monthDay">Der wievielte angegebene Wochentag ausgegeben werden soll</param>
        /// <param name="targetDay">Wochentag der bei einer angegebenen zahl ausgegeben werden soll</param>
        /// <returns></returns>
        private IEnumerable<DateTime> CalculateDatesWithoutWeekday(DateTime checkedStart, DateTime checkedEnd, DateTime current, int limit, int monthDay, string targetDay = "")
        {
            current = new DateTime(current.Year, Month, 1);
            int count = 0;//Counter für das Terminlimit
            while (current <= checkedEnd && count < limit)
            {
                while (current < checkedEnd)
                {
                    DateTime lastDay = GetLastDayOfMonth(current);

                    if (current >= lastDay)
                    {
                        if (monthDay > 28)
                        {
                            if (IsInRange(checkedStart, checkedEnd, current))
                            {
                                yield return current;
                            }
                        }
                        break;
                    }
                    else if (current.Day == monthDay)
                    {
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
                if (current.Month == Month)
                {
                    count++;
                }
                current = current.AddYears(1);
                current = new DateTime(current.Year, Month, 1);
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
            while (current <= checkedEnd && count < limit)
            {
                current = new DateTime(current.Year, Month, 1);
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
                            break;
                        }
                    }
                    current = current.AddDays(1);//Einen Tag hinzufügen zum Durchspulen des Monats
                }
                count++;
                current = current.AddYears(1);
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
            while (current <= checkedEnd && count < limit)
            {
            current = new DateTime(current.Year, Month, 1);
            current = new DateTime(current.Year, Month, GetLastDayOfMonth(current).Day);
                while (current.DayOfWeek.ToString() != targetDay)
                {
                    current = current.AddDays(-1);
                }
                if (IsInRange(checkedStart, checkedEnd, current))
                {
                    yield return current;
                }
                count++;
                current = current.AddYears(1);

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
        //private void SwitchToTargetMonth(DateTime current, )
        //Ohne Wochentag und Mit letzem wochentag funktioniert schon, nur das innerhalb des Startjahres zurückgesprungen wird. Um dies zu fixen muss die Obere Funktion fertig gestellt werden. Ronny wegen Unitest ansprechen.
    }
}
/*
using Serientermine.Serientermine;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Series
{
    internal class MonthlySerie : SerieBase
    {
        public List<string> DayList { get; set; }

        public override SerieType Type => SerieType.Monthly;

        public override string IntervallDescription => $"Jede(n) {Intervall}. Monat";
        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            //leere angaben bei "TagImMonat" herausfiltern
            if (MonthDay == 0)
                yield break;

            //mehrere angaben bei "Wochentage" herausfiltern
            if (DayList.Count > 1)
                yield break;

            List<string> dayList = DayList;
            int intervall = Intervall;

            //Startdatum definieren
            var (checkedStart, checkedEnd) = GetDatesForOutput(start, end);
            var current = Begin;

            int count = 0;//Counter für das Terminlimit
            int weekDayCount = 1;//Counter für die Wochentage 
            string targetDay = "";
            foreach (var day in dayList)
            {
                targetDay = day;
            }

            while (current <= checkedEnd && count < Limit)//wenn im zeitraum
            {
                string dayOfWeekString = "";
                string monthSaved = current.Month.ToString();
                while (current.Month.ToString() == monthSaved)
                {
                    //UI.ConsoleWriter.LineColor($"Start: Current: {current}; Current.Day: {current.Day}; Month.Day: {MonthDay}; count: {count}; weekDayCount: {weekDayCount}",ConsoleColor.DarkBlue);
                    if (DayList == null || DayList.Count == 0)
                    {
                        if (current.Day == MonthDay)
                        {
                            if (current > checkedStart)
                            {
                                yield return current;
                            }
                            else
                            {
                                UI.ConsoleWriter.LineColor($"|{current.ToString("dd.MM.yyyy")}| {current.DayOfWeek} OutOfRangeDate", ConsoleColor.DarkGray);
                            }
                            count++;
                            break;
                        }
                    }
                    else//Wochentag angegeben
                    {
                        if (weekDayCount >= MonthDay && current.DayOfWeek.ToString() == targetDay)
                        {
                            if (current > checkedStart)
                            {
                                yield return current;
                            }
                            else
                            {
                                UI.ConsoleWriter.LineColor($"|{current.ToString("dd.MM.yyyy")}| {current.DayOfWeek}   OutOfSelectedRange", ConsoleColor.Gray);
                            }
                            weekDayCount = 1;
                            count++;
                            break;
                        }
                        else
                        {
                            if (current.DayOfWeek.ToString() == targetDay)
                            {
                                weekDayCount++;
                                UI.ConsoleWriter.LineColor($"|{current.ToString("dd.MM.yyyy")}| {current.DayOfWeek}", ConsoleColor.DarkGray);
                            }
                        }
                    }
                    UI.ConsoleWriter.LineColor($"|{current.ToString("dd.MM.yyyy")}| {current.DayOfWeek}", ConsoleColor.DarkGray);
                    current = current.AddDays(1);//+1 Tag
                }
                current = current.AddDays(-1);//-1 Tag
                current = new DateTime(current.Year, current.Month, 1).AddMonths(1);

            }
        }
    }
}

 */