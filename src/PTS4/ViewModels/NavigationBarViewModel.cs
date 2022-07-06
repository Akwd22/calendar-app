using PTS4.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PTS4.ViewModels
{
    public class NavigationBarViewModel : BaseViewModel
    {
        private DateTime currentDate;
        private String currentView;
        
        public NavigationBarViewModel()
        {
            this.currentDate = DateTime.Now;
            this.currentView = "Jour";
        }

        /// <summary>
        /// Vue actuelle<br/>
        /// Valeurs possibles : "Jour", "Semaine", "Mois"
        /// </summary>
        public String CurrentView
        {
            get => this.currentView;
            set
            {
                this.currentView = value;
                this.OnPropertyChanged("CurrentDate");
            }
        }

        /// <summary>
        /// Date actuelle en cours de visionnage
        /// </summary>
        public String CurrentDate
        {
            get
            {
                switch (this.CurrentView)
                {
                    case "Jour":
                        return this.currentDate.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("fr-FR"));
                    case "Semaine":
                        return this.currentDate.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("fr-FR"))
                            + " à "
                            + Utils.GetWeekEndDate(this.currentDate).ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("fr-FR"));
                    default:
                        throw new Exception("Type vue inconnue : " + this.CurrentView);
                }
            }
        }

        /// <summary>
        /// Passer au jour, à la semaine, au mois suivant selon la vue actuelle
        /// </summary>
        public void NextDate()
        {
            switch (this.CurrentView)
            {
                case "Jour":
                    this.currentDate = this.currentDate.AddDays(1);
                    break;
                case "Semaine":
                    this.currentDate = Utils.GetWeekEndDate(this.currentDate).AddDays(1);
                    break;
                default:
                    throw new Exception("Type vue inconnue : " + this.CurrentView);
            }

            this.OnPropertyChanged("CurrentDate");
        }

        /// <summary>
        /// Passer au jour, à la semaine, au mois précédent selon la vue actuelle
        /// </summary>
        public void PreviousDate()
        {
            switch (this.CurrentView)
            {
                case "Jour":
                    this.currentDate = this.currentDate.AddDays(-1);
                    break;
                case "Semaine":
                    this.currentDate = Utils.GetWeekStartDate(this.currentDate).AddDays(-1);
                    break;
                default:
                    throw new Exception("Type vue inconnue : " + this.CurrentView);
            }

            this.OnPropertyChanged("CurrentDate");
        }
    }
}
