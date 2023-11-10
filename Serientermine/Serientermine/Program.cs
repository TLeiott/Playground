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
                host.Start();

                HostInstance = Host.CreateDefaultBuilder(args).Build();

                List<Serientermine.Serie> seriesList = new List<Serientermine.Serie>();

                var config = HostInstance.Services.GetRequiredService<IConfiguration>();
                var children = config.GetSection("Series").GetChildren();
                foreach (var child in children)
                {
                    string name = child.GetValue<string>("Name");
                    string type = child.GetValue<string>("Type");
                    int intervallNummer = child.GetValue<int>("IntervallNummer");
                    string dayOfWeek = child.GetValue<string>("Wochentage");
                    int limit=child.GetValue<int>("limit");
                    List<string> dayList = new List<string>();
                    if (dayOfWeek != null)
                    {
                        string[] tageArray = dayOfWeek.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                       dayList.AddRange(tageArray);
                    }
                    DateTime begin = (DateTime)child.GetDateTime("Begin");
                    DateTime? end = child.GetDateTime("end");

                    Serientermine.Serie serie = new Serientermine.Serie(name, type, intervallNummer, begin, end, dayList,limit);
                    seriesList.Add(serie);

                }   
                // Jetzt enthält seriesList Ihre Liste von Serie-Objekten
                foreach (Serientermine.Serie serie in seriesList)
                {
                    switch (serie.Type)
                    {
                        case "Daily":
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
    }
}
