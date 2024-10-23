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

        
        public ConfigurationViewModel( MainWindowViewModel? mainWindowViewModel)
        {
           this.mainWindowViewModel = mainWindowViewModel;
        }
    }
}
