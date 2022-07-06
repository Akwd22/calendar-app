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
    public partial class TaskMainPage : ContentPage
    {
        public TaskMainPage()
        {
            InitializeComponent();
            Shell.Current.GoToAsync("/MainPage?type=task");
        }
    }
}