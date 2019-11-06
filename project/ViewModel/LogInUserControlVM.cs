using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using project.Model;
using System.Windows;
using System.Windows.Input;
using project.Commands;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace project.ViewModel
{
    public class LogInUserControlVM: DependencyObject, INotifyPropertyChanged
    {
#region properties
        public LogInModel MyModel { get; set; }//the connection to the model

        public event PropertyChangedEventHandler PropertyChanged;//field to sign event when property changes
        private void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///this property contain true if  the user was loged in successfully
        /// </summary>
        Boolean isConnected;
        public Boolean IsConnectedProperty
        {
            get { return isConnected; }
            set { isConnected = value; if (value == true) NotifyPropertyChanged("connected"); }
        }

        /// <summary>
        ///this property contain true if the user was registerd successfully or he clicked on x button of register popup.
        /// </summary>
        public static readonly DependencyProperty IsRegistrationDone = DependencyProperty.Register("IsRegistrationDoneProperty", typeof(Boolean), typeof(LogInUserControlVM), new PropertyMetadata(new PropertyChangedCallback(onRegisterationDonePropertyChanged)));
        public Boolean IsRegistrationDoneProperty
        {
            get { return (Boolean)GetValue(IsRegistrationDone); }
            set { SetValue(IsRegistrationDone, value); }
        }

        public ICommand MyCommandLogIn { get; set; }//the command that will action when the user click on Login button
        public ICommand MyCommandRegister { get; set; }//the command that will action when the user click on Register button

        public String EmailAddress { get; set; }//the email address to identify the user

        /// <summary>
        ///this property contain the password that the user enter
        /// </summary>
        public static readonly DependencyProperty MyPassword = DependencyProperty.Register("MyPasswordProperty", typeof(String), typeof(LogInUserControlVM));
        public String MyPasswordProperty
        {
            get { return (String)GetValue(MyPassword); }
            set { SetValue(MyPassword, value); }
        }

        /// <summary>
        ///this property contain the text of popup that is opened when click on log in
        /// </summary>
        public static readonly DependencyProperty ToolTipText = DependencyProperty.Register("ToolTipTextProperty", typeof(String), typeof(LogInUserControlVM));
        public String ToolTipTextProperty
        {
            get { return (String)GetValue(ToolTipText); }
            set { SetValue(ToolTipText, value); }
        }

        /// <summary>
        ///if this property contain true- the popup of register will be opened when click on register
        /// </summary>
        public static readonly DependencyProperty RegisterPopUpIsOpen = DependencyProperty.Register("RegisterPopUpIsOpenProperty", typeof(Boolean), typeof(LogInUserControlVM), new PropertyMetadata(false));
        public Boolean RegisterPopUpIsOpenProperty
        {
            get { return (Boolean)GetValue(RegisterPopUpIsOpen); }
            set { SetValue(RegisterPopUpIsOpen, value); }
        }

        /// <summary>
        ///if this property contain true- the popup(like tooltip) will be opened when click log in
        /// </summary>
        public static readonly DependencyProperty ToolTipIsOpenLogIn = DependencyProperty.Register("ToolTipIsOpenLogInProperty", typeof(Boolean), typeof(LogInUserControlVM), new PropertyMetadata(false));
        public Boolean ToolTipIsOpenLogInProperty
        {
            get { return (Boolean)GetValue(ToolTipIsOpenLogIn); }
            set { SetValue(ToolTipIsOpenLogIn, value); }
        }
        #endregion
        /// <summary>
        ///this function return if the user can login or not
        /// </summary>
        /// <returns>
        /// true if the user can login(his details was verify, else-false.
        /// </returns>
        public bool canLogIn()
        {
            return (!ToolTipIsOpenLogInProperty);
        }

        /// <summary>
        ///function when the dp IsRegistrationDone changed
        /// </summary>
        /// <param name="d">the sender object</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void onRegisterationDonePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                (d as LogInUserControlVM).RegisterPopUpIsOpenProperty = false;
                (d as LogInUserControlVM).IsRegistrationDoneProperty = false;
            }
        }

        public LogInUserControlVM()//constructor
        {
            MyModel = new LogInModel();
            MyCommandLogIn = new LogInCommand();
            MyCommandRegister = new RegisterCommand();

        }

        /// <summary>
        ///task that check the user entering details - and show message according the verify checking result
        /// </summary>
        public async Task logIn()
        {
            User user= await Task.Run(() => MyModel.getUser(EmailAddress));
            ToolTipIsOpenLogInProperty = true;
            startTimerPopUpToolTip(2.2);//start the time that the popUp open
            if (user == null || user.Password != MyPasswordProperty)//if the user enter false details
                ToolTipTextProperty = "Sorry, email address or password are wrong";
            else
            {
                ToolTipTextProperty = "Welcome! don't forget tomorrow's diet;)";
                IsConnectedProperty = true;
            }

        }

        /// <summary>
        /// this function will close the toolTipPopUp after interval seconds
        /// </summary>
        /// <param name="interval">the time(in second) that the toolTipPopUp open</param>
        private void startTimerPopUpToolTip(double interval)
        {
            DispatcherTimer time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(interval);
            time.Start();
            time.Tick += delegate
            {
                time.Interval = TimeSpan.FromSeconds(interval);
                ToolTipIsOpenLogInProperty = false;
                time.Stop();
            };
           
        }

        /// <summary>
        ///this function active when clicking on register button
        /// </summary>
        public void register()
        {
            RegisterPopUpIsOpenProperty = true;
        }
    }
}
