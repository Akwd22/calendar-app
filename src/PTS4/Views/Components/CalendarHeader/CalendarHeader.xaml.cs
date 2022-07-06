using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTS4.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarHeader : ContentView
    {
        private bool unrolled = true;
        
        /// <summary>
        /// Référence vers la liste associée à l'en-tête
        /// </summary>
        public View List { get; set; }

        public CalendarHeader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enrouler la liste
        /// </summary>
        public void SetRolled()
        {
            this.unrolled = false;
            VisualStateManager.GoToState(this.CalendarHeaderFrame, "Closed");
            this.List.IsVisible = false;
        }

        private void OnCloseButtonClicked(object sender, EventArgs e)
        {
            if (this.unrolled)
            {
                VisualStateManager.GoToState(this.CalendarHeaderFrame, "Closed");
                this.List.IsVisible = false;
            }
            else
            {
                VisualStateManager.GoToState(this.CalendarHeaderFrame, "Normal");
                this.List.IsVisible = true;
            }

            this.unrolled = !this.unrolled;
        }
    }
}