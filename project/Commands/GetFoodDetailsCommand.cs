using project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace project.Commands
{
	public class GetFoodDetailsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;

        }

        public void Execute(object parameter)
        {
            ((FoodSearchItemViewModel)parameter).openPopUp();//this function in ViewModel make the popUp open. calls from button click
        }
    }
}