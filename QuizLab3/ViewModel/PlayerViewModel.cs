using QuizLab3.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QuizLab3.ViewModel
{
    class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        public DispatcherTimer timer;

        private ConfigurationViewModel _currentQuestion;

        private int _timeRemaining;
        public string TimeRemainingDisplay => TimeSpan.FromSeconds(TimeRemaining).ToString(@"ss");
        public ConfigurationViewModel CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                RaisePropertyChanged();
            }
        }
        public int TimeRemaining
        {
            get => _timeRemaining;
             set 
            {
                _timeRemaining = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(TimeRemainingDisplay));

            }
        }

        public DelegateCommand UpdateButtonCommand { get; } //måste gå o binda mot så inte bara field

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel) //
        {
            this.mainWindowViewModel = mainWindowViewModel;

            TimeRemaining = mainWindowViewModel?.ActivePack?.TimeLimitInSeconds ?? 0;
            
            this.timer = new DispatcherTimer(); //skapar ett objekt av Timer //sätt intervall och tick för
            timer.Interval = TimeSpan.FromSeconds(1); //skapar en timespan som är en sekund
            timer.Tick += Timer_Tick; //event += så kommer det upp förslag på eventhandler
                                      //timer.Start(); //sätt igång timer

        }

   
        private void Timer_Tick(object? sender, EventArgs e)
        {
            //TimeRemaining = mainWindowViewModel?.ActivePack?.TimeLimitInSeconds ?? 0;

            if (TimeRemaining > 0)
            {
                TimeRemaining--; 
            }
            else
            {
                timer.Stop(); 
         
            }
        }
    }
}
