using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace Serientermine
{
    internal class Program
    {

        public class RootObject
        {
            public List<Serie.Serie> Series { get; set; }
        }

        // Create a custom converter for DateTime
        class DateTimeConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                string dateStr = reader.GetString();
                return DateTime.ParseExact(dateStr, "dd.MM.yyyy", null);
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("dd.MM.yyyy"));
            }
        }
        static void Main(string[] args)
        {
            string fileContents = File.ReadAllText(@"D:\Github\PlayGround\Serientermine\Serientermine\appsettings.json");

            // Register the custom converter
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // to handle lowercase property names
            };
            options.Converters.Add(new DateTimeConverter());

            // Parse the JSON into a list of Serie objects
            var series = JsonSerializer.Deserialize<RootObject>(fileContents, options).Series;

            // Create a list to store Serie.Serie objects
            List<Serie.Serie> serieList = new List<Serie.Serie>();


            // Iterate through the parsed Serie objects and create Serie.Serie instances
            foreach (var serie in series)
            {
                string name = serie.Name;
                string type = serie.Type;
                int intervallNummer = serie.IntervallNummer;
                DateTime begin = serie.Begin;
                DateTime end = serie.End;
                string wochentage = serie.Wochentage;

                // Create a new Serie.Serie instance and add it to the list
                Serie.Serie newSerie = new Serie.Serie(name, type, intervallNummer, begin, end, wochentage);
                serieList.Add(newSerie);
            }
            // Now you have a list of Serie.Serie objects
            foreach (var serie in serieList)
            {
                Console.WriteLine($"Name: {serie.Name}");
                Console.WriteLine($"Type: {serie.Type}");
                Console.WriteLine($"IntervallNummer: {serie.IntervallNummer}");
                Console.WriteLine($"Begin: {serie.Begin}");
                Console.WriteLine($"End: {serie.End}");
                Console.WriteLine($"Wochentage: {serie.Wochentage}");
                Console.WriteLine();
            }

        }
        static void MainOFF(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).Build();
            string fileContents = "";

            try
            {
                host.Start();
                string filePath = @"D:\Github\PlayGround\Serientermine\Serientermine\appsettings.json";
                try
                {// Read the contents of the file into a string
                    fileContents = File.ReadAllText(filePath); Serie.Serie a = new Serie.Serie("Meine Serie", "TypA", 1, DateTime.Now, DateTime.Now.AddDays(7), "Montag");
                    List<Serie.Serie> serieList = new List<Serie.Serie>();
                    RootObject root = JsonSerializer.Deserialize<RootObject>(fileContents);
                    if (root != null)
                    {
                        foreach (var seriesItem in root.Series)
                        {
                            string name = seriesItem.Name;
                            string type = seriesItem.Type;
                            int intervallNummer = seriesItem.IntervallNummer;
                            DateTime begin = Convert.ToDateTime(seriesItem.Begin);
                            DateTime end = seriesItem.End;
                            string wochentage = seriesItem.Wochentage;

                            // Erstelle eine Instanz der Serie-Klasse und füge sie zur Liste hinzu
                            Serie.Serie newSerie = new Serie.Serie(name, type, intervallNummer, begin, end, wochentage);
                            serieList.Add(newSerie);
                        }
                        foreach (var seriesItem in root.Series)
                        {
                            Console.WriteLine(seriesItem.Name);
                            Console.WriteLine(seriesItem.Type);
                            Console.WriteLine(seriesItem.IntervallNummer);
                            Console.WriteLine(Convert.ToString(seriesItem.Begin));
                            Console.WriteLine(Convert.ToString(seriesItem.End));
                            Console.WriteLine(seriesItem.Wochentage);
                            Console.WriteLine();
                        }
                    }
                }
                catch (Exception e)
                { Console.WriteLine($"An error occurred: {e.Message}"); }


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
