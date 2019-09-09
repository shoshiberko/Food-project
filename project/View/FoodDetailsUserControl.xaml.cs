using BE;
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
    /// Interaction logic for UserControl3.xaml
    /// </summary>
    public partial class FoodDetailsUserControl: UserControl
    {

        public static readonly DependencyProperty FoodDependency = DependencyProperty.Register("FoodDependencyProperty", typeof(String), typeof(FoodDetailsUserControl));
        public String FoodDependencyProperty
        {
            get { return (String)GetValue(FoodDependency); }
            set { SetValue(FoodDependency, value);if(value!=null&&value!="")foodDetailsViewModel.updateFoodDetailsToScreen((String)value); }
        }
        //foodDetailsViewModel.updateFoodDetailsToScreen((FoodDetails)value); }

        FoodDetailsViewModel foodDetailsViewModel;
        public FoodDetailsUserControl()
        {
            InitializeComponent();
          this.DataContext = this;
           foodDetailsViewModel = new FoodDetailsViewModel();
           this.DataContext = foodDetailsViewModel;
        }
    }
}
