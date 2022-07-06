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
    public partial class WeekTaskView : ContentView
    {
        private MainViewModel viewModel;
        private int totalTasks = 0;
        private int completedTasks = 0;

        public WeekTaskView()
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
            this.totalTasks = 0;
            this.completedTasks = 0;

            this.TaskEntryList.Children.Clear();

            DateTime date = this.viewModel.CurrentDateTime;

            for (int i = 0; i < 7; i++)
            {
                EntryGroup group = new EntryGroup(date);
                this.TaskEntryList.Children.Add(group);

                // Enrouler la liste si jour déjà passé
                if (date.Date < DateTime.Now.Date) group.SetRolled();



                var entries = this.viewModel.GetTaskEntriesOnDay(date);
                this.totalTasks += entries.Count();

                foreach (var e in entries)
                {
                    TaskEntry entry = new TaskEntry(e);
                    group.AddChild(entry);
                    if (e.Completed) this.completedTasks++;
                }

                date = date.AddDays(1);
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