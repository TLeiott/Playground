using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Text.Json;
using System.Collections.Generic;

namespace Serientermine
{
    internal class Program
    {
        public class SeriesItem
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public int IntervallNummer { get; set; }
            public string Begin { get; set; }
            public string End { get; set; }
            public string Wochentage { get; set; }
        }

        public class RootObject
        {
            public List<SeriesItem> Series { get; set; }
        }
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).Build();
            string fileContents = "";

            try
            {
                host.Start();
                string filePath = @"D:\Github\PlayGround\Serientermine\Serientermine\appsettings.json";
                try
                {// Read the contents of the file into a string
                    fileContents = File.ReadAllText(filePath);
                    // Now, the file contents are stored in the "fileContents" variable as a string
                    Console.WriteLine(fileContents);
                }
                catch (Exception e)
                { Console.WriteLine($"An error occurred: {e.Message}"); }


            }
            finally
            {
                host.StopAsync().Wait();
            }
            RootObject root = JsonSerializer.Deserialize<RootObject>(fileContents);

            if (root != null)
            {
                foreach (var seriesItem in root.Series)
                {
                    Console.WriteLine($"Name: {seriesItem.Name}");
                    Console.WriteLine($"Type: {seriesItem.Type}");
                    Console.WriteLine($"IntervallNummer: {seriesItem.IntervallNummer}");
                    Console.WriteLine($"Begin: {seriesItem.Begin}");
                    Console.WriteLine($"End: {seriesItem.End}");
                    Console.WriteLine($"Wochentage: {seriesItem.Wochentage}");
                    Console.WriteLine();
                }
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
