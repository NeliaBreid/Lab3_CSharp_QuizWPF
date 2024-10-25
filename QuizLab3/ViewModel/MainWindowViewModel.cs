using QuizLab3.Command;
using QuizLab3.Dialogs;
using QuizLab3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
				RaisePropertyChanged(); //uppdateringsmetod
				ConfigurationViewModel.RaisePropertyChanged("ActivePack"); //ett exempel
			}
		}
        public ICommand OpenDialogCommand { get; }
		public MainWindowViewModel()
		{
			PlayerViewModel = new PlayerViewModel(this);

			ConfigurationViewModel = new ConfigurationViewModel(this);

			ActivePack = new QuestionPackViewModel(new QuestionPack("my default questionspack"));
			ActivePack.Questions.Add(new Question("vad är 2 plus 2", "4", "2", "6", "10"));

            ActivePack.Questions.Add(new Question("vad är 5 plus 2", "4", "2", "6", "10")); //fråga 2
			ConfigurationViewModel.ActiveQuestion = ActivePack.Questions.FirstOrDefault();

            OpenDialogCommand = new DelegateCommand(UpdateDialog, CanUpdateDialog);
			

        }
  //      private void OpenDialogCreatePack() //gör en metod för att öppna 
		//{
		//	CreateNewPackDialog createNewPackDialog = new CreateNewPackDialog();

		//	createNewPackDialog.ShowDialog();
		//}
        private bool CanUpdateDialog(object? arg) //lite tokigt namn, viewmodel vet inte om att det finns en knapp i view.
        {
            return true; //lägga in logic // längden på strängen
        }

        private void UpdateDialog(object obj)
        {
            CreateNewPackDialog createNewPackDialog = new CreateNewPackDialog();

            createNewPackDialog.ShowDialog(); //
        }

    }
}
