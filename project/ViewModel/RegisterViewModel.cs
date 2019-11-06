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
#region properties
        RegisterModel registerModel;//the connection to the model

        //fields that bind to the view
        public ICommand ClickCommand { get; set; }//the command that act when the user click on the Next button
        public ICommand RegisterXCommand { get; set; }//the command that act when the user enter on X button
        //Gender fields
        public GENDER Female { get; set; }
        public GENDER Male { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;//event that will be callen when property change and call this event by NotifyPropertyChanged
        private void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public String HeightString { get; set; }//the height of the user
        public String EmailAddress { get;set; }//the email address of the user


        /// <summary>
        ///the text of tooltip popup that is opened when click on Register
        /// </summary>
        public static readonly DependencyProperty ToolTipText = DependencyProperty.Register("ToolTipTextProperty", typeof(String), typeof(RegisterViewModel));
        public String ToolTipTextProperty
        {
            get { return (String)GetValue(ToolTipText); }
            set { SetValue(ToolTipText, value); }
        }


        /// <summary>
        /// this property contain true if the popup(like tooltip) will be opened when click log in
        /// </summary>
        public static readonly DependencyProperty ToolTipIsOpenRegister = DependencyProperty.Register("ToolTipIsOpenRegisterProperty", typeof(Boolean), typeof(RegisterViewModel), new PropertyMetadata(false));
        public Boolean ToolTipIsOpenRegisterProperty
        {
            get { return (Boolean)GetValue(ToolTipIsOpenRegister); }
            set { SetValue(ToolTipIsOpenRegister, value); }
        }


        /// <summary>
        ///this property contain true when user was registered successfully or when user click on x button.
        /// </summary>
        private Boolean isDone;
        public Boolean IsDoneProperty
        {
            get { return isDone; }
            set {
                isDone = value;
                if (value ==true)
                    NotifyPropertyChanged("IsDone");
            }
        }

        /// <summary>
        ///this property contain the user password
        /// </summary>
        public static readonly DependencyProperty MyPassword = DependencyProperty.Register("MyPasswordProperty", typeof(String), typeof(RegisterViewModel));
        public String MyPasswordProperty
        {
            get { return (String)GetValue(MyPassword); }
            set { SetValue(MyPassword, value); }
        }
        public DateTime BirthDate { get; set; }//user BirthDate
        public String FamilyStatusString { get; set; }//user FamilyStatus
        public String CurrentWeightString { get; set; }//user CurrentWeight
        public String GoalWeightString { get; set; }//user GoalWeight
        public DateTime MyDisplayDateEnd { get; set; }//display end date
        public List<String> FamilyStatusLst { get; set; }//the list of FamilyStatuses that the user can choose from
#endregion
        public RegisterViewModel()//constructor
        {
            registerModel = new RegisterModel();
            FamilyStatusLst=new List<String>() { "SINGLE","MARRIED", "DIVORCED"};
            MyDisplayDateEnd =DateTime.Now;
            BirthDate= DateTime.Now;
            ClickCommand = new AddUserCommand();
            RegisterXCommand = new RegisteredXCommand();
        }

        /// <summary>
        ///this function of x command
        /// </summary>
        public void close()
        {
            this.IsDoneProperty = true;
        }

        /// <summary>
        ///user registers
        /// </summary>
        public void addUser()
        {
            try
            {
                GENDER userGender = Female;// if the user is female then Female property =MALE ,and Male property= MALE, else: Female property =FEMALE ,and Male property= FEMALE
                registerModel.addUser(EmailAddress, MyPasswordProperty, userGender,BirthDate, (FAMILYSTATUS)Enum.Parse(typeof(FAMILYSTATUS), FamilyStatusString),
                    int.Parse(HeightString), float.Parse(CurrentWeightString), DateTime.Now, float.Parse(GoalWeightString));//create and adding new user according the entring details
                ToolTipTextProperty = "You have successfully registered";
                ToolTipIsOpenRegisterProperty = true;
                startTimerPopUpToolTip(2.2, true);
                
            }
            catch(Exception e)//the register failed
            {
                ToolTipTextProperty = e.Message;
                ToolTipIsOpenRegisterProperty = true;
                startTimerPopUpToolTip(2.2, false);
            }
        }

        /// <summary>
        ///this function will close the toolTipPopUp after interval seconds
        /// </summary>
        /// <param name="interval">the time (in seconds) that the toolTipPopUp will be opened</param>
        /// <param name="isSuccess">isDoneProperty will be true if the processdone successfuly</param>
        private void startTimerPopUpToolTip(double interval, bool isSuccess)
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
    public class CheckToMaleGenderEnumConverter : IValueConverter//convert true to GENDER.MALE ,and false to GENDER.FEMALE 
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

