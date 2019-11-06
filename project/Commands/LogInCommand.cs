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
                return ((LogInUserControlVM)parameter).canLogIn();//log in only if the user can log in.
        }

        public void Execute(object parameter)
        {
            try
            {
                ((LogInUserControlVM)parameter).logIn();//this function in ViewModel make the user log in . calls from button click
            }
            catch (Exception e)
            {

            }
        }

    }
}
