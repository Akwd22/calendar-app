using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTS4.Models
{
    /// <summary>
    /// Collection de tâches
    /// </summary>
    public class TaskEntries
    {
        private List<TaskEntry> taskEntries;
        private CalendarEntries calendarEntries;

        public TaskEntries(CalendarEntries calendarEntries)
        {
            this.taskEntries = new List<TaskEntry>();
            this.calendarEntries = calendarEntries;
        }

        /// <summary>
        /// Retourner toutes les tâches d'un jour spécifique
        /// </summary>
        /// <param name="date">Date de la journée</param>
        /// <returns>Tâches du jour</returns>
        public IEnumerable<TaskEntry> GetEntriesOnDay(DateTime date)
        {
            List<Course> courses = this.calendarEntries.GetCoursesOnDay(date);
            
            IEnumerable<TaskEntry> entries =
                from entry in this.taskEntries
                where (entry.StartTime.Date.Equals(date.Date) && entry.EndTime.Date.Equals(date.Date) && (entry.Repeat != TypeTaskRepeat.EACH_COURSE)) ||
                      ((entry.Repeat == TypeTaskRepeat.EACH_DAY) && (entry.StartTime.DayOfWeek == date.DayOfWeek)) ||
                      ((entry.Repeat == TypeTaskRepeat.EACH_COURSE) && (courses.Contains<Course>(entry.Course)))
                orderby entry.Completed, entry.StartTime
                select entry;

            return entries;
        }

        /// <summary>
        /// Retourner toutes les tâches d'une semaine
        /// </summary>
        /// <param name="date">Date de début de la semaine (lundi)</param>
        /// <returns>Tâches de la semaine</returns>
        public IEnumerable<TaskEntry> GetEntriesOnWeek(DateTime date)
        {
            // TODO : retourner les tâches avec REPEAT

            IEnumerable<TaskEntry> entries =
                from entry in this.taskEntries
                where ((entry.StartTime.Date >= date.Date) && (entry.EndTime.Date <= date.AddDays(6).Date))
                orderby entry.Completed, entry.StartTime
                select entry;

            return entries;
        }

        /// <summary>
        /// Retourner la tâche grâce à son GUID
        /// </summary>
        /// <param name="guid">GUID de la tâche</param>
        /// <returns>Tâche</returns>
        public TaskEntry GetEntryByGuid(String guid)
        {
            foreach (var task in this.taskEntries)
            {
                if (task.Guid == guid) return task;
            }

            throw new Exception("Aucune tâche correspondant au GUID " + guid + " existe.");
        }

        /// <summary>
        /// Ajouter une tâche à la collection
        /// </summary>
        /// <param name="entry">Tâche</param>
        public void AddEntry(TaskEntry entry)
        {
            this.taskEntries.Add(entry);
        }

        /// <summary>
        /// Réinitialiser la collection
        /// </summary>
        public void ClearEntries()
        {
            this.taskEntries.Clear();
        }

        /// <summary>
        /// Supprimer la tâche de la collection
        /// </summary>
        /// <param name="guid">GUID de la tâche</param>
        public void DeleteEntry(String guid)
        {
            this.taskEntries.Remove(this.GetEntryByGuid(guid));
        }

        public List<TaskEntry> GetEntries()
        {
            return this.taskEntries;
        }
    }
}
