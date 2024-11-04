using ListShuffle;
using QuizLab3.Command;
using QuizLab3.Model;
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
        public QuestionPackViewModel? ActivePack { get => mainWindowViewModel.ActivePack; }
        public List<string> ShuffledQuestions { get; set; }
        public List<Question> ShuffledAnswers { get; set; }

        public int TotalQuestions { get => ActivePack.Questions?.Count ?? 0; } //funkar inte än
            //Add a Prop/field thats readOnly and gets the count and the index of Number

   




        private int _timeRemaining;
        public string TimeRemainingDisplay => TimeSpan.FromSeconds(TimeRemaining).ToString($"ss");

        // TODO: Make this as minuts or change the slider.
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
            ShuffledQuestions = new List<string>(); //skapar en ny lista
            ShuffleQuestions();
            //TimeRemaining = mainWindowViewModel?.ActivePack?.TimeLimitInSeconds ?? 0;

            this.timer = new DispatcherTimer(); //skapar ett objekt av Timer //sätt intervall och tick för
            timer.Interval = TimeSpan.FromSeconds(1); //skapar en timespan som är en sekund
            timer.Tick += Timer_Tick; //event += så kommer det upp förslag på eventhandler                 

        }

   
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining--; 
            }
            else
            {
                timer.Stop(); 
            }
        }

        public void ShuffleQuestions()
        {
            if (mainWindowViewModel?.ActivePack?.Questions != null)
            {
                // Copy and shuffle questions from ActivePack
                ShuffledQuestions = mainWindowViewModel.ActivePack.Questions.Select(q => q.Query).ToList();

                ShuffledQuestions.Shuffle();

                RaisePropertyChanged(nameof(ShuffledQuestions)); // Notify binding of the update
            }
        }

    }
}
