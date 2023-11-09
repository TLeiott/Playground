using Microsoft.Extensions.Configuration;
using System;

namespace Serientermine
{
    internal static class Extentions
    {
        /// <summary>
        /// Liefert den Wert im richtigen Datumsformat aus. Ist eine Erweiterung der IConfigurationSection
        /// </summary>
        /// <param name="child">Das Item der IConfigurationSection</param>
        /// <param name="name"></param>
        /// <param name="dateformat"></param>
        /// <returns>Das rchtige Datum</returns>
        /// <exception cref="Exception"></exception>
        public static DateTime? GetDateTime(this IConfigurationSection child, string name, string dateformat = "dd.MM.yyyy")
        {
            var value = child.GetValue<string>(name);
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (!DateTime.TryParseExact(value, dateformat, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {
                throw new Exception($"Das Datum {value} konnte nicht geparst werden.");
            }
            
            return dateTime;
        }
    }
}
