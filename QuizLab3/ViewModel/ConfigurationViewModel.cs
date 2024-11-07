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

            ActiveQuestion = ActivePack.Questions.FirstOrDefault();

            AddQuestionsCommand = new DelegateCommand(AddQuestionToActivePack, CanAddQuestionToActivePack);

            RemoveQuestionsCommand = new DelegateCommand(RemoveQuestionFromActivePack, CanRemoveQuestionFromActivePack);

            CreateQuestionPacksCommand = new DelegateCommand(CreatePack);
            DeleteQuestionPacksCommand = new DelegateCommand(DeletePack);
        }

        private void AddQuestionToActivePack(object parameter)
        {
            if (ActiveQuestion != null)
            {
                var newQuestion = new Question(
                     ActiveQuestion.Query = "New Question",
                     ActiveQuestion.CorrectAnswer=string.Empty,
                     ActiveQuestion.IncorrectAnswers[0] = string.Empty,
                     ActiveQuestion.IncorrectAnswers[1] = string.Empty,
                     ActiveQuestion.IncorrectAnswers[2] = string.Empty);
                     ActivePack.Questions.Add(newQuestion);
            }
            else
            {
                ActivePack.Questions.Add(new Question("New Question", string.Empty, string.Empty, string.Empty, string.Empty));
            }


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
            ActiveQuestion = ActivePack.Questions.FirstOrDefault();

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
            return ActivePack != null && ActivePack.Questions.Any();
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
            RaisePropertyChanged(nameof(ActivePack));
        }
    }
}


