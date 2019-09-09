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

        public ICommand ChangeTypePickerCommand { get; set; }


        public static readonly DependencyProperty UserControl = DependencyProperty.Register("UserControlProperty", typeof(UserControl), typeof(MainWindowViewModel));

        public UserControl UserControlProperty
        {
            get { return (UserControl)GetValue(UserControl); }
            set { SetValue(UserControl, value); }
        }

        public static readonly DependencyProperty ButtonChangeContent = DependencyProperty.Register("ButtonChangeContentProperty", typeof(String), typeof(MainWindowViewModel));

        public String ButtonChangeContentProperty
        {
            get { return (String)GetValue(ButtonChangeContent); }
            set { SetValue(ButtonChangeContent, value); }
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

        public static readonly DependencyProperty Background = DependencyProperty.Register("BackgroundProperty", typeof(String), typeof(MainWindowViewModel), new PropertyMetadata("Gray"));

        public String BackgroundProperty
        {
            get { return (String)GetValue(Background); }
            set { SetValue(Background, value); }
        }

        public static readonly DependencyProperty ButtonVisibility = DependencyProperty.Register("ButtonVisibilityProperty", typeof(String), typeof(MainWindowViewModel));

        public String ButtonVisibilityProperty
        {
            get { return (String)GetValue(ButtonVisibility); }
            set { SetValue(ButtonVisibility, value); }
        }
        
        public static readonly DependencyProperty IsOpenChangeWeight = DependencyProperty.Register("IsOpenChangeWeightProperty", typeof(Boolean), typeof(LogInUserControlVM), new PropertyMetadata(false));
        public Boolean IsOpenChangeWeightProperty
        {
            get { return (Boolean)GetValue(IsOpenChangeWeight); }
            set { SetValue(IsOpenChangeWeight, value); }
        }
        public ICommand HomeCommand{ get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand DailyFoodCommand { get; set; }
        public ICommand WeekGoalsCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand WeekDetailsCommand { get; set; }

        public  void SetUserControl(string str)
        {
            switch (str)
            {
                case "Home":
                    UserControlProperty = new HomeUserControl(EmailAddressProperty);
                    ButtonVisibilityProperty = "Hidden";
                    BackgroundProperty = "LightGray";
                    break;
                case "WeekGoals":
                    UserControlProperty = new WeekGoalsUserControl(EmailAddressProperty);
                    ButtonVisibilityProperty = "Hidden";
                    BackgroundProperty = "White";
                    break;
                case "WeekDetails":
                    UserControlProperty = new MonthChartUserControl(EmailAddressProperty);
                    ButtonVisibilityProperty = "Visible";
                    BackgroundProperty = "White";
                    break;
                case "DailyFood":
                    UserControlProperty = new AddDailyFoodUserControl(EmailAddressProperty);
                    ButtonVisibilityProperty = "Hidden";
                    BackgroundProperty = "White";
                    break;
                case "SearchFood":
                    UserControlProperty = new SearchUserControl();
                    ButtonVisibilityProperty = "Hidden";
                    BackgroundProperty = "White";
                    break;
                case "LogOut":
                    initLogIn();
                    ButtonVisibilityProperty = "Hidden";
                    BackgroundProperty = "White";
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
            BackgroundProperty = "White";
            initLogIn();
            HomeCommand = new HomeCommand();
            LogOutCommand = new LogOutCommand();
            DailyFoodCommand = new DailyFoodCommand();
            WeekGoalsCommand = new WeekGoalsCommand();
            SearchCommand = new SearchFoodCommand();
            WeekDetailsCommand = new WeekDetailsCommand();
            ChangeTypePickerCommand = new ChangePickerCommand();
            ButtonVisibilityProperty = "Hidden";
            ButtonChangeContentProperty = "Change To Week Details";
        }

        //when user loged in successfully
        private void isLogedInFunc(object sender, PropertyChangedEventArgs e)
        {
            EmailAddressProperty = (sender as LogInUserControlVM).EmailAddress;
            UserControlProperty = new HomeUserControl((sender as LogInUserControlVM).EmailAddress);
            MenuVisibilityProperty = "Visible";
            ButtonVisibilityProperty = "Hidden";
            BackgroundProperty = "LightGray";
        }
    }
}
