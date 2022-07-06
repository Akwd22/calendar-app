using PTS4.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PTS4.ViewModels
{
    public class TaskEntryViewModel : BaseViewModel
    {
        private TaskEntry entry;

        public TaskEntryViewModel(TaskEntry entry)
        {
            this.entry = entry;
        }

        /// <summary>
        /// GUID de la tâche
        /// </summary>
        public String Guid
        {
            get => this.entry.Guid;
        }

        /// <summary>
        /// Titre de la tâche
        /// </summary>
        public String Title
        {
            get => this.entry.Title;
        }

        /// <summary>
        /// Description
        /// </summary>
        public String Description
        {
            get => this.entry.Description;
        }

        /// <summary>
        /// Tâche complétée ?
        /// </summary>
        public bool Completed
        {
            get => this.entry.Completed;
            set => this.entry.Completed = value;
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

        /// <summary>
        /// Titre de la matière associée à la tâche
        /// </summary>
        public String Course
        {
            get => this.entry.Course.Title;
        }

        /// <summary>
        /// Alias pour avoir le titre de la matière associée
        /// </summary>
        public String Subtitle
        {
            get => this.Course;
        }

        /// <summary>
        /// Matière associée à la tâche
        /// </summary>
        public TypeTaskRepeat Repeat
        {
            get => this.entry.Repeat;
            set => this.entry.Repeat = value;
        }
    }
}
