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
#region properties
        MainWindowModel mainWindowModel;//the connection to the model

        public ICommand ChangeTypePickerCommand { get; set; }//the command that will act when the user click on change to month or to week type picker

        /// <summary>
        ///this property contain the userControl that show on screen
        /// </summary>
        public static readonly DependencyProperty UserControl = DependencyProperty.Register("UserControlProperty", typeof(UserControl), typeof(MainWindowViewModel));

        public UserControl UserControlProperty
        {
            get { return (UserControl)GetValue(UserControl); }
            set { SetValue(UserControl, value); }
        }

        /// <summary>
        ///this property contain the content of button according the click on it. change between month and week pickers
        /// </summary>
        public static readonly DependencyProperty ButtonChangeContent = DependencyProperty.Register("ButtonChangeContentProperty", typeof(String), typeof(MainWindowViewModel));

        public String ButtonChangeContentProperty
        {
            get { return (String)GetValue(ButtonChangeContent); }
            set { SetValue(ButtonChangeContent, value); }
        }

        /// <summary>
        ///this property contain the user email address- to identify the user
        /// </summary>
        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(MainWindowViewModel));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        /// <summary>
        ///this property contain true if the menu need to be visible and otherwise false
        /// </summary>
        public static readonly DependencyProperty MenuVisibility = DependencyProperty.Register("MenuVisibilityProperty", typeof(String), typeof(MainWindowViewModel), new PropertyMetadata("Hidden"));
        public String MenuVisibilityProperty
        {
            get { return (String)GetValue(MenuVisibility); }
            set { SetValue(MenuVisibility, value); }

        }
        /// <summary>
        ///this property contain true if the button of changing between month picker and week picker need to be visible(in WeekDetails window). otherwise, false 
        /// </summary>
        public static readonly DependencyProperty ButtonVisibility = DependencyProperty.Register("ButtonVisibilityProperty", typeof(String), typeof(MainWindowViewModel));

        public String ButtonVisibilityProperty
        {
            get { return (String)GetValue(ButtonVisibility); }
            set { SetValue(ButtonVisibility, value); }
        }

        /// <summary>
        ///this property contain true if at least week passed from the last time the user update his current weight (that will make a popUp of update open). otherwise, false. 
        /// </summary>
        public static readonly DependencyProperty IsOpenChangeWeight = DependencyProperty.Register("IsOpenChangeWeightProperty", typeof(Boolean), typeof(LogInUserControlVM), new PropertyMetadata(false));
        public Boolean IsOpenChangeWeightProperty
        {
            get { return (Boolean)GetValue(IsOpenChangeWeight); }
            set { SetValue(IsOpenChangeWeight, value); }
        }
        public ICommand HomeCommand{ get; set; }//the command that act when the user click on home button (with the match image)
        public ICommand LogOutCommand { get; set; }//the command that act when the user click on logOut button (with the match image)
        public ICommand DailyFoodCommand { get; set; }//the command that act when the user click on DailyFood button (with the match image)
        public ICommand WeekGoalsCommand { get; set; }//the command that act when the user click on WeekGoals button (with the match image)
        public ICommand SearchCommand { get; set; }//the command that act when the user click on Search button (with the match image)
        public ICommand WeekDetailsCommand { get; set; }//the command that act when the user click on WeekDetails button (with the match image)
        #endregion

        /// <summary>
        ///this function change the userControlProperty according to the input string- and match all the view of the mainWindow according this userControl
        /// </summary>
        /// <param name="str">contain string that describe a user control</param>
        public void SetUserControl(string str)
        {
            switch (str)//change the userControl according the input string
            {
                case "Home":
                    UserControlProperty = new HomeUserControl(EmailAddressProperty);
                    ButtonVisibilityProperty = "Hidden";
                    break;
                case "WeekGoals":
                    UserControlProperty = new WeekGoalsUserControl(EmailAddressProperty);
                    ButtonVisibilityProperty = "Hidden";
                    break;
                case "WeekDetails":
                    UserControlProperty = new MonthChartUserControl(EmailAddressProperty);
                    ButtonVisibilityProperty = "Visible";
                    break;
                case "DailyFood":
                    UserControlProperty = new AddDailyFoodUserControl(EmailAddressProperty);
                    ButtonVisibilityProperty = "Hidden";
                    break;
                case "SearchFood":
                    UserControlProperty = new SearchUserControl();
                    ButtonVisibilityProperty = "Hidden";
                    break;
                case "LogOut":
                    initLogIn();
                    ButtonVisibilityProperty = "Hidden";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///this function initialize the window to be login window
        /// </summary>
        private void initLogIn()
        {
            UserControlProperty = new LogInUserControl();
            (UserControlProperty as LogInUserControl).VM.PropertyChanged += isLogedInFunc;//sign to propertyChangeEvent
            MenuVisibilityProperty = "Hidden";
        }

        public MainWindowViewModel()//constructor, initialization
        {
            mainWindowModel = new MainWindowModel();
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

        /// <summary>
        ///this function is event that call when the property connected change to true(the user can get in)
        /// </summary>
        /// <param name="sender">the sender of this event call</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void isLogedInFunc(object sender, PropertyChangedEventArgs e)
        {
            EmailAddressProperty = (sender as LogInUserControlVM).EmailAddress;
            UserControlProperty = new HomeUserControl((sender as LogInUserControlVM).EmailAddress);
            MenuVisibilityProperty = "Visible";
            ButtonVisibilityProperty = "Hidden";
        }
    }
}
