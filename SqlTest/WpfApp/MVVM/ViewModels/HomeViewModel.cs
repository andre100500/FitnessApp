using SqlTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.MVVM.Models;

namespace WpfApp.MVVM.ViewModels
{
    public class HomeViewModel : ViewModel , IPageViewModel
    {
        public string _errorMessage;

        public User CurentUser { get { return SettingsProvider.Instance.CurentUser; }  } 
        //#12 Login
        public string HeaderText { get { return $"#{CurentUser.Id}  {CurentUser.Login}"; } }

        public ICommand SettingsCommand { get; set; }
        public ICommand ExerciseCommand { get; set; }
        public ICommand AdvancementCommand { get; set; }
        public ICommand LocationCommand { get; set; }



        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                Notify("ErrorMessage");

            }
        }



        public HomeViewModel ()
        {
            SettingsCommand = new SimpleCommand(Settings);
            ExerciseCommand = new SimpleCommand(Exercise);
            AdvancementCommand = new SimpleCommand(Advancement);
            LocationCommand = new SimpleCommand(Location); 
        }
        private void Settings()
        {
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new SettingsViewModel());
        }
        private void Exercise()
        {
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new ExerciseViewModel());
        }
        private void Location()
        {
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new LocationViewModel());
        }
        private void Advancement()
        {
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new ProgressViewModel());
        }
    }
}
