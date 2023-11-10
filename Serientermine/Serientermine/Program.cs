using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Serientermine
{
    internal class Program
    {
        private static IHost HostInstance;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            // NOTE: Das Einlesen der appsettings.json sollte nun funktionieren
            var host = Host.CreateDefaultBuilder(args).Build();
            DateTime maxEnd = new DateTime(2099, 12, 31);
            try
            {
                UI.ConsoleWriter.Color("DEBUG0", ConsoleColor.Red);
                host.Start();

                HostInstance = Host.CreateDefaultBuilder(args).Build();

                List<Serientermine.Serie> seriesList = new List<Serientermine.Serie>();

                var config = HostInstance.Services.GetRequiredService<IConfiguration>();
                var children = config.GetSection("Series").GetChildren();
                UI.ConsoleWriter.Color("DEBUG1", ConsoleColor.Red);
                foreach (var child in children)
                {
                    string name = child.GetValue<string>("Name");
                    string type = child.GetValue<string>("Type");
                    int intervallNummer = child.GetValue<int>("IntervallNummer");
                    string dayOfWeek = child.GetValue<string>("Wochentage");
                    List<string> dayList = new List<string>();
                    if (dayOfWeek != null)
                    {
                        string[] tageArray = dayOfWeek.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                       dayList.AddRange(tageArray);
                    }

                    /*
                if (int.TryParse(daySetRaw, out int daySet)){}
                else
                {
                    daySet = 0;
                    switch (daySetRaw)
                    {
                        case "Monday":break;
                    }
                }
                    */
                    DateTime begin = (DateTime)child.GetDateTime("Begin");
                    DateTime? end = child.GetDateTime("end");

                    Serientermine.Serie serie = new Serientermine.Serie(name, type, intervallNummer, begin, end, dayList);
                    seriesList.Add(serie);

                }
                UI.ConsoleWriter.Color("DEBUG2", ConsoleColor.Red);
                // Jetzt enthält seriesList Ihre Liste von Serie-Objekten
                foreach (Serientermine.Serie serie in seriesList)
                {
                    UI.ConsoleWriter.Color("DEBUG3", ConsoleColor.Red);
                    switch (serie.Type)
                    {
                        case "Daily":UI.ConsoleWriter.Color("DEBUG2", ConsoleColor.Red);
                            CalculateDates.Daily.GetDates(serie, maxEnd);
                            break;
                        case "Weekly":
                            CalculateDates.Weekly.GetDates(serie, maxEnd);
                            break;
                        case "Monthly":
                            CalculateDates.Monthly.GetDates(serie, maxEnd);
                            break;
                    }
                }
                Console.Read();
            }
            finally
            {
                host.StopAsync().Wait();
            }
        }
        //private static List<Serientermin.ITerminSerie> GetSeriesFromConfiguration(IHost host)
        //{
        //    var config = host.Services.GetRequiredService<IConfiguration>();
        //    var children = config.GetSection("Series").GetChildren();

        //    List<Serientermin.ITerminSerie> serientermineListe = new();

        //    foreach (var child in children)
        //    {
        //        var begin = child.GetValue<string>("Begin") ?? throw new Exception("Das Datum 'Beginn' ist nicht gesetzt.");
        //        var type = child.GetValue<string>("Type");
        //        switch (type)
        //        {
        //            case "Daily":
        //                var dailyTerminSerie = new Serientermin.DailyTerminSerie
        //                {
        //                    Name = child.GetValue<string>("Name"),
        //                    Type = child.GetValue<string>("Type"),
        //                    IntervallNummer = child.GetValue<int>("IntervallNummer"),
        //                    Begin = (DateTime)child.GetDateTime("Begin"),
        //                    End = child.GetDateTime("End")
        //                };
        //                serientermineListe.Add(dailyTerminSerie);
        //                break;

        //            case "Weekly":
        //                var weeklyTerminSerie = new Serientermin.WeeklyTerminSerie
        //                {
        //                    Name = child.GetValue<string>("Name"),
        //                    Type = child.GetValue<string>("Type"),
        //                    IntervallNummer = child.GetValue<int>("IntervallNummer"),
        //                    Begin = (DateTime)child.GetDateTime("Begin"),
        //                    End = child.GetDateTime("End"),
        //                    Wochentage = child.GetValue<string>("Wochentage").Split(",").ToList()
        //                };
        //                serientermineListe.Add(weeklyTerminSerie);
        //                break;

        //            default:
        //                throw new NotSupportedException("Der Typ der Serie ist nicht bekannt.");
        //        }
        //    }
        //    return serientermineListe;
        //}

        //private static (DateTime begin, DateTime end) GetDatesFromParsedUserInput(string range)
        //{
        //    var dates = range
        //        .Replace(" ", "")
        //        .Split("-")
        //        .Select(dateString => DateTime.TryParse(dateString, CultureInfo.CurrentCulture, out var date) ? date : (DateTime?)null)
        //        .Where(date => date != null)
        //        .Select(date => date.Value)
        //        .ToList();

        //    if (dates.Count != 2)
        //        throw new Exception("Die Range muss folgendem beispielhaften Aufbau entsprechen: 01.01.2023 - 31.12.2023");

        //    return (dates[0], dates[1]);
        //}
    }
}
