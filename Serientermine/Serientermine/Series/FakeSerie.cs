using System;
using System.Collections.Generic;

namespace Serientermine.Series
{
    internal sealed class FakeSerie : SerieBase
    {
        public override SerieType Type => SerieType.Undefined;

        public override string IntervallDescription => "Fake";

        public override IEnumerable<DateTime> GetDatesInRange(DateTime start, DateTime end)
        {
            while (start <= end)
            {
                yield return start;
                start = start.AddDays(1);
            }
        }
    }
}
