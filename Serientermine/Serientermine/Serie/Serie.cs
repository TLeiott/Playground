using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Serie
{
    internal class Serie
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int IntervallNummer { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string Wochentage { get; set; }
        public List<DateTime> dateTimes { get; set; }
        public Serie(string name, string type, int intervallNummer, DateTime begin, DateTime end, string wochentage)
        {
            Name = name;
            Type = type;
            IntervallNummer = intervallNummer;
            Begin = begin;
            End = end;
            Wochentage = wochentage;
        }
    }
}
