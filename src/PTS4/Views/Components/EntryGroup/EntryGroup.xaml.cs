using PTS4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTS4.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryGroup : ContentView
    {
        public EntryGroup()
        {
            InitializeComponent();
        }

        public EntryGroup(DateTime date)
        {
            InitializeComponent();
            this.BindingContext = new EntryGroupViewModel(date);
        }

        /// <summary>
        /// Enrouler la liste
        /// </summary>
        public void SetRolled()
        {
            this.CalendarHeader.SetRolled();
        }

        public void AddChild(View child)
        {
            this.EntryList.Children.Add(child);
        }
    }
}