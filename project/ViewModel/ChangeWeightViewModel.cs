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
        ChangeWeightModel changeWeightModel;

        public static readonly DependencyProperty CurrentWeight = DependencyProperty.Register("CurrentWeightProperty", typeof(String), typeof(ChangeWeightViewModel), new PropertyMetadata(""));
        public String CurrentWeightProperty
        {
            get { return (String)GetValue(CurrentWeight); }
            set { SetValue(CurrentWeight, value); }
        }

        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(ChangeWeightViewModel), new PropertyMetadata(""));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }
        public ICommand SaveWeightChangeCommand { get; set; }

        public ChangeWeightViewModel()
        {
            changeWeightModel = new ChangeWeightModel();
            SaveWeightChangeCommand = new SaveNewWeightCommand();
        }

        public void SaveChange()
        {
            changeWeightModel.SaveChanges(CurrentWeightProperty,DateTime.Now,EmailAddressProperty);
        }
    }
}
