using SqlTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.MVVM.Models;

namespace WpfApp.MVVM.ViewModels
{
    public class LoginViewModel : ViewModel , IPageViewModel
    {
        private string errorMessage;

        public User CurentUser { get; set; }
        public ICommand LoginCommand { set; get; }
        public ICommand RegistrationCommand { set; get; }

        public string ErrorMessage 
        {
            get { return errorMessage; }
            set 
            {
                errorMessage = value;

                Notify("ErrorMessage");

            }
        }

        public LoginViewModel()
        {
            LoginCommand = new SimpleCommand(LogIn);
            RegistrationCommand = new SimpleCommand(Registration);
            CurentUser = new User();
        }

        /// <summary>
        /// Метод открытие страницы RegestrationViewModel
        /// </summary>
        private void Registration()
        {
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new RegestrationViewModel());
        }

        private void LogIn()
        {
            try
            {
                ErrorMessage = "";
                SQLHelper.Login(CurentUser);
                Validation.Login(CurentUser.Login);
                Validation.Pass(CurentUser.Pass);
                SettingsProvider.Instance.CurentUser = SQLHelper.Login(CurentUser); 

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new HomeViewModel());
        }
    }
}
