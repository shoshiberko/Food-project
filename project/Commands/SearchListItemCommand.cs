using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace project.Commands
{
   public class SearchListItemCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ((SearchUserControlViewModel)parameter).updateList();//this function in ViewModel update the list of food in SearchFood UserControl according to the user input. calls from button click
        }
    }
}

