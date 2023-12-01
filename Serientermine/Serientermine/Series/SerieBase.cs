using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Series
{
    public abstract class SerieBase : ISerie
    {
        public abstract SerieType Type { get; }

        public string Name { get; set; }

        public DateTime Begin { get; set; }

        public DateTime? End { get; set; }

        public int Intervall { get; set; }

        public abstract string IntervallDescription { get; }

        public int Limit { get; set; }
        public int MonthDay { get; set; }

        public abstract IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end);

        protected (DateTime start, DateTime end) GetDatesForOutput(DateTime start, DateTime end)
        {
            UI.ConsoleWriter.LineColor($"Limit(-;+): ({start}; {end}); Serienbereich(von;bis): ({Begin}; {End})", ConsoleColor.Red);
            if (Begin > start)
            {
                start = Begin;
            }
            if (End != null && End < end)
            {
                end = End.Value;
            }
            UI.ConsoleWriter.LineColor($"Überarbeitet:(-;+): ({start}; {end})", ConsoleColor.Red);
            UI.ConsoleWriter.LineColor($"-----------------------------------------------------", ConsoleColor.Red);
            return (start, end);
        }
    }
}
