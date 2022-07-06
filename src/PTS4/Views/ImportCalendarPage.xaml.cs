using PTS4.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTS4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImportCalendarPage : ContentPage
    {
        private ImportCalendarViewModel viewModel;

        public ImportCalendarPage()
        {
            InitializeComponent();
            this.viewModel = new ImportCalendarViewModel(); ;
            this.BindingContext = this.viewModel;
            this.viewModel.SearchCalendarFiles();
        }

        private async void ImportClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<ImportCalendarPage, String>(this, "CalendarFileImport", this.FileListView.SelectedItem as String);
            await Shell.Current.GoToAsync("..");
        }
    }
}