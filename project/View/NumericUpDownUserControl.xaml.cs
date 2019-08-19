using project.Commands;
using project.View;
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
    /// Interaction logic for NumericUpDownUserControl.xaml
    /// </summary>
    public partial class NumericUpDownUserControl : UserControl
    {
        NumericUpDownUserControlViewModel vm;
        public static readonly DependencyProperty TextNumValue = DependencyProperty.Register("TextNumValueProperty", typeof(String), typeof(NumericUpDownUserControl),new UIPropertyMetadata("1", new PropertyChangedCallback(OnTextNumValueChanged)));
        public String TextNumValueProperty
        {
            get{ return (String)GetValue(TextNumValue); }
            set { SetValue(TextNumValue, value); }
        }
        private static void OnTextNumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDownUserControl numericUpDownUserControl = d as NumericUpDownUserControl;
            numericUpDownUserControl.vm.TextNumValueProperty = numericUpDownUserControl.TextNumValueProperty;
        }
        public NumericUpDownUserControl()
        {
            InitializeComponent();
            vm = new NumericUpDownUserControlViewModel();
            stackPanel.DataContext = vm;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextNumValueProperty = (sender as TextBox).Text;
        }
    }

}

