using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PTS4.ViewModels
{
    public class EntryGroupViewModel : BaseViewModel
    {
        private DateTime date;

        /// <param name="date">Date du jour</param>
        public EntryGroupViewModel(DateTime date)
        {
            this.date = date;
        }

        /// <summary>
        /// Nom du jour de semaine
        /// </summary>
        public String Day
        {
            get => this.date.Date.ToString("dddd", CultureInfo.GetCultureInfo("fr-FR"));
        }

        /// <summary>
        /// Date du jour
        /// </summary>
        public String Date
        {
            get => this.date.Date.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("fr-FR"));
        }
    }
}
