using SqlTest.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp.MVVM.Models;

namespace WpfApp.MVVM.ViewModels
{
    public class RegestrationViewModel : ViewModel , IPageViewModel
    {
        private string _errorMessage;

        public User CurentUser { set; get; } 
        public String Age { set; get; }
        public String Gender { set; get; } = "Male"; 
        public ICommand RegestrationCommand { get; set; }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value; 
                Notify("ErrorMessage");

            }
        }
        public RegestrationViewModel()
        {
            RegestrationCommand = new SimpleCommand(Regestration);
            CurentUser = new User(); 
        }

        private void Regestration()
        {
            switch (Gender)
            {
                case "Male":
                    CurentUser.Gender = Sex.Male;
                    break;
                case "Female":
                    CurentUser.Gender = Sex.Female;
                    break;
            }


            try
            {
                ErrorMessage = "";
                Validation.IsValidEmail(CurentUser.Email);
                Validation.Login(CurentUser.Login);
                Validation.Pass(CurentUser.Pass);
                Validation.Age(CurentUser.Age);
                SQLHelper.Regestration(CurentUser);
                SQLHelper.Login(CurentUser);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
