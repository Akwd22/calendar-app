using PTS4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTS4.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarEntry : ContentView
    {
        private CalendarEntryViewModel viewModel;
        private int nbRemainingTasks = 0;

        public CalendarEntry()
        {
            InitializeComponent();
        }

        /// <param name="entry">Entrée calendrier</param>
        /// <param name="nbTasks">Nombre de tâches associées à l'entrée calendrier</param>
        public CalendarEntry(Models.CalendarEntry entry, int nbTasks = 0)
        {
            InitializeComponent();
            this.viewModel = new CalendarEntryViewModel(entry);
            this.BindingContext = this.viewModel ;
            this.nbRemainingTasks = nbTasks;
            this.RefreshRemainingCounter();

            MessagingCenter.Subscribe<TaskEntry, String>(this, "TaskComplete", (sender, args) =>
            {
                // Vérifier que le message nous concerne bien
                // en regardant la matière est la même
                if (this.viewModel.Summary == args)
                {
                    this.nbRemainingTasks--;
                    this.RefreshRemainingCounter();
                }
            });

            MessagingCenter.Subscribe<TaskEntry, String>(this, "TaskUncomplete", (sender, args) =>
            {
                // Vérifier que le message nous concerne bien
                // en regardant la matière est la même
                if (this.viewModel.Summary == args)
                {
                    this.nbRemainingTasks++;
                    this.RefreshRemainingCounter();
                }
            });
        }

        /// <summary>
        /// Rediriger vers la page de création d'une tâche avec comme source,
        /// cette entrée calendrier
        /// </summary>
        private async void OnTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//EditTaskPage?event={this.viewModel.Guid}");
        }

        /// <summary>
        /// Actualiser le compteur de tâches non complétées
        /// </summary>
        private void RefreshRemainingCounter()
        {
            if (this.nbRemainingTasks > 0)
                this.TaskNumberLabel.Text = this.nbRemainingTasks + " tâche(s) restante(s)";
            else
                this.TaskNumberLabel.Text = "";
        }
    }
}