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

namespace QuizLab3.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }

		public PlayerViewModel PlayerViewModel { get; } //get, bara för att hålla koll på den

        public ConfigurationViewModel ConfigurationViewModel { get; }


        private QuestionPackViewModel? _activePack; //backningfield. frågetecknet för att tala om för kompliern att vi vet att den kan vara null

    
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
        private Visibility _configurationViewVisibility = Visibility.Visible;
        public Visibility ConfigurationViewVisibility
        {
            get => _configurationViewVisibility;
            set
            {
                _configurationViewVisibility = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _playerViewVisibility = Visibility.Collapsed;
        public Visibility PlayerViewVisibility
        {
            get => _playerViewVisibility;
            set
            {
                _playerViewVisibility = value;
                RaisePropertyChanged(nameof(PlayerViewVisibility));
            }
        }

        private Visibility _resultViewVisibility = Visibility.Collapsed;
        public Visibility ReslutViewVisibility
        {
            get => _resultViewVisibility;
            set
            {
                _resultViewVisibility = value;
                RaisePropertyChanged();
            }
        }


        public DelegateCommand NewPackDialog { get; }
        public DelegateCommand PackOptionsDialog { get; }
        public DelegateCommand SetActivePackCommand { get; }
        public DelegateCommand ShowPlayerViewCommand { get; }
        public DelegateCommand ShowConfigurationViewCommand { get; }


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

            ShowPlayerViewCommand = new DelegateCommand(ShowPlayerView);

            ShowConfigurationViewCommand = new DelegateCommand(ShowConfigurationView);
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
            ActivePack = ActivePack;
            

            PackOptionsDialog newPackOptionsDialog = new PackOptionsDialog();
      
            newPackOptionsDialog.ShowDialog();

        }
        private void SetActivePack(object? obj)
        {
            ActivePack = (QuestionPackViewModel)obj;

            RaisePropertyChanged(nameof(ActivePack));
        }
        private void ShowPlayerView(object? obj)
        {
            ConfigurationViewVisibility = Visibility.Collapsed;
            PlayerViewVisibility = Visibility.Visible;
        }

        private void ShowConfigurationView(object? obj)
        {
            ConfigurationViewVisibility = Visibility.Visible;
            PlayerViewVisibility = Visibility.Collapsed;
        }


    }
}
