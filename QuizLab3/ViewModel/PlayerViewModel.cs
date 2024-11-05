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

        public List<String> ShuffledAnswers { get; set; }
        //field eller property

        public int TotalQuestions => ShuffledQuestions?.Count ?? 0; //returns counted shuffledQuestionslist

        private int _currentQuestionIndex; //field för att spara index
        public int CurrentQuestionIndex
        {
            get => _currentQuestionIndex +1;
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
        //private Question _questionOption1; //frågan jag är på just nu, innehåller både fråga o svar.
        //public Question QuestionOption1
        //{
        //    get => _questionOption1;
        //    private set
        //    {
        //        _questionOption1 = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //private Question _questionOption2; //frågan jag är på just nu, innehåller både fråga o svar.
        //public Question QuestionOption2
        //{
        //    get => _questionOption2;
        //    private set
        //    {
        //        _questionOption2 = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //private Question _questionOption3; //frågan jag är på just nu, innehåller både fråga o svar.
        //public Question QuestionOption3
        //{
        //    get => _questionOption3;
        //    private set
        //    {
        //        _questionOption3 = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //private Question _questionOption4; //frågan jag är på just nu, innehåller både fråga o svar.
        //public Question QuestionOption4
        //{
        //    get => _questionOption4;
        //    private set
        //    {
        //        _questionOption4 = value;
        //        RaisePropertyChanged();
        //    }
        //}
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

        //public DelegateCommand UpdateButtonCommand { get; } //måste gå o binda mot så inte bara field
        public DelegateCommand AnswerButtonCommand { get; }
        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel) //
        {
            this.mainWindowViewModel = mainWindowViewModel;
            ShuffledQuestions = new List<Question>();
            //skapar en ny lista
            ShuffleQuestions();

            this.timer = new DispatcherTimer(); //skapar ett objekt av Timer //sätt intervall och tick för
            timer.Interval = TimeSpan.FromSeconds(1); //skapar en timespan som är en sekund
            timer.Tick += Timer_Tick; //event += så kommer det upp förslag på eventhandler                 
            CurrentQuestionIndex = 0; //initialiserar index
            AnswerButtonCommand = new DelegateCommand(SetAnswerButton);
        }


        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining--;
            }
            else if (CurrentQuestionIndex != TotalQuestions)
            {
                NextQuestion();
                
                TimeRemaining = ActivePack?.TimeLimitInSeconds ?? 0;
            }
        }

        public void ShuffleQuestions()
        {
            if (mainWindowViewModel?.ActivePack?.Questions != null)
            {
                ShuffledQuestions = mainWindowViewModel.ActivePack.Questions.ToList(); //klonar listan från ActivePack.Questions
                ShuffledQuestions.Shuffle();

                RaisePropertyChanged(nameof(ShuffledQuestions));

                CurrentQuestion = ShuffledQuestions.ElementAtOrDefault(_currentQuestionIndex);
                ShuffleAnswers();
            }
        }
        public void ShuffleAnswers()
        {
            if (CurrentQuestion!=null)
            {

            ShuffledAnswers = new List<string>
            {
                CurrentQuestion.CorrectAnswer,
                CurrentQuestion.IncorrectAnswers[0],
                CurrentQuestion.IncorrectAnswers[1],
                CurrentQuestion.IncorrectAnswers[2]
            };
            }
            ShuffledAnswers?.Shuffle();
            RaisePropertyChanged(nameof(ShuffledAnswers));
        }
        public void NextQuestion()
        {
            if (_currentQuestionIndex < TotalQuestions) //lägg till minus 1?
            {
                _currentQuestionIndex++;
                RaisePropertyChanged(nameof(CurrentQuestionIndex)); // Update display index
                CurrentQuestion = ShuffledQuestions.ElementAtOrDefault(_currentQuestionIndex);
                ShuffleAnswers();
            }
        }
        public void SetAnswerButton(object? obj)
        {
            if ( obj == ShuffledQuestions) //Börja kolla här
            {
                
            }
        }

 

    }

}
    

