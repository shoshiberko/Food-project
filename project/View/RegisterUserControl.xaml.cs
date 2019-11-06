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
using System.ComponentModel;

namespace project.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RegisterUserControl : UserControl
    {
        RegisterViewModel registerViewModel;
        public static readonly DependencyProperty IsDone = DependencyProperty.Register("IsDoneProperty", typeof(Boolean), typeof(RegisterUserControl));
        public Boolean IsDoneProperty
        {
            get { return (Boolean)GetValue(IsDone); }
            set { SetValue(IsDone, value); }
        }
        public RegisterUserControl()
        {
            InitializeComponent();
            
            registerViewModel = new RegisterViewModel();
            registerViewModel.PropertyChanged += viewModelPropertyChanged;
            myGrid.DataContext = registerViewModel;
            birthDatePicker.SelectedDate = DateTime.Now;
        }

        private void viewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDone")
                IsDoneProperty = true;
        }
    }
}
