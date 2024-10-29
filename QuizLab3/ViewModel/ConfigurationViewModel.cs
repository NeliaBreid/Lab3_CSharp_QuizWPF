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
                if (value != null)
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
      

        public ICommand AddQuestionsCommand { get; }
        public ICommand RemoveQuestionsCommand { get; }


        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            QuestionPanelVisibility = Visibility.Hidden;
            

            //ActivePack.Questions.Add(new Question(" ", " ", " ", " ", " "));

            ActiveQuestion = ActivePack.Questions.FirstOrDefault(); //får ut första itemet i en lista.

            AddQuestionsCommand = new DelegateCommand(AddQuestionToActivePack, CanAddQuestionToActivePack);

            RemoveQuestionsCommand = new DelegateCommand(RemoveQuestionFromActivePack, CanRemoveQuestionFromActivePack);
            RemoveQuestionsCommand = new DelegateCommand(RemoveQuestionFromActivePack, CanRemoveQuestionFromActivePack);

        }

        private void AddQuestionToActivePack(object parameter)
        {

            if (ActiveQuestion != null)
            {
                QuestionPanelVisibility = Visibility.Visible;

                var newQuestion = new Question(
                     ActiveQuestion.Query,
                     ActiveQuestion.CorrectAnswer,
                     ActiveQuestion.IncorrectAnswers[0],
                     ActiveQuestion.IncorrectAnswers[1],
                     ActiveQuestion.IncorrectAnswers[2]);
                ActivePack.Questions.Add(newQuestion);
            }
            ActiveQuestion = new Question(" ", " ", " ", " ", " ");

            RaisePropertyChanged(); 

        }
        private bool CanAddQuestionToActivePack(object parameter)
        {
            return ActivePack != null;

        }
        private void RemoveQuestionFromActivePack(object parameter)
        {
            var questionToRemove = ActivePack.Questions.FirstOrDefault();

            // Check if there is a question to remove
            if (questionToRemove != null && ActivePack != null)
            {
                // Remove the question from the collection
                ActivePack.Questions.Remove(questionToRemove);
            }

            RaisePropertyChanged();


        }

        private bool CanRemoveQuestionFromActivePack(object parameter)
        {
            return ActiveQuestion != null;
            
        }

    }
}


