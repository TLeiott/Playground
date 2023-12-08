using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serientermine.Providers;
using Serientermine.Series;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serientermine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> errorLog = new List<string>();
            Console.CursorVisible = false;
            // NOTE: Das Einlesen der appsettings.json sollte nun funktionieren
            var host = Host.CreateDefaultBuilder(args).Build();
            try
            {
                var rangeStart = new DateTime(2021, 1, 5);
                var rangeEnd = new DateTime(2021, 1, 11);
                host.Start();

                var series = GetSeries(host);
                var writer = new ConsoleWriter();   // Anhand von Parameter kann hier eine alternative Implementierung gewählt werden, z.B. FileWriter

                // Jetzt enthält seriesList Ihre Liste von Serie-Objekten
                foreach (var serie in series)
                {
                    writer.Write(serie, rangeStart, rangeEnd, rangeStart, rangeEnd);
                }

                Console.Read();
                foreach (string text in errorLog)
                {
                    UI.ConsoleWriter.LineColor(text, ConsoleColor.Red);
                }
            }
            finally
            {
                host.StopAsync().Wait();
            }
        }

        private static List<ISerie> GetSeries(IHost host)
        {
            var list = new List<ISerie>();

            var config = host.Services.GetRequiredService<IConfiguration>();
            var children = config.GetSection("Series").GetChildren();

            foreach (var child in children)
            {
                SerieBase serie;
                var type = child.GetValue<string>("Type");
                switch (type)
                {
                    case "Daily":
                        serie = new DailySerie();
                        break;
                    case "Weekly":
                        serie = new WeeklySerie
                        {
                            DayList = GetDayList(child.GetValue<string>("Wochentage"))
                        };
                        break;
                    case "Monthly":
                        serie = new MonthlySerie
                        {
                            DayList = GetDayList(child.GetValue<string>("Wochentage"))
                        };
                        break;
                    case "Yearly":
                        serie = new YearlySerie
                        {
                            DayList = GetDayList(child.GetValue<string>("Wochentage"))
                        };
                        break;
                    default:                        
                        throw new NotSupportedException($"Der Serientyp '{type}' ist noch nicht implementiert.");
                }

                serie.Name = child.GetValue<string>("Name");
                serie.Intervall = child.GetValue<int>("Intervall");
                serie.Limit = child.GetValue<int>("limit");
                serie.Begin = (DateTime)child.GetDateTime("Begin");
                serie.End = child.GetDateTime("end");
                serie.MonthDay = child.GetValue<int>("TagImMonat");
                serie.Month = child.GetValue<int>("MonatImJahr");

                list.Add(serie);

                //string day = child.GetValue<string>("Wochentage");
                //string dayOfWeekIntervall = child.GetValue<string>("Wochentag-Intervall");
                //string monthsString = child.GetValue<string>("Monat");
                //List<int> months = new List<int>();
                //List<string> dayList = new List<string>();
                //List<int> weekdayList = new List<int>();
                //if (months.Count != 0)
                //{
                //    string[] monthsArray = monthsString.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //}
                //if (day != null)
                //{
                //    string[] tageArray = day.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //    dayList.AddRange(tageArray);
                //}
                //if (dayOfWeekIntervall != null)
                //{
                //    string[] weekdayIntervallArray = dayOfWeekIntervall.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string weekday in weekdayIntervallArray)
                //    {
                //        weekdayList.Add(int.Parse(weekday));
                //    }
                //}
                //Serientermine.Serie serie = new Serientermine.Serie(name, type, intervall, begin, end, dayList, weekdayList, months, limit);
                //seriesList.Add(serie);

            }

            return list;

            static List<string> GetDayList(string weekdays)
                => (weekdays ?? string.Empty).Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        }
        static int WeekdayToInt(string input)
        {
            int output = 0;
            switch (input)
            {
                case "Monday": output = 1; break;
                case "Tuesday": output = 2; break;
                case "Wednesday": output = 3; break;
                case "Thursday": output = 4; break;
                case "Friday": output = 5; break;
                case "Saturday": output = 6; break;
                case "Sunday": output = 0; break;
            }
            return output;
        }
    }
}
