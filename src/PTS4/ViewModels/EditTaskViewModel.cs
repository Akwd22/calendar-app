using PTS4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS4.ViewModels
{
    public class EditTaskViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        private readonly TaskEntries entries;
        private TaskEntry entry;

        /// <param name="entries">Tâches</param>
        /// <param name="guid">GUID de la tâche en cours d'édition, null si aucune</param>
        public EditTaskViewModel(MainViewModel mainViewModel, String guid = null)
        {
            this.mainViewModel = mainViewModel;
            this.entries = this.mainViewModel.GetTaskEntries();

            if (guid != null)
            {
                this.LoadEntry(guid);
            }
            else
            {
                this.ClearEntry();
            }
        }

        /// <summary>
        /// Réintialiser les données de la vue modèle
        /// </summary>
        public void ClearEntry()
        {
            this.entry          = null;
            this.Title          = "";
            this.Description    = "";
            this.Completed      = false;
            this.StartTime      = DateTime.Now;
            this.EndTime        = DateTime.Now;
            this.Repeat         = TypeTaskRepeat.NO_REPEAT;
            this.Course         = "Aucune";
        }

        /// <summary>
        /// Charger les données d'une tâche existante
        /// </summary>
        /// <param name="guid">GUID de la tâche en cours d'édition</param>
        public void LoadEntry(String guid)
        {
            this.entry          = this.entries.GetEntryByGuid(guid);
            this.Title          = this.entry.Title;
            this.Description    = this.entry.Description;
            this.Completed      = this.entry.Completed;
            this.StartTime      = this.entry.StartTime;
            this.Repeat         = this.entry.Repeat;
            this.Course         = (this.entry.Course.Title == "") ? "Aucune" : this.entry.Course.Title;
        }

        /// <summary>
        /// Charger les données d'une entrée calendrier
        /// </summary>
        /// <param name="guid">GUID de l'entrée calendrier source</param>
        public void LoadEvent(String guid)
        {
            var entry       = this.mainViewModel.GetCalendarEntries().GetEntryByGuid(guid);
            this.StartTime  = entry.StartTime;
            this.StartTime  = entry.EndTime;
            this.Course     = (entry.Course.Title == "") ? "Aucune" : entry.Course.Title;
        }

        public String Title { get; set; }
        public String Description { get; set; }
        public bool Completed { get; set; }

        private DateTime startTime;
        public DateTime StartTime
        {
            get => this.startTime;
            set
            {
                this.startTime = value;
                this.EndTime = value;
            }
        }

        public DateTime EndTime { get; set; }
        public TypeTaskRepeat Repeat { get; set; }

        private String course = "Aucune";
        public String Course {
            get => this.course;
            set => this.course = value;
        }

        /// <summary>
        /// Indiquer si les données renseignées sont valides
        /// </summary>
        /// <returns>true si valide</returns>
        public bool ValidateData()
        {
            return (this.StartTime != null)
                && (this.EndTime != null)
                && (this.StartTime <= this.EndTime)
                && (this.Title != "")
                && (this.Title != null);
        }

        /// <summary>
        /// Créer et sauvegarder la tâche
        /// </summary>
        public void CreateTask()
        {
            var entry           = new TaskEntry(Guid.NewGuid().ToString(), this.StartTime, this.EndTime);
            entry.Course        = Courses.GetCourse(this.Course);
            entry.Title         = this.Title;
            entry.Description   = this.Description;
            entry.Completed     = this.Completed;
            entry.Repeat        = this.Repeat;
            this.entries.AddEntry(entry);
        }

        /// <summary>
        /// Mettre à jour et sauvegarder la tâche
        /// </summary>
        public void UpdateTask()
        {
            var entry           = this.entry;
            entry.Course        = Courses.GetCourse(this.Course);
            entry.Title         = this.Title;
            entry.Description   = this.Description;
            entry.Completed     = this.Completed;
            entry.StartTime     = this.StartTime;
            entry.EndTime       = this.EndTime;
            entry.Repeat        = this.Repeat;
        }

        /// <summary>
        /// Supprimer la tâche existante
        /// </summary>
        public void DeleteTask()
        {
            this.entries.DeleteEntry(this.entry.Guid);
        }

        /// <summary>
        /// Retourner les matières du jour sélectionné
        /// </summary>
        /// <returns>Matières</returns>
        public List<Course> GetCourses()
        {
            return this.mainViewModel.GetCoursesOnDay(this.StartTime);
        }
    }
}
