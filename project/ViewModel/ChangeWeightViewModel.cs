using project.Commands;
using project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace project.ViewModel
{
     public class ChangeWeightViewModel:DependencyObject
    {
#region properties
        ChangeWeightModel changeWeightModel;//the connection to the model

        /// <summary>
        ///the currentWeight that the user update every week(in the update Current Weight window)
        /// </summary>
        public static readonly DependencyProperty CurrentWeight = DependencyProperty.Register("CurrentWeightProperty", typeof(String), typeof(ChangeWeightViewModel), new PropertyMetadata(""));//dependency property to the weight value(binding).
        public String CurrentWeightProperty
        {
            get { return (String)GetValue(CurrentWeight); }
            set { SetValue(CurrentWeight, value); }
        }

        /// <summary>
        ///the email of the current user
        /// </summary>
        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(ChangeWeightViewModel), new PropertyMetadata("")); //dependency property to the emailAddress value(binding).
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }
        public ICommand SaveWeightChangeCommand { get; set; }//the command of the button to saveChanges.
#endregion
        public ChangeWeightViewModel(String emailAddress)//constructor
        {
            changeWeightModel = new ChangeWeightModel();
            SaveWeightChangeCommand = new SaveNewWeightCommand();
            EmailAddressProperty = emailAddress;//change to the corrent user.
        }

        /// <summary>
        ///this function go to the model function- to save the weight changes in db.
        /// </summary>
        public void SaveChange()
        {
            changeWeightModel.SaveChanges(CurrentWeightProperty,DateTime.Now,EmailAddressProperty);
        }
    }
}
