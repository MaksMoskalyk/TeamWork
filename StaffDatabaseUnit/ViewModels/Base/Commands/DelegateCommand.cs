﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StaffDatabaseUnit
{
    //public class DelegateCommand : ICommand
    //{
    //    private Action<object> execute;
    //    private Predicate<object> canExecute;

    //    public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
    //    {
    //        if (execute == null)
    //            throw new ArgumentNullException("execute");
    //        this.execute = execute;
    //        this.canExecute = canExecute;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        if (canExecute != null)
    //        {
    //            return canExecute(parameter);
    //        }
    //        return false;
    //    }

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add { CommandManager.RequerySuggested += value; }
    //        remove { CommandManager.RequerySuggested -= value; }
    //    }

    //    public void Execute(object parameter)
    //    {
    //        execute(parameter);
    //    }
    //}
}