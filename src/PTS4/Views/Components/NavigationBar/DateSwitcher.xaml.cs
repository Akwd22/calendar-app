using PTS4.ViewModels;
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
    public partial class DateSwitcher : ContentView
    {
        private MainViewModel viewModel;

        public DateSwitcher()
        {
            InitializeComponent();
            this.viewModel = (MainViewModel)this.BindingContext;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.viewModel = (MainViewModel)this.BindingContext;
        }

        public void OnLeftButtonClicked(object sender, EventArgs e)
        {
            this.viewModel.PreviousDate();
            MessagingCenter.Send<DateSwitcher>(this, "DateChange");
        }

        public void OnRightButtonClicked(object sender, EventArgs e)
        {
            this.viewModel.NextDate();
            MessagingCenter.Send<DateSwitcher>(this, "DateChange");
        }
    }
}