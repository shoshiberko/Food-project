using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace project.Commands
{
    class SaveDateMealsCommand : ICommand
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
                ((AddDailyFoodViewModel)parameter).saveMeals();//this function in ViewModel saved all the user meals details in db. calls from button click
            }
            catch (Exception e)
            {

            }
        }
    }
}
