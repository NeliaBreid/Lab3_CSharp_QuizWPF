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
        public QuestionPackViewModel? ActivePack { get => mainWindowViewModel.ActivePack; }
        private List<Question> _shuffledQuestions { get; set; }

       // public List<Question> ShuffledAnswers { get; set; }

        public int TotalQuestions => ShuffledQuestions?.Count ?? 0; //returns counted shuffledQuestionslist

        private int _currentQuestionIndex; //field för att spara index
        public int CurrentQuestionIndex
        {
            get => _currentQuestionIndex;
            private set
            {
                _currentQuestionIndex = value;
                RaisePropertyChanged(nameof(TotalQuestions));
                
            }
        }
        private Question _currentQuestion; //frågan jag är på just nu, innehåller både fråga o svar.
        public Question CurrentQuestion
        {
            get => _currentQuestion;
            private set
            {
                _currentQuestion = value;
                RaisePropertyChanged();
            }
        }
        public List<Question> ShuffledQuestions
        {
            get => _shuffledQuestions;
            private set
            {
                _shuffledQuestions = value;
                RaisePropertyChanged(); // Notify view of the new list of questions
                RaisePropertyChanged(nameof(TotalQuestions)); // Update total question count
            }
        }


        private int _timeRemaining;
        public string TimeRemainingDisplay => TimeSpan.FromSeconds(TimeRemaining).ToString($"c");

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
            ShuffledQuestions = new List<Question>(); //skapar en ny lista
            ShuffleQuestions();
            //TimeRemaining = mainWindowViewModel?.ActivePack?.TimeLimitInSeconds ?? 0;

            this.timer = new DispatcherTimer(); //skapar ett objekt av Timer //sätt intervall och tick för
            timer.Interval = TimeSpan.FromSeconds(1); //skapar en timespan som är en sekund
            timer.Tick += Timer_Tick; //event += så kommer det upp förslag på eventhandler                 
            //_currentQuestionIndex = 0; //initialiserar index
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
                ShuffledQuestions = mainWindowViewModel.ActivePack.Questions.ToList();

                ShuffledQuestions.Shuffle();

                RaisePropertyChanged(nameof(ShuffledQuestions));

                CurrentQuestion = ShuffledQuestions.ElementAtOrDefault(_currentQuestionIndex);
            }
        }
       
        }

        }
    

