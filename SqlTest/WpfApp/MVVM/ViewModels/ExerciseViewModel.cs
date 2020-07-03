using Org.BouncyCastle.Asn1.Cms;
using SqlTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WpfApp.MVVM.Models;
using WpfApp.Utils;

namespace WpfApp.MVVM.ViewModels
{
    public class ExerciseViewModel : ViewModel , IPageViewModel
    {
        public string _errorMessage;
        public User CurentUser { get; set; }
        public ICommand StartOrPauseCommand { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand ProgressCommand { get; set; }
        public string _timers;
        /// <summary>
        /// Обновления таймера в строке lable = name(Timers)
        /// </summary>
        public string Timers { 
            get 
            { 
                return _timers;
            } 
            set 
            {
                _timers = value;
                Notify("Timers");
            } 
        }
        DispatcherTimer dTimer = new DispatcherTimer();

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                Notify("ErrorMessage");

            }
        }
        public ExerciseViewModel()
        {
            dTimer.Interval = TimeSpan.FromSeconds(1);
            dTimer.Tick += DTimer_Tick;
            Timers = "";
            StartOrPauseCommand = new SimpleCommand(StartOrPause);
            CurentUser = SettingsProvider.Instance.CurentUser;
            HomeCommand = new SimpleCommand(Home);
            ProgressCommand = new SimpleCommand(Progress);
            CurentUser.ExerciseList = new List<Exercise>()
            {
                new Exercise{CurentProgress=0},
                new Exercise{CurentProgress=(ExercisProgress)1},
                new Exercise{CurentProgress=(ExercisProgress)2}
            };
            //Exercise CurentExercise = CurentUser.ExerciseList.FirstOrDefault((ex) => ex.CurentProgress == ExercisProgress.InProcess);
            //Timers = CurentExercise.Count.ToString();
        }
        public void StartOrPause()
        {

            dTimer.IsEnabled = !dTimer.IsEnabled; 
            if(dTimer.IsEnabled)
            {
                DoneCount = 0;
                dTimer.Start();
            }
            else
            {
                DoneCount = 0;
                Timers = "";
                dTimer.Stop();
            }
                
        }
        public void Home()
        {
           StartViewModel.Instant.ChangeViewModel((IPageViewModel)new HomeViewModel());
        }
        public void Progress()
        {
            StartViewModel.Instant.ChangeViewModel((IPageViewModel)new ProgressViewModel());
        }

        private int DoneCount = 0;

        private void DTimer_Tick(object sender, EventArgs e)
        {
            DoneCount++;
            Timers = $"{DoneCount} sec"; 
        }
    }
}
