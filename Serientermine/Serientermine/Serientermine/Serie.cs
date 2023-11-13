﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Serientermine
{
    internal class Serie
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Intervall { get; set; }
        public DateTime Begin { get; set; }
        public DateTime? End { get; set; }
        public List<string> DayList { get; set; }
        public int Limit { get; set; }

        public Serie(string name, string type, int intervall, DateTime begin, DateTime? end, List<string> dayList, int limit)
        {
            Name = name;
            Type = type;
            Intervall = intervall;
            Begin = begin;
            End = end;
            DayList = dayList;
            Limit = limit;
        }
    }
}
