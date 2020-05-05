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
    public class SettingsViewModel : ViewModel, IPageViewModel
    {
        private string _errorMessage;
        public User CurentUser { get; set;  }
        public ICommand SettingsCommand { get; set; }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                Notify("ErrorMessage");
            }
        }
        public SettingsViewModel()
        {
            SettingsCommand = new SimpleCommand(SaveSettings);
            CurentUser = new User();
            CurentUser.Id = SettingsProvider.Instance.CurentUser.Id;
           // CurentUser.Login = SettingsProvider.Instance.CurentUser.Login;
            CurentUser.Pass = "You password";
            CurentUser.Mass = 40d;
            CurentUser.Height = 160d;
            CurentUser.Age = 18;
        }

        public void SaveSettings()
        {
            try
            {
                ErrorMessage = "";
                Validation.Pass(CurentUser.Pass);
                Validation.Age(CurentUser.Age);
                Validation.Mass(CurentUser.Mass);
                Validation.Height(CurentUser.Height);
                SQLHelper.SaveSettings(CurentUser);
                SQLHelper.Settings(CurentUser);
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new HomeViewModel());
        }
    }
}
