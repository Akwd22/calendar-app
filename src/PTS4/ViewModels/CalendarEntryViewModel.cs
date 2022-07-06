using PTS4.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PTS4.ViewModels
{
    public class CalendarEntryViewModel : BaseViewModel
    {
        private CalendarEntry entry;

        public CalendarEntryViewModel(CalendarEntry entry)
        {
            this.entry = entry;
        }

        /// <summary>
        /// GUID de l'entrée calendrier
        /// </summary>
        public String Guid
        {
            get => this.entry.Guid;
        }

        /// <summary>
        /// Titre de l'entrée calendrier
        /// </summary>
        public String Summary
        {
            get => this.entry.Course.Title;
        }

        /// <summary>
        /// Alias pour le titre de l'entrée calendrier
        /// </summary>
        public String Title
        {
            get => this.Summary;
        }

        /// <summary>
        /// Lieu
        /// </summary>
        public String Location
        {
            get => this.entry.Location;
        }

        /// <summary>
        /// Alias pour le lieu
        /// </summary>
        public String Subtitle
        {
            get => this.Location;
        }

        /// <summary>
        /// Heure de début
        /// </summary>
        public String StartTime
        {
            get => this.entry.StartTime.ToString("HH:mm", CultureInfo.GetCultureInfo("fr-FR"));
        }

        /// <summary>
        /// Heure de fin
        /// </summary>
        public String EndTime
        {
            get => this.entry.EndTime.ToString("HH:mm", CultureInfo.GetCultureInfo("fr-FR"));
        }
    }
}
