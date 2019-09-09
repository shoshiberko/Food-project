using project.Commands;
using project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace project.ViewModel
{
    public class HomeUserControlViewModel:DependencyObject
    {
        HomeUserControlModel homeUserControlModel;
        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(HomeUserControlViewModel), new PropertyMetadata(""));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }
        public static readonly DependencyProperty IsOpenChangeWeight = DependencyProperty.Register("IsOpenChangeWeightProperty", typeof(Boolean), typeof(HomeUserControlViewModel));
        public Boolean IsOpenChangeWeightProperty
        {
            get { return (Boolean)GetValue(IsOpenChangeWeight); }
            set { SetValue(IsOpenChangeWeight, value); }
        }

        public ICommand CloseCommand { get; set; }
        public HomeUserControlViewModel(String emailAddress)
        {
            homeUserControlModel = new HomeUserControlModel();
            CloseCommand = new CloseWeightUpdateCommand();
            EmailAddressProperty = emailAddress;
            var user = homeUserControlModel.getUser(EmailAddressProperty);
            var dateTmp = (user.LastUpdateCurrentWeight.AddDays(7));
            if (dateTmp.Date<DateTime.Now.Date)
                IsOpenChangeWeightProperty = true;
            else
                IsOpenChangeWeightProperty = false;
        }      
}
}
