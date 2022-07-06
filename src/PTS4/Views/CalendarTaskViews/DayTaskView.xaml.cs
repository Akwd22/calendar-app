using PTS4.ViewModels;
using PTS4.Views.Components;
using PTS4.Views.Components.NavigationBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTS4.Views.CalendarTaskViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayTaskView : ContentView
    {
        private MainViewModel viewModel;
        private int totalTasks = 0;
        private int completedTasks = 0;

        public DayTaskView()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<DateSwitcher>(this, "DateChange", (sender) => this.RefreshView());

            MessagingCenter.Subscribe<TaskEntry>(this, "TaskComplete", (sender) => {
                this.completedTasks++;
                this.RefreshCompletedCounter();
            });

            MessagingCenter.Subscribe<TaskEntry>(this, "TaskUncomplete", (sender) => {
                this.completedTasks--;
                this.RefreshCompletedCounter();
            });
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.viewModel = (MainViewModel)this.BindingContext;
            this.RefreshView();
        }

        /// <summary>
        /// Actualiser les tâches
        /// </summary>
        public void RefreshView()
        {
            this.TaskEntryList.Children.Clear();

            var entries = this.viewModel.GetTaskEntriesOnDay(this.viewModel.CurrentDateTime);

            this.totalTasks     = entries.Count();
            this.completedTasks = 0;

            foreach (var entry in entries)
            {
                this.TaskEntryList.Children.Add(new TaskEntry(entry));
                if (entry.Completed) this.completedTasks++;
            }

            this.RefreshCompletedCounter();
        }

        /// <summary>
        /// Actualiser le compteur de tâches complétées
        /// </summary>
        public void RefreshCompletedCounter()
        {
            this.CompletedTaskLabel.Text = $"{this.completedTasks}/{this.totalTasks} terminées";
        }
    }
}