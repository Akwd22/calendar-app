using PTS4.Adapters;
using PTS4.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PTS4.ViewModels
{
    /// <summary>
    /// Types de vue
    /// </summary>
    public enum ViewType
    {
        Daily,
        Weekly,
        Monthly
    }

    public class MainViewModel : BaseViewModel
    {
        private DateTime currentDate;
        private ViewType currentView;
        private CalendarEntries calendarEntries;
        private TaskEntries taskEntries;

        private void MockTaskData()
        {
            TaskEntry t1 = new TaskEntry(Guid.NewGuid().ToString(), DateTime.Now, DateTime.Now.AddHours(2));
            t1.Course = Courses.GetCourse("TD maths S4B toxq821");
            t1.Title = "DM exercice 3 4";

            TaskEntry t2 = new TaskEntry(Guid.NewGuid().ToString(), DateTime.Now.AddHours(-2), DateTime.Now);
            t2.Course = Courses.GetCourse("TP crypto f6151qy");
            t2.Title = "IE chapître 8";
            t2.Completed = true;

            TaskEntry t3 = new TaskEntry(Guid.NewGuid().ToString(), DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(2));
            t3.Course = Courses.GetCourse("TP Sys Emb 8mwjtaa");
            t3.Title = "Repeat course";
            t3.Repeat = TypeTaskRepeat.EACH_COURSE;

            TaskEntry t4 = new TaskEntry(Guid.NewGuid().ToString(), DateTime.Now, DateTime.Now.AddHours(2));
            t4.Title = "Sans matière";

            TaskEntry t5 = new TaskEntry(Guid.NewGuid().ToString(), DateTime.Now, DateTime.Now);
            t5.Course = Courses.GetCourse("TP Sys Emb 8mwjtaa");
            t5.Title = "Repeat jour";
            t5.Repeat = TypeTaskRepeat.EACH_DAY;

            this.taskEntries.AddEntry(t1);
            this.taskEntries.AddEntry(t2);
            this.taskEntries.AddEntry(t3);
            this.taskEntries.AddEntry(t4);
            this.taskEntries.AddEntry(t5);
        }

        public MainViewModel() : base()
        {
            this.calendarEntries = new CalendarEntries();
            this.taskEntries = new TaskEntries(this.calendarEntries);
            this.currentDate = DateTime.Now;
            this.currentView = ViewType.Daily;
        }

        /// <summary>
        /// Charger un calendrier
        /// </summary>
        /// <param name="file">Chemin fichier ICS</param>
        public void LoadCalendar(String file)
        {
            IcalNetAdapter adapter = new IcalNetAdapter(file);
            this.calendarEntries.AddEntries(adapter.GetAllEntries());
            this.taskEntries.ClearEntries();
            this.MockTaskData();
        }

        /// <summary>
        /// Réintialiser le calendrier et tâches
        /// </summary>
        public void ResetCalendar()
        {
            this.calendarEntries.ClearEntries();
            this.taskEntries.ClearEntries();
        }

        /// <summary>
        /// Réintialiser les tâches
        /// </summary>
        public void ResetTasks()
        {
            this.taskEntries.ClearEntries();
        }

        /// <summary>
        /// Retourner toutes les entrées calendrier
        /// </summary>
        /// <returns>Entrées calendrier</returns>
        public CalendarEntries GetCalendarEntries()
        {
            return this.calendarEntries;
        }

        /// <summary>
        /// Retourner toutes les tâches
        /// </summary>
        /// <returns>Tâches</returns>
        public TaskEntries GetTaskEntries()
        {
            return this.taskEntries;
        }

        /// <summary>
        /// Retourner les tâches d'un jour
        /// </summary>
        /// <param name="date">Date du jour</param>
        /// <returns>Tâches</returns>
        public List<TaskEntry> GetTaskEntriesOnDay(DateTime date)
        {
            return this.taskEntries.GetEntriesOnDay(date).ToList();
        }

        /// <summary>
        /// Retourner les tâches d'une semaine
        /// </summary>
        /// <param name="date">Début de la semaine</param>
        /// <returns>Tâches</returns>
        public List<TaskEntry> GetTaskEntriesOnWeek(DateTime date)
        {
            return this.taskEntries.GetEntriesOnWeek(Utils.GetWeekStartDate(date)).ToList();
        }

        /// <summary>
        /// Retourner le nombre de tâches NON COMPLÉTÉES pour une journée et une matière spécifique
        /// </summary>
        /// <param name="calendarEntry">Entrée calendrier</param>
        /// <returns>Nombre de tâches NON COMPLÉTÉES</returns>
        public int GetTaskNumberForCalendarEntry(CalendarEntry calendarEntry)
        {
            int nb = 0;

            this.GetTaskEntriesOnDay(calendarEntry.StartTime).ForEach((entry) => {
                if ((entry.Course == calendarEntry.Course) && !entry.Completed) nb++;
            });

            return nb;
        }

        /// <summary>
        /// Retourner les entrées calendrier d'un jour
        /// </summary>
        /// <param name="date">Date du jour</param>
        /// <returns>Entrées calendrier</returns>
        public List<CalendarEntry> GetCalendarEntriesOnDay(DateTime date)
        {
            return this.calendarEntries.GetEntriesOnDay(date).ToList();
        }

        /// <summary>
        /// Retourner les entrées calendrier d'une semaine
        /// </summary>
        /// <param name="date">Début de la semaine</param>
        /// <returns>Entrées calendrier</returns>
        public List<CalendarEntry> GetCalendarEntriesOnWeek(DateTime date)
        {
            return this.calendarEntries.GetEntriesOnWeek(Utils.GetWeekStartDate(date)).ToList();
        }

        /// <summary>
        /// Retourner toutes les matières d'un jour
        /// </summary>
        /// <param name="date">Date du jour</param>
        /// <returns>Matières</returns>
        public List<Course> GetCoursesOnDay(DateTime date)
        {
            return this.calendarEntries.GetCoursesOnDay(date);
        }

        /// <summary>
        /// Vue actuelle
        /// </summary>
        public ViewType CurrentView
        {
            get => this.currentView;
            set
            {
                this.currentView = value;

                if (value == ViewType.Weekly)
                    this.currentDate = Utils.GetWeekStartDate(this.currentDate);

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
                    case ViewType.Daily:
                        return this.currentDate.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("fr-FR"));
                    case ViewType.Weekly:
                        return this.currentDate.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("fr-FR"))
                            + " à "
                            + Utils.GetWeekEndDate(this.currentDate).ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("fr-FR"));
                    default:
                        throw new Exception("Type vue inconnue : " + this.CurrentView);
                }
            }
        }

        /// <summary>
        /// Date actuelle en cours de visionnage
        /// </summary>
        public DateTime CurrentDateTime
        {
            get => this.currentDate;
        }

        /// <summary>
        /// Passer au jour, à la semaine, au mois suivant selon la vue actuelle
        /// </summary>
        public void NextDate()
        {
            switch (this.CurrentView)
            {
                case ViewType.Daily:
                    this.currentDate = this.currentDate.AddDays(1);
                    break;
                case ViewType.Weekly:
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
                case ViewType.Daily:
                    this.currentDate = this.currentDate.AddDays(-1);
                    break;
                case ViewType.Weekly:
                    this.currentDate = Utils.GetWeekStartDate(this.currentDate.AddDays(-1));
                    break;
                default:
                    throw new Exception("Type vue inconnue : " + this.CurrentView);
            }

            this.OnPropertyChanged("CurrentDate");
        }
    }
}
