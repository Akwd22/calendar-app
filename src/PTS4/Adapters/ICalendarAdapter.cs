using PTS4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS4.Adapters
{
    /// <summary>
    /// Adaptateur pour interface calendrier
    /// </summary>
    public interface ICalendarAdapter
    {
        /// <summary>
        /// Retourner toutes les entrées calendrier
        /// </summary>
        /// <returns>Entrées calendrier</returns>
        CalendarEntries GetAllEntries();
    }
}
