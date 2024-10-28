using QuizLab3.Command;
using QuizLab3.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizLab3.ViewModel
{
    class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        public QuestionPackViewModel? ActivePack{ get => mainWindowViewModel.ActivePack;}

        private Question _activeQuestion;
      

        public Question? ActiveQuestion
        {
            get => _activeQuestion;
             set
            {
                _activeQuestion = value;
                RaisePropertyChanged(); //skickas varje gång man sätter testdatan. annars måste man sätta metoden överallt. 
                //inte alla gånger doxk*
            }
        }

        private string _query;
        public string Query
        {
            get => ActiveQuestion.Query;
            set
            {
                ActiveQuestion.Query = value;
                RaisePropertyChanged();
                RaisePropertyChanged("SelectedItem");
            }
        }

        public ICommand AddQuestionsCommand { get; }

        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            
            ActivePack.Questions.Add(new Question(" "," "," "," ", " "));

            ActiveQuestion = ActivePack.Questions.FirstOrDefault(); //får ut första itemet i en lista.

            AddQuestionsCommand = new DelegateCommand(AddQuestionToActivePack, CanAddQuestionToActivePack);

        }

        private void AddQuestionToActivePack(object parameter)
        {

                var newQuestion = new Question(
                 ActiveQuestion.Query,
                 ActiveQuestion.CorrectAnswer,
                 ActiveQuestion.IncorrectAnswers[0],
                 ActiveQuestion.IncorrectAnswers[1],
                 ActiveQuestion.IncorrectAnswers[2]);
                
                ActivePack.Questions.Add(newQuestion);

        }
        private bool CanAddQuestionToActivePack(object parameter)
        {
            return ActivePack != null;
        }


    }
    }


