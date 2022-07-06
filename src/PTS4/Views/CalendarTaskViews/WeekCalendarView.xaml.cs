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
    public partial class WeekCalendarView : ContentView
    {
        private MainViewModel viewModel;

        public WeekCalendarView()
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

            DateTime date = this.viewModel.CurrentDateTime;

            for (int i = 0; i < 7; i++)
            {
                EntryGroup group = new EntryGroup(date);
                this.CalendarEntryList.Children.Add(group);

                // Enrouler la liste si jour déjà passé
                if (date.Date < DateTime.Now.Date) group.SetRolled();

                foreach (var e in this.viewModel.GetCalendarEntriesOnDay(date))
                {
                    CalendarEntry entry = new CalendarEntry(e);
                    group.AddChild(entry);
                }

                date = date.AddDays(1);
            }
        }
    }
}