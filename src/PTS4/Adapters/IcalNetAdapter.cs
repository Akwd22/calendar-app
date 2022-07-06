using Ical.Net;
using PTS4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PTS4.Adapters
{
    /// <summary>
    /// Adaptateur de calendrier pour la bibliothèque Ical.Net
    /// </summary>
    public class IcalNetAdapter : ICalendarAdapter
    {
        private Calendar calendar;
        private CalendarEntries entries;

        /// <param name="file">Chemin fichier ICS</param>
        public IcalNetAdapter(String file)
        {
            this.LoadFromFile(file);
        }

        public CalendarEntries GetAllEntries()
        {
            this.entries = new CalendarEntries();

            foreach (var e in this.calendar.Events)
            {
                Course course = Courses.GetCourse(e.Summary);
                CalendarEntry entry = new CalendarEntry(Guid.NewGuid().ToString(), course, e.Location, e.Start.Value, e.End.Value);
                this.entries.AddEntry(entry);
            }

            return this.entries;
        }

        /// <summary>
        /// Charger un calendrier depuis un fichier ICS
        /// </summary>
        /// <param name="file">Chemin fichier</param>
        private void LoadFromFile(string file)
        {
            if (!File.Exists(file)) throw new Exception("Le fichier " + file + " n'existe pas.");

            try
            {
                this.calendar = Calendar.Load(File.ReadAllText(file));
            }
            catch (Exception ex)
            {
                throw new Exception("Impossible de charger le fichier ICS : " + ex.Message);
            }
        }
    }
}
