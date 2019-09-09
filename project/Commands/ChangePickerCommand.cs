using project.ViewModel;
using project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace project.Commands
{
    public class ChangePickerCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if ((((MainWindowViewModel)parameter).UserControlProperty as MonthChartUserControl) != null)
            {
                ((MainWindowViewModel)parameter).UserControlProperty = new UserControlChart(((MainWindowViewModel)parameter).EmailAddressProperty);
                ((MainWindowViewModel)parameter).ButtonChangeContentProperty = "Change To Month Details";
            }
            else
            {
                ((MainWindowViewModel)parameter).UserControlProperty = new MonthChartUserControl(((MainWindowViewModel)parameter).EmailAddressProperty);
                ((MainWindowViewModel)parameter).ButtonChangeContentProperty = "Change To Week Details";
            }
        }
    }
}
