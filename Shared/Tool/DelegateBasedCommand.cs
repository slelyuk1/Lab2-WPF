using System;
using System.Windows.Input;

namespace Shared.Tool
{
    public class DelegateBasedCommand : ICommand
    {
        private static readonly Func<object, bool> TruePredicate = _ => true;

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public DelegateBasedCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public DelegateBasedCommand(Action<object> execute) : this(execute, TruePredicate)
        {
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        // todo try to move
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}