using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine
{
    internal static class DayOfWeekExtensions
    {
        public static string ToGermanText(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                _ => "Sonntag"
            };
        }
    }
}
