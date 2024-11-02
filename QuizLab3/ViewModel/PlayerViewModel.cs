using QuizLab3.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QuizLab3.ViewModel
{
    class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private DispatcherTimer timer;
        private string _testData;

        public string TestData
        {
            get => _testData;
            private set 
            { 
                _testData = value;
                RaisePropertyChanged(); //skickas varje gång man sätter testdatan. annars måste man sätta metoden överallt. 
              
            }
        }

        

        public DelegateCommand UpdateButtonCommand { get; } //måste gå o binda mot så inte bara field

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel) //
        {
            this.mainWindowViewModel = mainWindowViewModel;

            TestData = "Start Value";

            this.timer = new DispatcherTimer(); //skapar ett objekt av Timer //sätt intervall och tick för
            timer.Interval = TimeSpan.FromSeconds(1); //skapar en timespan som är en sekund
            timer.Tick += Timer_Tick; //event += så kommer det upp förslag på eventhandler
                                      //timer.Start(); //sätt igång timer

            UpdateButtonCommand = new DelegateCommand(UpdateButton, CanUpdateButton); //CanUpdate är valfri
        }

        private bool CanUpdateButton(object? arg) //lite tokigt namn, viewmodel vet inte om att det finns en knapp i view.
        {
            return TestData.Length <20; //lägga in logic // längden på strängen
        }

        private void UpdateButton(object obj)
        {
            TestData += "x";
            UpdateButtonCommand.RaiseCanExecuteChanged(); //
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TestData += "x";
           
        }
    }
}
