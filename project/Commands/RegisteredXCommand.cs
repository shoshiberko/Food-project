using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using project.ViewModel;

namespace project.Commands
{
    class RegisteredXCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                ((RegisterViewModel)parameter).close();
            }
            catch (Exception e)
            {

            }
        }
    }
}
