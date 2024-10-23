using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizLab3.ViewModel
{
    class ViewModelBase : INotifyPropertyChanged  // Lägger det i en separatklass så den kan återanvändas. För att använda metoden behöver den ärvas.
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null) //Den här metoden håller koll på när det uppdateras
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
