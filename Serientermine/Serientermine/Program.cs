using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serientermine.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).Build();

            try
            {
                host.Start();
                //var series = GetSeriesFromConfiguration(host);


                var dateFrom = new DateTime(2023, 1, 1);
                var dateTo = new DateTime(2023, 12, 31);

                List<Input.Serie> serien = new List<Input.Serie>();
                serien.Add(new Serie { ID = 1, StartDatum = DateTime.Parse("01.01.2022"), EndDatum = DateTime.Parse("01.01.2023"), Wochentage= new List<int> {1,2,3,4,5,6,7} });


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
