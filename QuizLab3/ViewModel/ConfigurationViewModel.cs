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
                if (value != null)
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
        public ICommand RemoveQuestionsCommand { get; }


        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;


            ActivePack.Questions.Add(new Question(" ", " ", " ", " ", " "));

            ActiveQuestion = ActivePack.Questions.FirstOrDefault(); //får ut första itemet i en lista.

            AddQuestionsCommand = new DelegateCommand(AddQuestionToActivePack, CanAddQuestionToActivePack);

            RemoveQuestionsCommand = new DelegateCommand(RemoveQuestionFromActivePack, CanRemoveQuestionFromActivePack);

        }

        private void AddQuestionToActivePack(object parameter)
        {
            if (ActiveQuestion == null)
            {
                ActiveQuestion = new Question(

                 ActiveQuestion.Query = string.Empty,
                 ActiveQuestion.CorrectAnswer = string.Empty,
                 ActiveQuestion.IncorrectAnswers[0] = string.Empty,
                 ActiveQuestion.IncorrectAnswers[1] = string.Empty,
                 ActiveQuestion.IncorrectAnswers[2] = string.Empty
                 );

            }

            var newQuestion = new Question(
                 ActiveQuestion.Query,
                 ActiveQuestion.CorrectAnswer,
                 ActiveQuestion.IncorrectAnswers[0],
                 ActiveQuestion.IncorrectAnswers[1],
                 ActiveQuestion.IncorrectAnswers[2]);

            ActivePack.Questions.Add(newQuestion);

            RaisePropertyChanged(nameof(ActiveQuestion));

        }
        private bool CanAddQuestionToActivePack(object parameter)
        {
            return ActivePack != null;
        }
        private void RemoveQuestionFromActivePack(object parameter)
        {
            if (ActivePack != null)
            {
            ActivePack.Questions.Remove(ActiveQuestion);

                RaisePropertyChanged(nameof(ActiveQuestion));
            }
     
        }

        private bool CanRemoveQuestionFromActivePack(object parameter)
        {
            return ActivePack != null;
        }



    }
}


