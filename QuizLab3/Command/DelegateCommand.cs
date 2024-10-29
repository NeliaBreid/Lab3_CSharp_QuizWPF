using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizLab3.Command
{
    internal class DelegateCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object?, bool> canExecute;
        public event EventHandler? CanExecuteChanged; //när man bindar knappens command med det här eventet. om det ändras i koden så ändras alla som suscribar på den. 

        public DelegateCommand(Action<object> execute, Func<object?,bool> canExecute = null) //referens till en void metod som tar en object in //tar objekt in o retunerar en bool. det är i princip metoderna nedan
        {
            ArgumentNullException.ThrowIfNull(execute); //om null så exeption
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty); //uppdateringsmetoden
        public bool CanExecute(object? parameter)
        {
            return canExecute is null? true: canExecute(parameter);
        }

        public void Execute(object? parameter) //kod som körs när man trycker på knappen, men bara om CanExecute är true(bool)
        {
            execute(parameter); //passar vidare
        }

    }
}
