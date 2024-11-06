using ListShuffle;
using QuizLab3.Command;
using QuizLab3.Dialogs;
using QuizLab3.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace QuizLab3.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }
        public PlayerViewModel PlayerViewModel { get; } //get, bara för att hålla koll på den
        public ConfigurationViewModel ConfigurationViewModel { get; }

        private QuestionPackViewModel? _activePack; //backningfield. frågetecknet för att tala om för kompliern att vi vet att den kan vara null

        public List<Question> ShuffledQuestions { get; set; }
        public List<Question> ShuffledAnswers { get; set; }
        public QuestionPackViewModel? ActivePack
        {
            get => _activePack;
            set
            {
                _activePack = value;
                RaisePropertyChanged(nameof(ActivePack));
                ConfigurationViewModel?.RaisePropertyChanged();

            }
        }
        private bool _isConfigurationMode = true; //börjar här, därför true
        public bool IsConfigurationMode
        {
            get => _isConfigurationMode;
            set
            {
                _isConfigurationMode = value;
                RaisePropertyChanged(nameof(IsConfigurationMode));
                RaisePropertyChanged(nameof(IsPlayerMode));
                RaisePropertyChanged(nameof(IsResultMode));
            }
        }
        private bool _isPlayerMode = false;
        public bool IsPlayerMode
        {
            get => _isPlayerMode;
            set
            {
                _isPlayerMode = value;
                RaisePropertyChanged(nameof(IsConfigurationMode));
                RaisePropertyChanged(nameof(IsPlayerMode));
                RaisePropertyChanged(nameof(IsResultMode));
            }
        }
        private bool _isResultMode = false;
        public bool IsResultMode
        {
            get => _isResultMode;
            set
            {
                _isResultMode = value;
                RaisePropertyChanged(nameof(IsConfigurationMode));
                RaisePropertyChanged(nameof(IsPlayerMode));
                RaisePropertyChanged(nameof(IsResultMode));
            }
        }

        public DelegateCommand NewPackDialog { get; }
        public DelegateCommand PackOptionsDialog { get; }
        public DelegateCommand SetActivePackCommand { get; }
        public DelegateCommand SelectViewCommand { get; }
        public DelegateCommand DefaultCommand { get; }
        public DelegateCommand ShowConfigurationViewCommand { get; }
        public DelegateCommand ShowPlayerViewCommand { get; }
        public DelegateCommand FullScreenCommand { get; }



        public MainWindowViewModel()
        {
            Packs = new ObservableCollection<QuestionPackViewModel>(); //skapar en instans av Packs

            ActivePack = new QuestionPackViewModel(new QuestionPack("My Default QuestionPack"));
            Packs.Add(ActivePack);

            PlayerViewModel = new PlayerViewModel(this);

            ConfigurationViewModel = new ConfigurationViewModel(this);

            NewPackDialog = new DelegateCommand(CreateNewPackDialog, CanCreateNewPackDialog);

            PackOptionsDialog = new DelegateCommand(UpdatePackOptionsDialog, CanUpdatePackOptionsDialog);

            SetActivePackCommand = new DelegateCommand(SetActivePack);

            ShowConfigurationViewCommand = new DelegateCommand(ShowConfigurationView);

            ShowPlayerViewCommand = new DelegateCommand(ShowPlayerView, CanShowPlayerView);

            FullScreenCommand = new DelegateCommand(SetFullScreen);


        }



        private bool CanCreateNewPackDialog(object? arg)
        {
            return true;
        }

        private void CreateNewPackDialog(object obj)
        {
            ConfigurationViewModel.NewQuestionPack = new QuestionPack(" ");

            CreateNewPackDialog createNewPackDialog = new CreateNewPackDialog();
            createNewPackDialog.ShowDialog();

        }
        private bool CanUpdatePackOptionsDialog(object? arg)
        {
            return true;
        }

        private void UpdatePackOptionsDialog(object? obj)
        {
            ConfigurationViewModel.NewQuestionPack = new QuestionPack(" ");
            PackOptionsDialog newPackOptionsDialog = new PackOptionsDialog();

            newPackOptionsDialog.ShowDialog();
        }
        private void SetActivePack(object? obj)
        {

            ActivePack = (QuestionPackViewModel)obj;

            RaisePropertyChanged(nameof(ActivePack));

        }
        private void ShowConfigurationView(object? obj)
        {
            IsConfigurationMode = true;
            IsPlayerMode = false;
            IsResultMode = false;

            PlayerViewModel.GameReset();
        }

        private void ShowPlayerView(object? obj)
        {
            IsConfigurationMode = false;
            IsPlayerMode = true;
            IsResultMode = false;

            PlayerViewModel.StartGame();
        }
        private bool CanShowPlayerView(object? arg)
        {
            return ActivePack.Questions.Any();

        }

        private void SetFullScreen(object? obj)
        {
            var window = App.Current.MainWindow;

            if (window.WindowState == WindowState.Normal)
            {
                window.WindowStyle = WindowStyle.None;
                window.ResizeMode = ResizeMode.NoResize;
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowStyle = WindowStyle.SingleBorderWindow;
                window.ResizeMode = ResizeMode.CanResize;
                window.WindowState = WindowState.Normal;
            }

        }
        public void ShowResultView()
        {
        ResultDialog createResultDialog = new ResultDialog();
            createResultDialog.ShowDialog();
        }
            
    
        
   
    }
}
