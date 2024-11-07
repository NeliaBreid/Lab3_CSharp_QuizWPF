using QuizLab3.Command;
using QuizLab3.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace QuizLab3.ViewModel
{
    class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;
        public ObservableCollection<QuestionPackViewModel> Packs { get => mainWindowViewModel.Packs; } //funkar denna?
        public QuestionPackViewModel? ActivePack{ get => mainWindowViewModel.ActivePack;}
        
        private Question _activeQuestion;

        private QuestionPack _newQuestionPack;

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

        public QuestionPack? NewQuestionPack
        {
            get => _newQuestionPack;
            set
            {
                _newQuestionPack = value;
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

      

        public DelegateCommand AddQuestionsCommand { get; }
        public DelegateCommand RemoveQuestionsCommand { get; }
        public DelegateCommand CreateQuestionPacksCommand { get; }
        public DelegateCommand DeleteQuestionPacksCommand { get; }



        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            QuestionPanelVisibility = Visibility.Hidden;

            ActiveQuestion = ActivePack.Questions.FirstOrDefault(); //får ut första itemet i en lista.

            AddQuestionsCommand = new DelegateCommand(AddQuestionToActivePack, CanAddQuestionToActivePack);

            RemoveQuestionsCommand = new DelegateCommand(RemoveQuestionFromActivePack, CanRemoveQuestionFromActivePack);

            CreateQuestionPacksCommand = new DelegateCommand(CreatePack);
            DeleteQuestionPacksCommand = new DelegateCommand(DeletePack, CanDeletePack);


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
            mainWindowViewModel.ShowPlayerViewCommand.RaiseCanExecuteChanged();

            mainWindowViewModel.SaveDataAsync();
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
                mainWindowViewModel.ShowPlayerViewCommand.RaiseCanExecuteChanged();
            }
            mainWindowViewModel.SaveDataAsync();
            RaisePropertyChanged();
        }

        private bool CanRemoveQuestionFromActivePack(object parameter)
        {
            return ActivePack.Questions.Any();
            
        }

        private void CreatePack(object? parameter) //här är när man klickar på CreateKnappen i dialog
        {
            Packs.Add(new QuestionPackViewModel(new QuestionPack(NewQuestionPack.Name, NewQuestionPack.Difficulty, NewQuestionPack.TimeLimitInSeconds)));
            mainWindowViewModel.SaveDataAsync();
        }

        private bool CanCreatePack()
        {
            return true;
        }
        private void DeletePack(object parameter)
        {
            if (ActivePack != null && Packs.Contains(ActivePack))
            {
                Packs.Remove(ActivePack);
                mainWindowViewModel.ActivePack = null;
                DeleteQuestionPacksCommand.RaiseCanExecuteChanged();
            }
            mainWindowViewModel.SaveDataAsync();
            RaisePropertyChanged();
        }

        private bool CanDeletePack(object parameter)
        {
            return ActivePack != null;

        }

    }
}


