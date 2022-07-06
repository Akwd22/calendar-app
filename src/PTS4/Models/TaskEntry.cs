using System;
using System.Collections.Generic;
using System.Text;

namespace PTS4.Models
{
    /// <summary>
    /// Types de répétition de la tâche dans le calendrier
    /// </summary>
    public enum TypeTaskRepeat
    {
        NO_REPEAT,      // Pas de répétition
        EACH_COURSE,    // À chaque fois qu'il y a la matière
        EACH_DAY,       // À ce jour
    }

    /// <summary>
    /// Classe correspondant à une tâche
    /// </summary>
    public class TaskEntry
    {
        private readonly String guid;
        private String title;
        private DateTime startTime;
        private DateTime endTime;

        /// <param name="guid">Identificateur unique</param>
        /// <param name="startTime">Date de début</param>
        /// <param name="endTime">Date de fin</param>
        public TaskEntry(String guid, DateTime startTime, DateTime endTime)
        {
            this.guid = guid;
            this.Completed = false;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Repeat = TypeTaskRepeat.NO_REPEAT;
            this.Course = Courses.GetCourse("Aucune");
        }

        /// <summary>
        /// Identificateur unique
        /// </summary>
        public String Guid { get => this.guid; }

        /// <summary>
        /// Titre de la tâche
        /// </summary>
        public String Title
        {
            get => this.title;
            set
            {
                if (value is null) throw new Exception("Title ne peut être vide.");
                this.title = value;
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        public String Description { get; set; }

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

        /// <summary>
        /// Matière associée à la tâche
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// Tâche complétée ?
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Type de répétition de le tâche
        /// </summary>
        public TypeTaskRepeat Repeat { get; set; }
    }
}
