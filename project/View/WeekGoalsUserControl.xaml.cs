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
    /// Interaction logic for UserControl8.xaml
    /// </summary>
    public partial class WeekGoalsUserControl : UserControl
    {
        public WeekGoalsUserControlVM Vm { get; set; }

        

       
        public WeekGoalsUserControl(String emailAddress)
        {
            
            Vm = new WeekGoalsUserControlVM(emailAddress);
            InitializeComponent();
            myGrid.DataContext = Vm;
        }
    }
}
