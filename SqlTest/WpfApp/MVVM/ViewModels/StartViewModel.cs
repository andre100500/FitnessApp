using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.MVVM.Models;

namespace WpfApp.MVVM.ViewModels
{
    public class StartViewModel : ViewModel
    {
        #region Fields

        private ICommand _changePageCommand;
        private  IPageViewModel _currentPageViewModel;

        public static StartViewModel Instant { set; get; }

        #endregion

        public StartViewModel()
        {

            // Set starting page
            CurrentPageViewModel = new LoginViewModel();
            Instant = this;
        }

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new SimpleCommand<IPageViewModel>(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }


        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    Notify("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods

        public void ChangeViewModel(IPageViewModel viewModel)
        { 
            CurrentPageViewModel = viewModel;
        }

        #endregion
    }
}
