using project.Commands;
using project.Model;
using project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace project.ViewModel
{
    public class MainWindowViewModel:DependencyObject
    {
        MainWindowModel mainWindowModel;

        

         public static readonly DependencyProperty UserControl = DependencyProperty.Register("UserControlProperty", typeof(UserControl), typeof(MainWindowViewModel));

        public UserControl UserControlProperty
        {
            get { return (UserControl)GetValue(UserControl); }
            set { SetValue(UserControl, value); }
        }
        public ICommand HomeCommand{ get; set; }
        public ICommand DailyFoodCommand { get; set; }
        public ICommand WeekGoalsCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand WeekDetailsCommand { get; set; }

        public void SetUserControl(string str)
        {
            switch (str)
            {
                case "Home":
                    break;
                case "WeekGoals":
                    UserControlProperty = new WeekGoalsUserControl();
                    break;
                case "WeekDetails":
                    UserControlProperty = new UserControl9();
                    break;
                case "DailyFood":
                    UserControlProperty = new AddDailyFoodUserControl();
                    break;
                case "SearchFood":
                    UserControlProperty = new SearchUserControl();
                    break;
                default:
                    break;
            }
        }


        public MainWindowViewModel()
        {
            mainWindowModel = new MainWindowModel();
            //UserControlProperty = new AddDailyFoodUserControl();
            HomeCommand = new HomeCommand();
            DailyFoodCommand = new DailyFoodCommand();
            WeekGoalsCommand = new WeekGoalsCommand();
            SearchCommand = new SearchFoodCommand();
            WeekDetailsCommand = new WeekDetailsCommand();
    }
    }
}
