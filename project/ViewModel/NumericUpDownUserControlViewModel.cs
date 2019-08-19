using project.Commands;
using System;
using System.Windows;
using System.Windows.Input;


namespace project.ViewModel
{
    public class NumericUpDownUserControlViewModel: DependencyObject
    {
        public static readonly DependencyProperty TextNumValue = DependencyProperty.Register("TextNumValueProperty", typeof(String), typeof(NumericUpDownUserControlViewModel), new UIPropertyMetadata("1"));
        public String TextNumValueProperty
        {
            get { return (String)GetValue(TextNumValue); }
            set { SetValue(TextNumValue, value); }
        } 
        public ICommand UpCommand { get; set; }
        public ICommand DownCommand { get; set; }

        public NumericUpDownUserControlViewModel()
        {
            //TextNumValueProperty = "1";
            UpCommand = new UpCommand();
            DownCommand = new DownCommand();
        }
        public void up()
        {
            TextNumValueProperty = (int.Parse(TextNumValueProperty) + 1).ToString();
        }

        public void down()
        {
            if (int.Parse(TextNumValueProperty) > 1)
            {
                TextNumValueProperty = (int.Parse(TextNumValueProperty) - 1).ToString();
            }
        }
       
    }
}
