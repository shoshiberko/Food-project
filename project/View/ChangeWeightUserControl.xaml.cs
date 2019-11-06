using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project.View
{
    /// <summary>
    /// Interaction logic for UserControl10.xaml
    /// </summary>
    public partial class ChangeWeightUserControl : UserControl
    {
        ChangeWeightViewModel changeWeightViewModel;
       /* public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(ChangeWeightUserControl), new PropertyMetadata("", new PropertyChangedCallback(emailAddressChanged)));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        public static void emailAddressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChangeWeightUserControl p = (ChangeWeightUserControl)d;
            p.changeWeightViewModel.EmailAddressProperty = (String)(e.NewValue);
        }*/

        public ChangeWeightUserControl(String emailAddress)
        {
            InitializeComponent();
            changeWeightViewModel = new ChangeWeightViewModel(emailAddress);
            myGrid.DataContext = changeWeightViewModel;
        }
    }
}
