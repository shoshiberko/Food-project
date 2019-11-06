using project.Commands;
using System;
using System.Windows;
using System.Windows.Input;


namespace project.ViewModel
{
    public class NumericUpDownUserControlViewModel: DependencyObject
    {
        #region properties
        /// <summary>
        /// this property contains the text num value on this userControl
        /// </summary>
        public static readonly DependencyProperty TextNumValue = DependencyProperty.Register("TextNumValueProperty", typeof(String), typeof(NumericUpDownUserControlViewModel), new UIPropertyMetadata("1"));
        public String TextNumValueProperty
        {
            get { return (String)GetValue(TextNumValue); }
            set { SetValue(TextNumValue, value); }
        } 
        public ICommand UpCommand { get; set; }//the command that done when the user click on the up-button
        public ICommand DownCommand { get; set; }//the command that done when the user click on the down-button
        #endregion
        public NumericUpDownUserControlViewModel()//constructor
        {
            UpCommand = new UpCommand();
            DownCommand = new DownCommand();
        }
        /// <summary>
        /// Increase in one TextNumValueProperty only if it's>1
        /// </summary>
        public void up()
        {
            TextNumValueProperty = (int.Parse(TextNumValueProperty) + 1).ToString();
        }

        /// <summary>
        /// Reduces in one TextNumValueProperty only if it's>1
        /// </summary>
        public void down()
        {
            if (int.Parse(TextNumValueProperty) > 1)
            {
                TextNumValueProperty = (int.Parse(TextNumValueProperty) - 1).ToString();
            }
        }
       
    }
}
