using project.Commands;
using project.Model;
using project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(MainWindowViewModel));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        public static readonly DependencyProperty MenuVisibility = DependencyProperty.Register("MenuVisibilityProperty", typeof(String), typeof(MainWindowViewModel), new PropertyMetadata("Hidden"));
        public String MenuVisibilityProperty
        {
            get { return (String)GetValue(MenuVisibility); }
            set { SetValue(MenuVisibility, value); }

        }

        public ICommand HomeCommand{ get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand DailyFoodCommand { get; set; }
        public ICommand WeekGoalsCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand WeekDetailsCommand { get; set; }

        public void SetUserControl(string str)
        {
            switch (str)
            {
                case "Home":
                    UserControlProperty=new UserControl1();
                    break;
                case "WeekGoals":
                    UserControlProperty = new WeekGoalsUserControl(EmailAddressProperty);
                    break;
                case "WeekDetails":
                    UserControlProperty = new MonthChartUserControl(EmailAddressProperty);
                    break;
                case "DailyFood":
                    UserControlProperty = new AddDailyFoodUserControl(EmailAddressProperty);
                    break;
                case "SearchFood":
                    UserControlProperty = new SearchUserControl();
                    break;
                case "LogOut":
                    initLogIn();
                    break;
                default:
                    break;
            }
        }

        private void initLogIn()
        {
            UserControlProperty = new LogInUserControl();
            (UserControlProperty as LogInUserControl).VM.PropertyChanged += isLogedInFunc;
            MenuVisibilityProperty = "Hidden";
        }

        public MainWindowViewModel()
        {
            mainWindowModel = new MainWindowModel();
            initLogIn();
            HomeCommand = new HomeCommand();
            LogOutCommand = new LogOutCommand();
            DailyFoodCommand = new DailyFoodCommand();
            WeekGoalsCommand = new WeekGoalsCommand();
            SearchCommand = new SearchFoodCommand();
            WeekDetailsCommand = new WeekDetailsCommand();
    }

        //when user loged in successfully
        private void isLogedInFunc(object sender, PropertyChangedEventArgs e)
        {
            EmailAddressProperty = (sender as LogInUserControlVM).EmailAddress;
            UserControlProperty = new AddDailyFoodUserControl(EmailAddressProperty);
            MenuVisibilityProperty = "Visible";
        }
    }
}
