using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace project.Commands
{
   public class HomeCommand :ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ((MainWindowViewModel)parameter).SetUserControl("Home");//this function in ViewModel make the mainWindow show the Home UserControl. calls from button click
        }
    }
}
