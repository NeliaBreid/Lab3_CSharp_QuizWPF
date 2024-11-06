using ListShuffle;
using QuizLab3.Command;
using QuizLab3.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
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
                RaisePropertyChanged(nameof(CurrentQuestionIndex));

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

                //CurrentQuestion = ShuffledQuestions.ElementAtOrDefault(_currentQuestionIndex);
                
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
            //if (_currentQuestionIndex < TotalQuestions) //lägg till minus 1?
            //{
            //    _currentQuestionIndex++;
            //    RaisePropertyChanged(nameof(CurrentQuestionIndex)); // Update display index
            //  // CurrentQuestion = ShuffledQuestions.ElementAtOrDefault(_currentQuestionIndex);
            //    ShuffleAnswers();
            //}
            if (_currentQuestionIndex < TotalQuestions - 1)
            {
                _currentQuestionIndex++;
                RaisePropertyChanged(nameof(CurrentQuestionIndex)); // Update display index

                // Set the current question based on the new index
                CurrentQuestion = ShuffledQuestions.ElementAtOrDefault(_currentQuestionIndex);
                ShuffleAnswers();
            }
            else
            {
                // Optional: Stop the game or handle end-of-quiz behavior
                timer.Stop();
            }
        }
        public void SetAnswerButton(object? obj)
        {

            //AnswerColors = ShuffledAnswers
            //    .Select(answer => answer == CurrentQuestion.CorrectAnswer ? Brushes.Green: Brushes.Red)
            //    .ToList();

            //RaisePropertyChanged(nameof(AnswerColors));
    
        }
       
        public void StartGame()
        {
            _currentQuestionIndex = 0;

            ShuffleQuestions();
            ShuffleAnswers();

            TimeRemaining = ActivePack?.TimeLimitInSeconds ?? 0;
            timer.Start();

            CurrentQuestion = ShuffledQuestions.ElementAtOrDefault(_currentQuestionIndex);

           
        }
        public void GameReset()
        {
            _currentQuestionIndex = 0;
            TimeRemaining = ActivePack?.TimeLimitInSeconds ?? 0;
            CurrentQuestion = ShuffledQuestions.ElementAtOrDefault(_currentQuestionIndex);
            RaisePropertyChanged(nameof(CurrentQuestionIndex));
        }




    }

}
    

