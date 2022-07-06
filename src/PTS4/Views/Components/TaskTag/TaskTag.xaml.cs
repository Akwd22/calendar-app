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
    public partial class TaskTag : ContentView
    {
        /// <summary>
        /// Référence vers le champ de texte à modifier
        /// </summary>
        public Entry Entry { get; set; }

        /// <summary>
        /// Texte du tag
        /// </summary>
        public String Text { get; set; }

        public TaskTag()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="text">Texte du tag</param>
        /// <param name="entry">Référence vers le champ de texte à modifier</param>
        public TaskTag(String text, Entry entry)
        {
            InitializeComponent();
            this.Entry = entry;
            this.Text = text;
            this.TagButton.Text = text;
        }

        private void OnTagButtonClicked(object sender, EventArgs e)
        {
            this.Entry.Text = this.Text + " ";
        }
    }
}