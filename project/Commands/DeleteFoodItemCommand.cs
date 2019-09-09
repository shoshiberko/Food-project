using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using project.ViewModel;

namespace project.Commands
{
    class DeleteFoodItemCommand : ICommand
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
                ((AddFoodItemViewModel)parameter).deleteFoodItem();
            }
            catch (Exception e)
            {

            }
        }
    }
}
