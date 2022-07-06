using PTS4.ViewModels;
using PTS4.Views.CalendarTaskViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTS4.Views.Components.NavigationBar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewSwitcher : ContentView
    {
        private MainViewModel viewModel;
        private Button selectedBtn;

        public ViewSwitcher()
        {
            InitializeComponent();
            this.SubscribeMessageCenter();
            this.selectedBtn = this.DayViewButton;
            this.CheckViewButton(selectedBtn);
        }

        /// <summary>
        /// S'inscrire aux messages
        /// </summary>
        private void SubscribeMessageCenter()
        {
            MessagingCenter.Subscribe<ViewSwitcher>(this, "SwitchToDayView", (sender) => this.CheckViewButton(this.DayViewButton));
            MessagingCenter.Subscribe<ViewSwitcher>(this, "SwitchToWeekView", (sender) => this.CheckViewButton(this.WeekViewButton));
            MessagingCenter.Subscribe<ViewSwitcher>(this, "SwitchToMonthView", (sender) => this.CheckViewButton(this.MonthViewButton));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.viewModel = (MainViewModel)this.BindingContext;
        }

        public void OnDayViewButtonClick(object sender, EventArgs e)
        {
            this.CheckViewButton((Button)sender);
            this.viewModel.CurrentView = ViewType.Daily;
            MessagingCenter.Send<ViewSwitcher>(this, "SwitchToDayView");
        }

        public void OnWeekViewButtonClick(object sender, EventArgs e)
        {
            this.CheckViewButton((Button)sender);
            this.viewModel.CurrentView = ViewType.Weekly;
            MessagingCenter.Send<ViewSwitcher>(this, "SwitchToWeekView");
        }

        public void OnMonthViewButtonClick(object sender, EventArgs e)
        {
            this.CheckViewButton((Button)sender);
            this.viewModel.CurrentView = ViewType.Monthly;
            MessagingCenter.Send<ViewSwitcher>(this, "SwitchToMonthView");
        }

        public void CheckViewButton(Button btn)
        {
            if (this.selectedBtn != null) VisualStateManager.GoToState(this.selectedBtn, "Normal");
            this.selectedBtn = btn;
            VisualStateManager.GoToState(this.selectedBtn, "Checked");
        }
    }
}