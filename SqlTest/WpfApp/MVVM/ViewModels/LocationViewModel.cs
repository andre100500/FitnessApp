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
    public class LocationViewModel : ViewModel, IPageViewModel
    {
        public User CurentUser { set; get; }
        public string _errorMessage;
        public ICommand CloseComand { get; set; }


        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                Notify("ErrorMessage");

            }
        }
        public LocationViewModel()
        {
            CloseComand = new SimpleCommand(Close);
        }
        public void Close()
        {
            StartViewModel.Instant.ChangeViewModel((IPageViewModel) new HomeViewModel()); 
        }
    }
}
