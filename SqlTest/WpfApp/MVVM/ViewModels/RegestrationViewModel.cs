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
        private string errorMessage;

        public User curentUser { set; get; }
        public String Gender { set; get; } = "Male";
        public ICommand RegestrationCommand { get; set; }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;

                Notify("ErrorMessage");

            }
        }
        public RegestrationViewModel()
        {
            RegestrationCommand = new SimpleCommand(Regestration);
            curentUser = new User();
        }

        private void Regestration()
        {
            switch (Gender)
            {
                case "Male":
                    curentUser.Gender = Sex.Male;
                    break;
                case "Female":
                    curentUser.Gender = Sex.Female;
                    break;
            }


            try
            {
                ErrorMessage = "";
                Validation.Login(curentUser.Login);
                Validation.Pass(curentUser.Pass);
                SQLHelper.Regestration(curentUser);
                SQLHelper.Login(curentUser.Login, curentUser.Pass);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
