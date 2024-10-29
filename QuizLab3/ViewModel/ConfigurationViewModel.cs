using QuizLab3.Command;
using QuizLab3.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuizLab3.ViewModel
{
    class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        public QuestionPackViewModel? ActivePack{ get => mainWindowViewModel.ActivePack;}

        private Question _activeQuestion;

        private Visibility _questionPanelVisibility;
        public Visibility QuestionPanelVisibility
        {
            get => _questionPanelVisibility;
            set
            {  
                _questionPanelVisibility = value;
                RaisePropertyChanged();
            }
        }

        public Question? ActiveQuestion
        {
            get => _activeQuestion;
             set 
            {
                _activeQuestion = value;
                RaisePropertyChanged();

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
            }
        }
      

        public DelegateCommand AddQuestionsCommand { get; }
        public DelegateCommand RemoveQuestionsCommand { get; }



        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            QuestionPanelVisibility = Visibility.Hidden;

            ActiveQuestion = ActivePack.Questions.FirstOrDefault(); //får ut första itemet i en lista.

            AddQuestionsCommand = new DelegateCommand(AddQuestionToActivePack, CanAddQuestionToActivePack);

            RemoveQuestionsCommand = new DelegateCommand(RemoveQuestionFromActivePack, CanRemoveQuestionFromActivePack);

        }

        private void AddQuestionToActivePack(object parameter)
        {

            if (ActiveQuestion != null)
            {
               
                var newQuestion = new Question(
                     ActiveQuestion.Query,
                     ActiveQuestion.CorrectAnswer,
                     ActiveQuestion.IncorrectAnswers[0],
                     ActiveQuestion.IncorrectAnswers[1],
                     ActiveQuestion.IncorrectAnswers[2]);
                     ActivePack.Questions.Add(newQuestion);
            }
            else 
            {
                ActivePack.Questions.Add(new Question("New Question", string.Empty, string.Empty, string.Empty, string.Empty));
            }

            QuestionPanelVisibility = Visibility.Visible;
            RemoveQuestionsCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged(); 

        }
        private bool CanAddQuestionToActivePack(object parameter)
        {
            return ActivePack != null;

        }
        private void RemoveQuestionFromActivePack(object parameter)
        {
            if (ActivePack != null && ActiveQuestion != null)
            {
                
                ActivePack.Questions.Remove(ActiveQuestion);
                RemoveQuestionsCommand.RaiseCanExecuteChanged();
            }

            RaisePropertyChanged();
        }

        private bool CanRemoveQuestionFromActivePack(object parameter)
        {
            return ActivePack.Questions.Any();
        }

    }
}


