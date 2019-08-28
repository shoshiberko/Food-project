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
    /// Interaction logic for UserControl7.xaml
    /// </summary>
    public partial class AddDailyFoodUserControl : UserControl
    {
        AddDailyFoodViewModel addDailyFoodViewModel;
        public AddDailyFoodUserControl(String emailAddress)
        {
            InitializeComponent();
            addDailyFoodViewModel = new AddDailyFoodViewModel(emailAddress);
            this.DataContext = addDailyFoodViewModel;
        }
        

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            addDailyFoodViewModel.getDailyFoodByDate((DateTime)(sender as DatePicker).SelectedDate);
        }
    }
}
