using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PTS4.ViewModels
{
    public class ImportCalendarViewModel : BaseViewModel
    {
        /// <summary>
        /// Chemins des fichiers iCalendar trouvés
        /// </summary>
        public List<String> Files { get; private set; }

        public ImportCalendarViewModel()
        {
            this.Files = new List<String>();
        }

        /// <summary>
        /// Rechercher sur l'appareil les fichiers iCalendar
        /// </summary>
        /// <returns>Chemins vers les fichiers iCalendar</returns>
        public void SearchCalendarFiles()
        {
            String path;

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    path = "sdcard/";
                    break;
                case Device.iOS:
                    path = "Documents/";
                    break;
                default:
                    throw new Exception("Plateforme " + Device.RuntimePlatform + " non supportée par cette application.");
            }

            this.Files.Clear();
            this.Files.AddRange(Directory.GetFiles(path, "*.ics", SearchOption.AllDirectories));
        }
    }
}
