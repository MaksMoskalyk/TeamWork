using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DelegateCommandNS
{
    class AsyncDelegateCommand : ICommand
    {
        private Func<object, Task> asyncExecute;

        private Predicate<object> canExecute;

        public AsyncDelegateCommand(Func<object, Task> asyncExecute,
            Predicate<object> canExecute)
        {
            this.asyncExecute = asyncExecute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public async void Execute(object parameter)
        {
            await AsyncExecute(parameter);
        }

        protected async Task AsyncExecute(object parameter)
        {
            await asyncExecute(parameter);
        }
    }
}
