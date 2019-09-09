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
        public LogInModel MyModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public bool canLogIn()
        {
            return (!ToolTipIsOpenLogInProperty);
        }

        public ICommand MyCommandLogIn { get; set; }
        public ICommand MyCommandRegister { get; set; }
        
        public String EmailAddress { get; set; }


        //true if user was loged in successfully
        //public static readonly DependencyProperty IsConnected = DependencyProperty.Register("IsConnectedProperty", typeof(Boolean), typeof(LogInUserControlVM), new PropertyMetadata(false));
        Boolean isConnected;
        public Boolean IsConnectedProperty
        {
            get { return isConnected; }
            set { isConnected = value; if(value==true) NotifyPropertyChanged("connected"); }
        }

        //true if the user was registerd successfully or he clicked on x button of register popup.
        public static readonly DependencyProperty IsRegistrationDone = DependencyProperty.Register("IsRegistrationDoneProperty", typeof(Boolean), typeof(LogInUserControlVM), new PropertyMetadata(new PropertyChangedCallback(onRegisterationDonePropertyChanged)));
        public Boolean IsRegistrationDoneProperty
        {
            get { return (Boolean)GetValue(IsRegistrationDone); }
            set { SetValue(IsRegistrationDone, value); }
        }

        //function when the dp IsRegistrationDone changed
        private static void onRegisterationDonePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                (d as LogInUserControlVM).RegisterPopUpIsOpenProperty = false;
                (d as LogInUserControlVM).IsRegistrationDoneProperty = false;
            }
        }


        public static readonly DependencyProperty MyPassword = DependencyProperty.Register("MyPasswordProperty", typeof(String), typeof(LogInUserControlVM));
        public String MyPasswordProperty
        {
            get { return (String)GetValue(MyPassword); }
            set { SetValue(MyPassword, value); }
        }

        //the text of popup that is opened when click on log in
        public static readonly DependencyProperty ToolTipText = DependencyProperty.Register("ToolTipTextProperty", typeof(String), typeof(LogInUserControlVM));
        public String ToolTipTextProperty
        {
            get { return (String)GetValue(ToolTipText); }
            set { SetValue(ToolTipText, value); }
        }

        //true- the popup of register will be opened when click on register
        public static readonly DependencyProperty RegisterPopUpIsOpen = DependencyProperty.Register("RegisterPopUpIsOpenProperty", typeof(Boolean), typeof(LogInUserControlVM),new PropertyMetadata(false));
        public Boolean RegisterPopUpIsOpenProperty
        {
            get { return (Boolean)GetValue(RegisterPopUpIsOpen); }
            set { SetValue(RegisterPopUpIsOpen, value); }
        }

        //true- the popup(like tooltip) will be opened when click log in
        public static readonly DependencyProperty ToolTipIsOpenLogIn = DependencyProperty.Register("ToolTipIsOpenLogInProperty", typeof(Boolean), typeof(LogInUserControlVM), new PropertyMetadata(false));
        public Boolean ToolTipIsOpenLogInProperty
        {
            get { return (Boolean)GetValue(ToolTipIsOpenLogIn); }
            set { SetValue(ToolTipIsOpenLogIn, value); }
        }
        public LogInUserControlVM()
        {
            MyModel = new LogInModel();
            MyCommandLogIn = new LogInCommand();
            MyCommandRegister = new RegisterCommand();

        }

        public async Task logIn()
        {
            User user= await Task.Run(() => MyModel.getUser(EmailAddress));
            ToolTipIsOpenLogInProperty = true;
            startTimerPopUpToolTip(2.2);
            if (user == null || user.Password != MyPasswordProperty)
                ToolTipTextProperty = "Sorry, email address or password are wrong";
            else
            {
                ToolTipTextProperty = "Welcome! don't forget tomorrow's diet;)";
                IsConnectedProperty = true;
            }

        }
        /*public void logIn()
        {
            User user = MyModel.getUser(EmailAddress);
            ToolTipIsOpenLogInProperty = true;
            startTimerPopUpToolTip(2.2);
            if (user == null || user.Password != MyPasswordProperty)
                ToolTipTextProperty = "Sorry, email address or password are wrong";
            else
            {
                ToolTipTextProperty = "Welcome! don't forget tomorrow's diet;)";
                IsConnectedProperty = true;
            }

        }*/
        
        private void startTimerPopUpToolTip(double interval)//will close the toolTipPopUp after interval seconds
        {
            DispatcherTimer time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(interval);
            time.Start();
            time.Tick += delegate
            {
                time.Interval = TimeSpan.FromSeconds(interval);
                ToolTipIsOpenLogInProperty = false;
                time.Stop();
                //IsConnectedProperty = true;
            };
           
        }

        //when clicking on register  button
        public void register()
        {
            RegisterPopUpIsOpenProperty = true;
        }
    }
}
