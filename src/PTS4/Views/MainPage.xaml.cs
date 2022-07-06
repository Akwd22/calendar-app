using PTS4.ViewModels;
using PTS4.Views.CalendarTaskViews;
using PTS4.Views.Components.NavigationBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTS4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Type), "type")]
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.viewModel = this.BindingContext as MainViewModel;
            this.SubscribeMessageCenter();
            this.SwitchToDayView();
        }

        /// <summary>
        /// Type d'affichage : "calendar", "task"
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// S'inscrire aux messages
        /// </summary>
        private void SubscribeMessageCenter()
        {
            MessagingCenter.Subscribe<ViewSwitcher>(this, "SwitchToDayView", (sender) => this.SwitchToDayView());
            MessagingCenter.Subscribe<ViewSwitcher>(this, "SwitchToWeekView", (sender) => this.SwitchToWeekView());
            MessagingCenter.Subscribe<ViewSwitcher>(this, "SwitchToMonthView", (sender) => this.SwitchToMonthView());
            MessagingCenter.Subscribe<EditTaskPage>(this, "EditTaskPageDisappeared", (sender) => this.SwitchToDayView());
            MessagingCenter.Subscribe<AppShell>(this, "CalendarReset", (sender) => this.ResetCalendar());
            MessagingCenter.Subscribe<AppShell>(this, "TasksReset", (sender) => this.ResetTasks());
            MessagingCenter.Subscribe<ImportCalendarPage, String>(this, "CalendarFileImport", (sender, args) => this.LoadCalendar(args));
        }

        private void LoadCalendar(String file)
        {
            this.viewModel.LoadCalendar(file);
            this.SwitchToDayView();
        }

        private void ResetCalendar()
        {
            this.viewModel.ResetCalendar();
            this.SwitchToDayView();
        }

        private void ResetTasks()
        {
            this.viewModel.ResetTasks();
            this.SwitchToDayView();
        }

        private void SwitchToDayView()
        {
            this.HideAllViews();

            if (Type == "calendar")
            {
                this.DayCalendarView.RefreshView();
                this.DayCalendarView.IsVisible = true;
            }
            
            if (Type == "task")
            {
                this.DayTaskView.RefreshView();
                this.DayTaskView.IsVisible = true;
            }
        }

        private void SwitchToWeekView()
        {
            this.HideAllViews();

            if (Type == "calendar")
            {
                this.WeekCalendarView.RefreshView();
                this.WeekCalendarView.IsVisible = true;
            }

            if (Type == "task")
            {
                this.WeekTaskView.RefreshView();
                this.WeekTaskView.IsVisible = true;
            }
        }

        private void SwitchToMonthView()
        {
            throw new NotImplementedException();
        }

        private void HideAllViews()
        {
            this.DayCalendarView.IsVisible = false;
            this.WeekCalendarView.IsVisible = false;
            this.DayTaskView.IsVisible = false;
            this.WeekTaskView.IsVisible = false;
        }
    }
}