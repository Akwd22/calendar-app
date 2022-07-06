using System;
using System.Collections.Generic;
using System.Text;

namespace PTS4.Models
{
    public class Utils
    {
        /// <summary>
        /// Retourner la date du début de la semaine (lundi)
        /// </summary>
        /// <param name="date">Date quelconque</param>
        public static DateTime GetWeekStartDate(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }

            return date;
        }

        /// <summary>
        /// Retourner la date de fin de la semaine (dimanche)
        /// </summary>
        /// <param name="date">Date quelconque</param>
        public static DateTime GetWeekEndDate(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }

            return date;
        }
    }
}
