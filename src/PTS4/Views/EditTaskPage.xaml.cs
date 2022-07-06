using PTS4.Models;
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

namespace PTS4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(TaskGuid), "task")]
    [QueryProperty(nameof(EventGuid), "event")]
    public partial class EditTaskPage : ContentPage
    {
        private EditTaskViewModel viewModel;

        /// <summary>
        /// Indiquer si la vue est en train de charger les données d'une tâche existante
        /// </summary>
        private bool isLoading;

        /// <summary>
        /// GUID de la tâche en cours d'édition<br/>
        /// null si création d'une nouvelle tâche
        /// </summary>
        public String TaskGuid { get; set; }

        /// <summary>
        /// GUID de l'entrée calendrier source<br/>
        /// null si création d'une nouvelle tâche à partir de rien
        /// </summary>
        public String EventGuid { get; set; }

        public EditTaskPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.viewModel is null)
            {
                this.viewModel = new EditTaskViewModel(((MainViewModel)this.BindingContext), this.TaskGuid); ;
                this.BindingContext = this.viewModel;
            }
        }

        private void PopulateRepeatPicker()
        {
            this.RepeatPicker.Items.Clear();
            this.RepeatPicker.Items.Add("Aucune");
            this.RepeatPicker.Items.Add("À chaque fois qu'il y a cette matière");
            this.RepeatPicker.Items.Add("À ce jour");
            this.RepeatPicker.SelectedIndex = 0;
        }

        private void PopulateCoursePicker()
        {
            this.CoursePicker.Items.Clear();
            this.CoursePicker.Items.Add("Aucune");

            foreach (var Course in this.viewModel.GetCourses())
            {
                this.CoursePicker.Items.Add(Course.Title);
            }

            this.CoursePicker.SelectedIndex = 0;
        }

        private void GenerateTaskTags()
        {
            this.TaskTagList.Children.Clear();
            this.TaskTagList.Children.Add(new TaskTag("Partiel", this.TitleEntry));
            this.TaskTagList.Children.Add(new TaskTag("DS", this.TitleEntry));
            this.TaskTagList.Children.Add(new TaskTag("IE", this.TitleEntry));
            this.TaskTagList.Children.Add(new TaskTag("DM", this.TitleEntry));
            this.TaskTagList.Children.Add(new TaskTag("TP", this.TitleEntry));
            this.TaskTagList.Children.Add(new TaskTag("TD", this.TitleEntry));
            this.TaskTagList.Children.Add(new TaskTag("Oral", this.TitleEntry));
            this.TaskTagList.Children.Add(new TaskTag("Exposé", this.TitleEntry));
            this.TaskTagList.Children.Add(new TaskTag("Colle", this.TitleEntry));
        }

        private async void ValidateButtonClicked(object sender, EventArgs e)
        {
            if (this.viewModel.ValidateData())
            {
                if (this.IsEditingTask())
                    this.viewModel.UpdateTask();
                else
                    this.viewModel.CreateTask();

                await Shell.Current.GoToAsync("//TaskMainPage");
            }
        }

        private async void DeleteButtonClicked(object sender, EventArgs e)
        {
            this.viewModel.DeleteTask();
            await Shell.Current.GoToAsync("//TaskMainPage");
        }

        private void RepeatPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.viewModel is null || this.isLoading) return;

            switch (this.RepeatPicker.SelectedIndex)
            {
                case 0:
                    this.viewModel.Repeat = TypeTaskRepeat.NO_REPEAT;
                    break;
                case 1:
                    this.viewModel.Repeat = TypeTaskRepeat.EACH_COURSE;
                    break;
                case 2:
                    this.viewModel.Repeat = TypeTaskRepeat.EACH_DAY;
                    break;
                default:
                    break;
            }
        }

        private void CoursePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.viewModel is null || this.isLoading) return;

            this.viewModel.Course = (String)this.CoursePicker.SelectedItem;
        }

        private void DatePickerDateSelected(object sender, DateChangedEventArgs e)
        {
            this.PopulateCoursePicker();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.isLoading = true;

            this.GenerateTaskTags();
            this.PopulateRepeatPicker();
            this.PopulateCoursePicker();

            if (!this.IsEditingTask() && !this.HasEventSource())
            {
                this.Title = "Créer une tâche";
                this.ValidateButton.Text = "Créer";
                this.DeleteButton.IsVisible = false;
            }
            else if (this.HasEventSource())
            {
                this.Title = "Créer une tâche";
                this.ValidateButton.Text = "Créer";
                this.DeleteButton.IsVisible = false;
                this.viewModel.LoadEvent(this.EventGuid);
                this.PopulateCoursePicker();
            }
            else
            {
                this.Title = "Modifier une tâche";
                this.ValidateButton.Text = "Modifier";
                this.DeleteButton.IsVisible = true;
                this.viewModel.LoadEntry(this.TaskGuid);
                this.PopulateCoursePicker();
            }

            this.LoadDataFromViewModel();
            this.isLoading = false;
        }

        /// <summary>
        /// Remplir les champs à partir des données du vue modèle
        /// </summary>
        private void LoadDataFromViewModel()
        {
            this.DatePicker.Date            = this.viewModel.StartTime;
            this.CoursePicker.SelectedItem  = this.viewModel.Course;
            this.TitleEntry.Text            = this.viewModel.Title;
            this.DescriptionEntry.Text      = this.viewModel.Description;

            switch (this.viewModel.Repeat)
            {
                case TypeTaskRepeat.NO_REPEAT:
                    this.RepeatPicker.SelectedItem = "Aucune";
                    break;
                case TypeTaskRepeat.EACH_COURSE:
                    this.RepeatPicker.SelectedItem = "À chaque fois qu'il y a cette matière";
                    break;
                case TypeTaskRepeat.EACH_DAY:
                    this.RepeatPicker.SelectedItem = "À ce jour";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Indiquer si on est en train d'éditer une tâche existante
        /// </summary>
        /// <returns></returns>
        public bool IsEditingTask()
        {
            return this.TaskGuid != null;
        }

        /// <summary>
        /// Indiquer s'il y a une source entrée calendrier
        /// </summary>
        /// <returns></returns>
        public bool HasEventSource()
        {
            return this.EventGuid != null;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.TaskGuid = null;
            this.EventGuid = null;
            this.viewModel.ClearEntry();
            MessagingCenter.Send<EditTaskPage>(this, "EditTaskPageDisappeared");
        }
    }
}