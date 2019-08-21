using BE;
using project.Commands;
using project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace project.ViewModel
{
    public class RegisterViewModel: DependencyObject, INotifyPropertyChanged
    {
        RegisterModel registerModel;
        //fields that bind to the view
        public ICommand ClickCommand { get; set; }
        public ICommand RegisterXCommand { get; set; }
        public GENDER Female { get; set; }
        public GENDER Male { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void close()///of x command
        {
            this.IsDoneProperty = true;
        }

        public String HeightString { get; set; }
        public String EmailAddress { get;set; }

        //the text of tooltip popup that is opened when click on -
        public static readonly DependencyProperty ToolTipText = DependencyProperty.Register("ToolTipTextProperty", typeof(String), typeof(RegisterViewModel));
        public String ToolTipTextProperty
        {
            get { return (String)GetValue(ToolTipText); }
            set { SetValue(ToolTipText, value); }
        }

        //true- the popup(like tooltip) will be opened when click log in
        public static readonly DependencyProperty ToolTipIsOpenRegister = DependencyProperty.Register("ToolTipIsOpenRegisterProperty", typeof(Boolean), typeof(RegisterViewModel), new PropertyMetadata(false));
        public Boolean ToolTipIsOpenRegisterProperty
        {
            get { return (Boolean)GetValue(ToolTipIsOpenRegister); }
            set { SetValue(ToolTipIsOpenRegister, value); }
        }

        private Boolean isDone;//true when user was registered successfully or when user click on x button.
        public Boolean IsDoneProperty
        {
            get { return isDone; }
            set {
                isDone = value;
                if (value ==true)
                    NotifyPropertyChanged("IsDone");
            }
        }
        public static readonly DependencyProperty MyPassword = DependencyProperty.Register("MyPasswordProperty", typeof(String), typeof(RegisterViewModel));
        public String MyPasswordProperty
        {
            get { return (String)GetValue(MyPassword); }
            set { SetValue(MyPassword, value); }
        }
        public DateTime BirthDate { get; set; }
        public String FamilyStatusString { get; set; }
        public String CurrentWeightString { get; set; }
        public String GoalWeightString { get; set; }
        public float Age { get; set; }
        public DateTime MyDisplayDateEnd { get; set; }
        public List<String> FamilyStatusLst { get; set; }

        public RegisterViewModel()
        {
            registerModel = new RegisterModel();
            FamilyStatusLst=new List<String>() { "SINGLE","MARRIED", "DIVORCED"};
            MyDisplayDateEnd =DateTime.Now;
            ClickCommand = new AddUserCommand();
            RegisterXCommand = new RegisteredXCommand();
        }
        public void addUser()//user registers
        {
            try
            {
                GENDER userGender = Female;// if the user is female then Female property =MALE ,and Male property= MALE, else: Female property =FEMALE ,and Male property= FEMALE
                registerModel.addUser(EmailAddress, MyPasswordProperty, userGender,BirthDate, (FAMILYSTATUS)Enum.Parse(typeof(FAMILYSTATUS), FamilyStatusString),
                    int.Parse(HeightString), float.Parse(CurrentWeightString), DateTime.Now, float.Parse(GoalWeightString));
                ToolTipTextProperty = "You have successfully registered";
                ToolTipIsOpenRegisterProperty = true;
                startTimerPopUpToolTip(2.2, true);
                
            }
            catch(Exception e)
            {
                ToolTipTextProperty = e.Message;
                ToolTipIsOpenRegisterProperty = true;
                startTimerPopUpToolTip(2.2, false);
            }
        }
        private void startTimerPopUpToolTip(double interval, bool isSuccess)//will close the toolTipPopUp after interval seconds
        {
            DispatcherTimer time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(interval);
            time.Start();
            time.Tick += delegate
            {
                time.Interval = TimeSpan.FromSeconds(interval);
                ToolTipIsOpenRegisterProperty = false;
                if(isSuccess)
                    this.IsDoneProperty = true;
                time.Stop();
            };

        }
    }
    



    public class CheckToFemaleGenderEnumConverter : IValueConverter//convert true to GENDER.FEMALE ,and false to GENDER.MALE 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue) { return GENDER.FEMALE; }
            else { return GENDER.MALE; }
        }
    }
    public class CheckToMaleGenderEnumConverter : IValueConverter//convert true to GENDER.FEMALE ,and false to GENDER.MALE 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue) { return GENDER.MALE; }
            else { return GENDER.FEMALE; }
        }
    }
}

