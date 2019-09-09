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
using BE;
using BL;
using System.Collections.ObjectModel;

namespace project.View
{
    /// <summary>
    /// Interaction logic for SearchFoodComboBox.xaml
    /// </summary>
    public partial class SearchFoodComboBox : UserControl
    {
        public static readonly DependencyProperty SearchList = DependencyProperty.Register("SearchListProperty", typeof(ObservableCollection<FoodItem>), typeof(SearchFoodComboBox));
        public ObservableCollection<FoodItem> SearchListProperty
        {
            get { return (ObservableCollection<FoodItem>)GetValue(SearchList); }
            set { SetValue(SearchList, value); }
        }

        public static readonly DependencyProperty SearchText = DependencyProperty.Register("SearchTextProperty", typeof(string), typeof(SearchFoodComboBox), new UIPropertyMetadata(""));
        public string SearchTextProperty
        {
            get { return (string)GetValue(SearchText); }
            set { SetValue(SearchText, value); }
        }

        public static readonly DependencyProperty IsOpen = DependencyProperty.Register("IsOpenProperty", typeof(Boolean), typeof(SearchFoodComboBox));
        public Boolean IsOpenProperty
        {
            get { return (Boolean)GetValue(IsOpen); }
            set { SetValue(IsOpen, value); }
        }


        public static readonly DependencyProperty SelectedFoodItem = DependencyProperty.Register("SelectedFoodItemProperty", typeof(FoodItem), typeof(SearchFoodComboBox),new PropertyMetadata(null));
        public FoodItem SelectedFoodItemProperty
        {
            get { return (FoodItem)GetValue(SelectedFoodItem); }
            set { SetValue(SelectedFoodItem, value); }
        }
        
        ImpBL BL;
        public SearchFoodComboBox()
        {
            InitializeComponent();
            //LayoutRoot.DataContext = this;///////
            BL = new ImpBL();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            String textToSearch = ((TextBox)sender).Text;
            if (textToSearch != null && textToSearch != "")
            {
                SearchListProperty = new ObservableCollection<FoodItem>( BL.getListFoodItems(textToSearch));
                IsOpenProperty = true;
            }
            //SelectedFoodItemProperty = new FoodItem() { Name = "Pizza", Key = "123" };
        }
    }
}
