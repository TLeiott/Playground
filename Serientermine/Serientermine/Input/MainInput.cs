using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Input
{
    internal class MainInput
    {
        public static void Input()
        {
            DateTime date1;
            DateTime date2;
            while (true)
            {
                //Date 1 
                UI.MainRender.DisplayAdvice("Start-Datum eingeben:");
                while (true)
                {
                    UI.MainRender.ClearInput();
                    string input = Console.ReadLine();
                    if (DateTime.TryParse(input, out date1)) { break; }
                    else { UI.MainRender.Error("Bitte gieb ein gültiges Datum ein."); }
                }
                int weekday1 = (int)date1.DayOfWeek; weekday1 = weekday1 == 0 ? 7 : weekday1; // Sonntag von 0 auf 7 setzen
                UI.MainRender.DisplayInfo($"Startdatum: {date1.ToString("dd.MM.yyyy")}, Wochentag: {ConvertWeekday(weekday1)} ({weekday1})");
                
                //Date 2
                UI.MainRender.DisplayAdvice("End-Datum eingeben:");
                while (true)
                {
                    UI.MainRender.ClearInput();
                    string input = Console.ReadLine();
                    if (DateTime.TryParse(input, out date2)) { break; }
                    else { UI.MainRender.Error("Bitte gieb ein gültiges Datum ein."); }
                }
                int weekday2 = (int)date2.DayOfWeek; weekday2 = weekday2 == 0 ? 7 : weekday2; // Sonntag von 0 auf 7 setzen
                UI.MainRender.DisplayInfo($"Enddatum: {date2.ToString("dd.MM.yyyy")}, Wochentag: {ConvertWeekday(weekday2)} ({weekday2})");

                //Weitere Wochentage Hinzufügen
                UI.MainRender.DisplayAdvice("Weitere Wochentage Hinzufügen:");

                //test-berechnen (1)
                int weekdayCount = ZaehleWochentage(date1, date2, weekday1); 
                Console.SetCursorPosition(2, 6);
                Console.Write($"Anzahl:{weekdayCount}");
            }
        }
        static int ZaehleWochentage(DateTime startDatum, DateTime endDatum, int wochentag)
        {
            int anzahl = 0;

            for (DateTime datum = startDatum; datum <= endDatum; datum = datum.AddDays(1))
            {
                int aktuellerWochentag = (int)datum.DayOfWeek;
                aktuellerWochentag = aktuellerWochentag == 0 ? 7 : aktuellerWochentag; // Sonntag von 0 auf 7 setzen

                if (aktuellerWochentag == wochentag)
                {
                    anzahl++;
                }
            }

            return anzahl;
        }
        static string ConvertWeekday(int day=-1, string="")
        {
            string output="Err";
            switch (day)
            {
                case 1: output = "Mo"; break;
                case 2: output = "Di"; break;
                case 3: output = "Mi"; break;
                case 4: output = "Do"; break;
                case 5: output = "Fr"; break;
                case 6: output = "Sa"; break;
                case 7: output = "So"; break;
            }
            return output;
        }
    }
    public class Serie
    {
        public int ID { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EndDatum { get; set; }
        public List<int> Wochentage { get; set; }
    }
}
