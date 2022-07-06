using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTS4.Models
{
    /// <summary>
    /// Collection d'entrées calendrier
    /// </summary>
    public class CalendarEntries
    {
        private List<CalendarEntry> entries;

        public CalendarEntries()
        {
            this.entries = new List<CalendarEntry>();
        }

        /// <summary>
        /// Retourner tous les entrées calendrier d'un jour spécifique
        /// </summary>
        /// <param name="date">Date de la journée</param>
        /// <returns>Entrées calendrier du jour</returns>
        public IEnumerable<CalendarEntry> GetEntriesOnDay(DateTime date)
        {
            IEnumerable<CalendarEntry> entries =
                from entry in this.entries
                where (entry.StartTime.Date.Equals(date.Date) && entry.EndTime.Date.Equals(date.Date))
                orderby entry.StartTime
                select entry;

            return entries;
        }

        /// <summary>
        /// Retourner tous les entrées calendrier d'une semaine
        /// </summary>
        /// <param name="date">Date de début de la semaine (lundi)</param>
        /// <returns>Entrées calendrier de la semaine</returns>
        public IEnumerable<CalendarEntry> GetEntriesOnWeek(DateTime date)
        {
            IEnumerable<CalendarEntry> entries =
                from entry in this.entries
                where ((entry.StartTime.Date >= date.Date) && (entry.EndTime.Date <= date.AddDays(6).Date))
                orderby entry.StartTime
                select entry;

            return entries;
        }

        /// <summary>
        /// Retourner toutes les matières d'un jour
        /// </summary>
        /// <param name="date">Date du jour</param>
        /// <returns>Matières</returns>
        public List<Course> GetCoursesOnDay(DateTime date)
        {
            var entries = this.GetEntriesOnDay(date);
            var courses = new List<Course>();

            foreach (var entry in entries)
            {
                courses.Add(entry.Course);
            }

            return courses;
        }

        /// <summary>
        /// Retourner l'entrée calendrier grâce à son GUID
        /// </summary>
        /// <param name="guid">GUID de l'entrée calendrier</param>
        /// <returns>Entrée calendrier</returns>
        public CalendarEntry GetEntryByGuid(String guid)
        {
            foreach (var entry in this.entries)
            {
                if (entry.Guid == guid) return entry;
            }

            throw new Exception("Aucune entrée calendrier correspondant au GUID " + guid + " existe.");
        }

        /// <summary>
        /// Ajouter une entrée calendrier à la collection
        /// </summary>
        /// <param name="entry">Entrée calendrier</param>
        public void AddEntry(CalendarEntry entry)
        {
            this.entries.Add(entry);
        }

        /// <summary>
        /// Ajouter les entrées calendrier d'une autre collection
        /// </summary>
        /// <param name="entries">Collection d'entrées calendrier</param>
        public void AddEntries(CalendarEntries entries)
        {
            this.entries.Clear();
            this.entries.AddRange(entries.GetEntries());
        }

        /// <summary>
        /// Réinitialiser la collection
        /// </summary>
        public void ClearEntries()
        {
            this.entries.Clear();
        }

        /// <summary>
        /// Retourner toutes les entrées calendrier
        /// </summary>
        /// <returns>Entrées calendrier</returns>
        public List<CalendarEntry> GetEntries()
        {
            return this.entries;
        }
    }
}
