using QuizLab3.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLab3.ViewModel
{
    class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        public QuestionPackViewModel? ActivePack{ get => mainWindowViewModel.ActivePack;}

        private Question _activeQuestion;


        public Question ActiveQuestion
        {
            get => _activeQuestion;
             set
            {
                _activeQuestion = value;
                RaisePropertyChanged(); //skickas varje gång man sätter testdatan. annars måste man sätta metoden överallt. 
                //inte alla gånger doxk*
            }
        }
    

        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
 
        }

      
        //en metod som lägger till frågorna i

      

            //Först skapar vi en default questionpack. Den kommer finnas i början av spelet så att det ska initieras när programmet byggs.
            //när programmet byggs, (i ngn konstruktor) skapa en questionpack.

            //varje gång jag klickar på add i configurationview så adderas ett objekt av typen Question till Active QuestionPack.
            //Question har då tillhörande question, correct answer och tre fel svar. (dessa måste vara med)
        }
    }


