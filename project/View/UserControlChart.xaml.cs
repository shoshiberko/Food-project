using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using project.ViewModel;

namespace project.View
{
    /// <summary>
    /// Interaction logic for UserControl9.xaml
    /// </summary>
    public partial class UserControlChart : UserControl
    {
        public UserControlChartVM VM { get; set; }
        
        public UserControlChart()
        {
            InitializeComponent();
            VM = new UserControlChartVM();
            myGrid.DataContext = VM;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            VM.addChartLine((sender as ToggleButton).Name);
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            VM.removeChartLine((sender as ToggleButton).Name);
        }
    }
}
