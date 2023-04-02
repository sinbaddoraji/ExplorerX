﻿using System;
using System.Windows.Input;

namespace ExplorerX.Data.Implementations;

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        if (execute == null)
        {
            throw new ArgumentNullException(nameof(execute));
        }

        _execute = execute;
        _canExecute = canExecute ?? (() => true);
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute();
    }

    public void Execute(object parameter)
    {
        _execute();
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}