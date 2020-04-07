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

        public User CurentUser { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand ExerciseCommand { get; set; }
        public ICommand PorgressCommand { get; set; }
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
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new ExerciseViewModel());
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new LocationViewModel());
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new ProgressViewModel());

        }


        private void Settings()
        {
            try
            {
                ErrorMessage = "";
                Validation.Pass(CurentUser.Pass);
                Validation.IsValidEmail(CurentUser.Email);
                Validation.Age(CurentUser.Age);
                Validation.Mass(CurentUser.Mass);
                Validation.Height(CurentUser.Height);
                SQLHelper.Settings(CurentUser);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
