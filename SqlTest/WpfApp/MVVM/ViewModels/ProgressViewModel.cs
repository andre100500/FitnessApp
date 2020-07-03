using Microsoft.Win32;
using Newtonsoft.Json;
using SqlTest.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp.MVVM.Models;
using WpfApp.Utils;

namespace WpfApp.MVVM.ViewModels
{
    public class ProgressViewModel : ViewModel, IPageViewModel
    { 
        public User CurentUser { set; get; }
        public string HeaderText { get { return $"#{CurentUser.Id} {CurentUser.Login}"; } }

        public string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                Notify("ErrorMessage"); 
            }
        }

        public ICommand OpenFileCommand { get; set; }
         

        public ICommand BackToHome { get; set; }
        public ICommand BackToExercise { get; set; }
        public ICommand SaveToFileProgress { get; set; }


        public ProgressViewModel()
        {
            OpenFileCommand = new SimpleCommand(OpenFile);
            SaveToFileProgress = new SimpleCommand(SaveToFile);
            BackToHome = new SimpleCommand(BackToHomeView);
            BackToExercise = new SimpleCommand(BackToExerciseView);
            CurentUser = SettingsProvider.Instance.CurentUser;
            CurentUser.ExerciseList = new List<Exercise>()
            {
                new Exercise{Name = "Crow ", Mass = 50,Count=5,CounterType=ExerciseType.Time,CurentProgress=ExercisProgress.InProcess },
                 new Exercise{Name = "Jhon", Mass = 25,Count=7,CounterType=ExerciseType.Count,CurentProgress=ExercisProgress.Done},
                  new Exercise{Name = "Roy", Mass = 30,Count=2,CounterType=ExerciseType.Count,CurentProgress=ExercisProgress.UnDone }
            };
            Notify("CurentUser");
        }

        private void OpenFile()
        {
            
            ErrorMessage = "";
            try
            {
                OpenFileDialog FileDialog = new OpenFileDialog();
                FileDialog.Filter = "JSON Documents | *.json";
                if (FileDialog.ShowDialog().Value)
                {
                    using (var reader = new StreamReader(FileDialog.FileName))
                    {
                        var data = reader.ReadToEnd();
                        CurentUser.ExerciseList = new List<Exercise>();
                        CurentUser.ExerciseList = JsonConvert.DeserializeObject<List<Exercise>>(data);
                        Notify("CurentUser");
                    }
                }
            }
            catch(Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }
        private async void SaveToFile()
        {
            ErrorMessage = "";
            try
            { 
                SaveFileDialog FileDialog = new SaveFileDialog();
                FileDialog.Filter = "JSON Documents | *.json";
                if (FileDialog.ShowDialog().Value)
                {
                    using (var writer = new StreamWriter(FileDialog.FileName))
                    {
                        var data = JsonConvert.SerializeObject(CurentUser.ExerciseList);
                        await writer.WriteLineAsync(data);
                    }
                }
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }
        private void BackToHomeView()
        {
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new HomeViewModel());
        }
        private void BackToExerciseView()
        {
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new ExerciseViewModel());
        } 
     
    }
}
