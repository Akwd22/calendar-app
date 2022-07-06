using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PTS4.Models
{
    /// <summary>
    /// Classe correspondant à une matière
    /// </summary>
#pragma warning disable CS0659 // 'Course' se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
    public class Course
#pragma warning restore CS0659 // 'Course' se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
    {
        public Course(String title)
        {
            this.Title = title;
        }

        /// <summary>
        /// Titre de la matière
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// Couleur associée à la matière
        /// </summary>
        public Color Color { get; set; }

        public override string ToString()
        {
            return this.Title;
        }

        public override bool Equals(object obj)
        {
            return (this.Title == ((Course)obj).Title);
        }
    }
}
