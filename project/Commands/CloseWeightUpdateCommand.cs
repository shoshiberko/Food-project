﻿using project.View;
using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace project.Commands
{
    class CloseWeightUpdateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ((HomeUserControlViewModel)parameter).IsOpenChangeWeightProperty = false;

        }
    }
}
