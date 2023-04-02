using System;
using System.Windows.Input;

namespace ExplorerX.Data.Implementations;

public class RelayCommand : ICommand
{
    #region Properties

    private readonly Action<object> _executeAction;
    private readonly Predicate<object> _canExecuteAction;

    #endregion

    public RelayCommand(Action<object> execute)
        : this(execute, _ => true)
    {
    }
    public RelayCommand(Action<object> action, Predicate<object> canExecute)
    {
        _executeAction = action;
        _canExecuteAction = canExecute;
    }

    #region Methods

    public bool CanExecute(object? parameter)
    {
        return parameter != null && _canExecuteAction(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public void Execute(object? parameter)
    {
        if (parameter != null) 
            _executeAction(parameter);
    }

    #endregion
}