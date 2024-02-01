using Hmd.Environments;
using Microsoft.Extensions.Configuration;
using Serientermine.Series;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serientermine.Providers
{
    internal class JsonSeriesProvider : ISeriesProvider
    {
        public List<ISerie> GetSeries()
        {
            var list = new List<ISerie>();

            var config = HmdEnvironment.GetRequiredService<IConfiguration>();
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
                        serie = new MonthlySerie{};
                        break;
                    case "Yearly":
                        serie = new YearlySerie{};
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
                serie.WeekDay = child.GetValue<string>("Wochentag");
                serie.Month = child.GetValue<int>("MonatImJahr");

                list.Add(serie);

            }

            return list;

            static List<string> GetDayList(string weekdays)
                => (weekdays ?? string.Empty).Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
