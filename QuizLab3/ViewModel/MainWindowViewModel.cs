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
    

        public DelegateCommand NewPackDialog { get; }
        public DelegateCommand PackOptionsDialog { get; }
        public DelegateCommand SetActivePackCommand { get; }
        public DelegateCommand SelectViewCommand { get; }
        public DelegateCommand DefaultCommand { get; }

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
      

    }
}
