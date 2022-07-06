using System;
using System.Collections.Generic;
using System.Text;

namespace PTS4.Models
{
    /// <summary>
    /// Classe correspondant à une entrée calendrier
    /// </summary>
    public class CalendarEntry
    {
        private readonly String guid;
        private Course course;
        private DateTime startTime;
        private DateTime endTime;

        /// <param name="guid">Identifiant unique</param>
        /// <param name="startTime">Date de début</param>
        /// <param name="endTime">Date de fin</param>
        public CalendarEntry(String guid, DateTime startTime, DateTime endTime)
        {
            this.guid = guid;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        /// <param name="course">Matière correspondant à l'évènement</param>
        /// <param name="location">Lieu de l'évènement</param>
        /// <param name="startTime">Date de début</param>
        /// <param name="endTime">Date de fin</param>
        public CalendarEntry(String guid, Course course, String location, DateTime startTime, DateTime endTime)
        {
            this.guid = guid;
            this.Course = course;
            this.Location = location;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        /// <summary>
        /// Identificateur unique
        /// </summary>
        public String Guid { get => this.guid; }

        /// <summary>
        /// Matière correspondant à l'évènement
        /// </summary>
        public Course Course {
            get => this.course;
            set
            {
                if (value is null) throw new Exception("Course ne peut être null.");
                this.course = value;
            }
        }

        /// <summary>
        /// Lieu de l'évènement
        /// </summary>
        public String Location { get; set; }

        /// <summary>
        /// Date de début
        /// </summary>
        public DateTime StartTime
        {
            get => this.startTime;
            set
            {
                //if (value > this.endTime) throw new Exception("StartTime ne peut être plus grand que EndTime");
                this.startTime = value;
            }
        }

        /// <summary>
        /// Date de fin
        /// </summary>
        public DateTime EndTime
        {
            get => this.endTime;
            set
            {
                //if (value < this.startTime) throw new Exception("EndTime ne peut être plus petit que EndTime");
                this.endTime = value;
            }
        }
    }
}
