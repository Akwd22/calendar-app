using PTS4.ViewModels;
using PTS4.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PTS4
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private MainViewModel viewModel;

        public AppShell()
        {
            InitializeComponent();
            this.viewModel = new MainViewModel();
            this.BindingContext = this.viewModel;
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ImportCalendarPage), typeof(ImportCalendarPage));
        }

        private async void ImportCalendarClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ImportCalendarPage");
        }

        private void ResetCalendarClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<AppShell>(this, "CalendarReset");
        }

        private void ResetTasksClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<AppShell>(this, "TasksReset");
        }
    }
}
