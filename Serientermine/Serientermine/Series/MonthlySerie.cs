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

            if (DayList == null || DayList.Count == 0)//WOchentag nich angegeben
            {
                CalculateDatesWithWeekday(checkedStart, checkedEnd, current, Limit);
            }
            else//Wochentag angegeben
            {
            }
        }
        private IEnumerable<DateTime> CalculateDatesWithWeekday(DateTime checkedStart, DateTime checkedEnd, DateTime current, int limit)
        {
            int count = 0;
            while (current <= checkedEnd && count < limit)
            {
                string dayOfWeekString = "";
                string monthSaved = current.Month.ToString();
                while (current.Month.ToString() == monthSaved)
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
            }
        }
    }
}//Die Berechnung von allem ist noch nicht perfekt, auch wenn die Titel von manchen commits anderes behaubten können. Vorallem Monthly ist gerade stark im Umbau. Viele fehler z.B. bei jeder 31 tag jeden monat existieren immernoch im Algorythmus. Jetzt wird das alles noch auf 2 einzelne Funktionen unterteilt. Der Code von vor der Teilung befindet sich darunter:
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