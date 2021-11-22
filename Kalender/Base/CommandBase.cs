using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kalender.Base
{
    public class CommandBase<T> : ICommand
    {
        private readonly Action<T> executeAction;
        Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public CommandBase(Action<T> executeAction)
            : this(executeAction, null)
        {
        }

        public CommandBase(Action<T> executeAction, Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            executeAction((T)parameter);
        }
        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChanged;
            if (handler != null)
                handler(this, new EventArgs());
        }
    }
}
