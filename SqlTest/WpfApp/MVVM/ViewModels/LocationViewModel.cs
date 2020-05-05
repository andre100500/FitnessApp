using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVVM.ViewModels
{
    public class LocationViewModel : ViewModel, IPageViewModel
    {
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
        public LocationViewModel()
        {

        }
    }
}
