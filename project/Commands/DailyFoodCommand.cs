﻿using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace project.Commands
{
   public class DailyFoodCommand :ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ((MainWindowViewModel)parameter).SetUserControl("DailyFood");//this function in ViewModel make the mainWindow show the dailyFood UserControl. calls from button click
        }
    }
}
