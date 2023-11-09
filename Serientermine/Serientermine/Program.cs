using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Serientermine
{
    internal class Program
    {
        private static IHost HostInstance;

        static void Main(string[] args)
        {
            // NOTE: Das Einlesen der appsettings.json sollte nun funktionieren
            var host = Host.CreateDefaultBuilder(args).Build();
            DateTime maxEnd= new DateTime(2099,12,31);
            try
            {
                host.Start();

                HostInstance = Host.CreateDefaultBuilder(args).Build();

                List<Serientermine.Serie> seriesList = new List<Serientermine.Serie>();

                var config = HostInstance.Services.GetRequiredService<IConfiguration>();
                var children = config.GetSection("Series").GetChildren();

                foreach (var child in children)
                {
                    string wochentage="";
                    string name = child.GetValue<string>("Name");
                    string type = child.GetValue<string>("Type");
                    int intervallNummer = child.GetValue<int>("IntervallNummer");
                    DateTime begin = (DateTime)child.GetDateTime("Begin");
                    DateTime? end = child.GetDateTime("end");

                    Serientermine.Serie serie = new Serientermine.Serie(name, type, intervallNummer, begin, end, wochentage);
                    seriesList.Add(serie);

                }

                // Jetzt enthält seriesList Ihre Liste von Serie-Objekten
                foreach (Serientermine.Serie serie in seriesList)
                {
                    switch (serie.Type)
                    {
                        case "Daily": CalculateDates.Daily.GetDates(serie, maxEnd);
                            break;
                        case "Weekly":
                            break;
                    }
                }
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
