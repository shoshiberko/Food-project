using BE;
using project.Commands;
using project.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace project.ViewModel
{
    public class RegisterViewModel
    {
        RegisterModel registerModel;
        //fields that bind to the view
        public AddUserCommand ClickCommand { get; set; }
        public GENDER Female { get; set; }
        public GENDER Male { get; set; }
        public String HeightString { get; set; }
        public String EmailAddress { get;set; }
        public String MyPassword { get;set; }
        public String BirthDateString { get; set; }
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
        }
        public void addUser()
        {
            GENDER userGender = Female;// if the user is female then Female property =MALE ,and Male property= MALE, else: Female property =FEMALE ,and Male property= FEMALE
            registerModel.addUser(EmailAddress,MyPassword, userGender,DateTime.Parse(BirthDateString), (FAMILYSTATUS)Enum.Parse(typeof(FAMILYSTATUS), FamilyStatusString),
                int.Parse(HeightString),float.Parse(CurrentWeightString),DateTime.Now, float.Parse(GoalWeightString));
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

