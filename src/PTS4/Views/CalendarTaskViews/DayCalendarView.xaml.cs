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
    public partial class DayCalendarView : ContentView
    {
        private MainViewModel viewModel;

        public DayCalendarView()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<DateSwitcher>(this, "DateChange", (sender) => this.RefreshView());
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.viewModel = (MainViewModel)this.BindingContext;
            this.RefreshView();
        }

        /// <summary>
        /// Actualiser les entrées calendrier
        /// </summary>
        public void RefreshView()
        {
            this.CalendarEntryList.Children.Clear();

            foreach (var entry in this.viewModel.GetCalendarEntriesOnDay(this.viewModel.CurrentDateTime))
            {
                var taskNumber = this.viewModel.GetTaskNumberForCalendarEntry(entry);
                this.CalendarEntryList.Children.Add(new CalendarEntry(entry, taskNumber));
            }
        }
    }
}