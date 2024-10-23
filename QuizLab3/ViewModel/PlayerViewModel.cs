using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLab3.ViewModel
{
    class PlayerViewModel
    {
        private readonly MainWindowViewModel? mainWindowViewModel; //readOnly för att hålla koll

        public string TestData { get => "lala"; }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel) //
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }
    }
}
