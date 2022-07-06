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
    public partial class TaskEntry : ContentView
    {
        private TaskEntryViewModel viewModel;

        public TaskEntry()
        {
            InitializeComponent();
        }

        /// <param name="entry">Tâche</param>
        public TaskEntry(Models.TaskEntry entry)
        {
            InitializeComponent();
            this.viewModel = new TaskEntryViewModel(entry);
            this.BindingContext = this.viewModel;
        }

        private void OnCompleteButtonClicked(object sender, EventArgs e)
        {
            if (this.viewModel.Completed)
            {
                VisualStateManager.GoToState(this.TaskEntryFrame, "Normal");
                MessagingCenter.Send<TaskEntry>(this, "TaskUncomplete");
                MessagingCenter.Send<TaskEntry, String>(this, "TaskUncomplete", this.viewModel.Course);
            }
            else
            {
                VisualStateManager.GoToState(this.TaskEntryFrame, "Completed");
                MessagingCenter.Send<TaskEntry>(this, "TaskComplete");
                MessagingCenter.Send<TaskEntry, String>(this, "TaskComplete", this.viewModel.Course);
            }
                
            this.viewModel.Completed = !this.viewModel.Completed;
        }

        /// <summary>
        /// Rediriger vers la page d'édition de la tâche à l'appuie
        /// </summary>
        private async void OnTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//EditTaskPage?task={this.viewModel.Guid}");
        }
    }
}