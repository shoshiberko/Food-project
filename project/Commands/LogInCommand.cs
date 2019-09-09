using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using project.ViewModel;

namespace project.Commands
{
    class LogInCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if(parameter==null)
                return true;
            else
                return ((LogInUserControlVM)parameter).canLogIn();
        }

        public void Execute(object parameter)
        {
            try
            {
                ((LogInUserControlVM)parameter).logIn();
            }
            catch (Exception e)
            {

            }
        }

    }
}
