using System;

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
