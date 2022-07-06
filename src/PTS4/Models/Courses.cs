using System;
using System.Collections.Generic;
using System.Text;

namespace PTS4.Models
{
    /// <summary>
    /// Multiton Collection de matières
    /// </summary>
    public class Courses
    {
        private static Dictionary<String, Course> courses = new Dictionary<String, Course>();

        /// <summary>
        /// Retourner toutes les matières
        /// </summary>
        /// <returns>Matières</returns>
        public static Course[] GetCourses()
        {
            Course[] list = new Course[Courses.courses.Count];
            Courses.courses.Values.CopyTo(list, 0);
            return list;
        }

        /// <summary>
        /// Retourner une matière par son titre
        /// </summary>
        /// <param name="title">Titre de la matière</param>
        /// <returns>Matière correspondante</returns>
        public static Course GetCourse(String title)
        {
            Course course;

            if (!Courses.courses.TryGetValue(title, out course))
            {
                course = new Course(title == "Aucune" ? "" : title);
                Courses.courses.Add(title, course);
            }

            return course;
        }
    }
}
