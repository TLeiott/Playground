using System;
using System.Collections.Generic;

namespace Serientermine.Series
{
    public abstract class SerieBase : ISerie
    {
        public abstract SerieType Type { get; }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Begin { get; set; }

        public DateTime? End { get; set; }

        public int Intervall { get; set; }

        public abstract string IntervallDescription { get; }

        public int Limit { get; set; }
        /// <summary>
        /// "TagImMonat"=>Json
        /// </summary>
        public int MonthDay { get; set; }
        public string WeekDay { get; set; }
        /// <summary>
        /// "MonatImJahr"=>Json
        /// </summary>
        public int Month { get; set; }

        public abstract IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end);

        protected (DateTime start, DateTime end) GetDatesForOutput(DateTime start, DateTime end)
        {
            if (Begin > start)
            {
                start = Begin;
            }
            if (End != null && End < end)
            {
                end = End.Value;
            }
            return (start, end);
        }
    }
}



//
//       PRIMARY KEY("Id" AUTOINCREMENT)
//private const string CreateTableSql = @"
//          CREATE TABLE IF NOT EXISTS Series (
//              Id INTEGER PRIMARY KEY AUTOINCREMENT,
//              SerieType INTEGER NOT NULL,
//              Name TEXT,ö
//              Begin TEXT NOT NULL,
//              End TEXT,
//              Intervall INTEGER NOT NULL,
//              RepeatLimit INTEGER,
//              WeekDay TEXT,
//              MonthDay INTEGER,
//              Month INTEGER
//          );";
//
//
//
//
//
//
//
//
//
//
//
//