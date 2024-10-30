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
				RaisePropertyChanged(); 
            }
		}

        public DelegateCommand NewPackDialog { get; }
        public DelegateCommand PackOptionsDialog { get; }


        public MainWindowViewModel()
		{
            ActivePack = new QuestionPackViewModel(new QuestionPack("my default Questionspack"));

            Packs = new ObservableCollection<QuestionPackViewModel>(); //skapar en instans av Packs

            PlayerViewModel = new PlayerViewModel(this);

			ConfigurationViewModel = new ConfigurationViewModel(this);

            NewPackDialog = new DelegateCommand(UpdateNewPackDialog, CanUpdateNewPackDialog);

            PackOptionsDialog = new DelegateCommand(UpdatePackOptionsDialog, CanUpdatePackOptionsDialog);
        }



        private bool CanUpdateNewPackDialog(object? arg) 
        {
            return true; 
        }

        private void UpdateNewPackDialog(object obj)
        {
            CreateNewPackDialog createNewPackDialog = new CreateNewPackDialog();

            createNewPackDialog.ShowDialog();
        }
        private bool CanUpdatePackOptionsDialog(object? arg) 
        {
            return true; 
        }

        private void UpdatePackOptionsDialog(object obj)
        {
            CreateNewPackDialog createNewPackDialog = new CreateNewPackDialog();

            createNewPackDialog.ShowDialog(); 
        }

    }
}
