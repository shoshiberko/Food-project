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
    /// Interaction logic for mySlider.xaml
    /// </summary>
    public partial class MySliderUserControl : UserControl
    {
        public static readonly DependencyProperty StringValue = DependencyProperty.Register("StringValueProperty", typeof(String),typeof(MySliderUserControl));
        public String StringValueProperty
        {
            get { return (String)GetValue(StringValue); }
            set { SetValue(StringValue, value); }
        }
        public MySliderUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /*
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string msg = String.Format("  {0}", (int)e.NewValue);
            this.textBlock1.Text = msg;
            Text = (int)e.NewValue;
        }
        */
    }



}
