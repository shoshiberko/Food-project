using project.Commands;
using project.Model;
using project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace project.ViewModel
{
    public class HomeUserControlViewModel:DependencyObject
    {
#region properties
        HomeUserControlModel homeUserControlModel;//the connection to the model

        /// <summary>
        ///the email address to identify the current user
        /// </summary>
        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(HomeUserControlViewModel), new PropertyMetadata(""));//dependency property to the email address value(binding).
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        /// <summary>
        ///property that make a updateWeight window open
        /// </summary>
        public static readonly DependencyProperty IsOpenChangeWeight = DependencyProperty.Register("IsOpenChangeWeightProperty", typeof(Boolean), typeof(HomeUserControlViewModel));//dependency property to the bool value-if the changeWeight popUp is open or not(binding).
        public Boolean IsOpenChangeWeightProperty
        {
            get { return (Boolean)GetValue(IsOpenChangeWeight); }
            set { SetValue(IsOpenChangeWeight, value); }
        }

        /// <summary>
        ///user control that will be showen in the view
        /// </summary>
        public static readonly DependencyProperty UserControl = DependencyProperty.Register("UserControlProperty", typeof(UserControl), typeof(HomeUserControlViewModel));//dependency property to the userControl value(binding)-define which userControl will apear on screen.
        public UserControl UserControlProperty
        {
            get { return (UserControl)GetValue(UserControl); }
            set { SetValue(UserControl, value); }
        }

        public ICommand CloseCommand { get; set; }//command to close the update weight popUp
#endregion
        public HomeUserControlViewModel(String emailAddress)//constructor
        {
            homeUserControlModel = new HomeUserControlModel();
            CloseCommand = new CloseWeightUpdateCommand();
            EmailAddressProperty = emailAddress;
            var user = homeUserControlModel.getUser(EmailAddressProperty);//set the current User
            var dateTmp = (user.LastUpdateCurrentWeight.AddDays(7));//check when the user last update his weight
            if (dateTmp.Date < DateTime.Now.Date)//if it's more than week ago-open update popUp
            {
                IsOpenChangeWeightProperty = true;
                UserControlProperty = new ChangeWeightUserControl(EmailAddressProperty);
            }
            else
            {
                IsOpenChangeWeightProperty = false;
            }
        }      
}
}
